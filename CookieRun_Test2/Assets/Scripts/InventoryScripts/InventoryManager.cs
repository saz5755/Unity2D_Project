using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private List<ItemCellManager> cells = new List<ItemCellManager>();

    [SerializeField]
    private Transform itemCellParent;
    private GameObject itemCellPrefab;

    public Inventory inventory;

    [SerializeField]
    private GameObject itemShadow;

    private void Awake()
    {
        inventory = new Inventory();
        itemShadow.SetActive(false);

        itemCellPrefab = Resources.Load<GameObject>("Shop/InventoryResources/Prefabs/UI/ItemCell");
        for (int i = 0; i < inventory.cells.Length; i++)
        {
            int index = i;
            ItemCellManager cell = Instantiate(itemCellPrefab, itemCellParent).GetComponent<ItemCellManager>();
            cell.Refresh(inventory.cells[index]);
            cell.SetManager(this);

            cell.SetOnDragStart((item) =>
            {
                itemShadow.SetActive(true);
                itemShadow.GetComponent<Image>().sprite = item.itemImage;
            });

            cell.SetOnDragging((pos) =>
            {
                itemShadow.GetComponent<RectTransform>().position = pos;
            });

            cell.SetOnDragEnd((item) =>
            {
                itemShadow.SetActive(false);
            });

            cell.index = index;

            cell.onClick += (p) =>
            {
                inventory.UseItem(p.data.itemCode);
            };
            cells.Add(cell);
        }

        AddItem("00002", 4);
        AddItem("00004", 5);
    }

    public void SwapItem(int a, int b)
    {
        inventory.SwapItem(a, b);
        Refresh();
    }

    public void AddItem(string code, int count)
    {
        inventory.AddItem(code, count);
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < inventory.cells.Length; i++)
        {
            cells[i].Refresh(inventory.cells[i]);
        }
    }
}
