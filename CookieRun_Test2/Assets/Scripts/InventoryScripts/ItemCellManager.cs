using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemCellManager : EventTrigger
{
    public int index = 0;
    private InventoryManager inventoryManager;

    public delegate void OnDragging(Vector3 pos);
    public OnDragging onDragging;

    public delegate void OnDragEvent(ItemData item);
    public OnDragEvent onDragStart;
    public OnDragEvent onDragEnd;

    public delegate void OnClick(ItemCell cell);
    public OnClick onClick;

    public ItemCell data;
    [SerializeField] private Image imgItemImage;
    [SerializeField] private Text txtItemCount;

    public void SetOnDragStart(OnDragEvent func)
    {
        onDragStart = func;
    }

    public void SetOnDragging(OnDragging func)
    {
        onDragging = func;
    }

    public void SetOnDragEnd(OnDragEvent func)
    {
        onDragEnd = func;
    }

    public void SetManager(InventoryManager manager)
    {
        inventoryManager = manager;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (data != null)
            {
                if (data.itemCount > 0)
                {
                    onClick(data);
                    Refresh(data);
                }
            }
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        onDragStart(data.data);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        onDragEnd(data.data);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        onDragging(eventData.position);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<ItemCellManager>() != null)
        {
            int dragIndex = eventData.pointerDrag.GetComponent<ItemCellManager>().index;
            inventoryManager.SwapItem(dragIndex, index);
        }
    }

    public void Refresh(ItemCell cell)
    {
        data = cell;
        if (cell.itemCount == 0)
        {
            imgItemImage.sprite = null;
            imgItemImage.color = new Color(0, 0, 0, 0);
            txtItemCount.text = "";
        }
        else
        {
            imgItemImage.sprite = cell.data.itemImage;
            imgItemImage.color = Color.white;
            txtItemCount.text = cell.itemCount.ToString();
        }
    }
}
