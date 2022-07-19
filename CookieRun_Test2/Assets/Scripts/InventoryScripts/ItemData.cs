using UnityEngine;
using UnityEngine.Events;

public class ItemData
{
    public string itemCode;
    public string itemName;
    public string itemDescript;
    public Sprite itemImage;
    public int itemPrice;

    public UnityAction<object> useItem;

    public ItemData(string itemCode, string itemName, string itemDescript, int itemPrice, UnityAction<object> useItem = null)
    {
        this.itemCode = itemCode;
        this.itemName = itemName;
        this.itemDescript = itemDescript;
        this.itemPrice = itemPrice;
        this.itemImage = Resources.Load<Sprite>($"Shop/InventoryResources/Sprites/UI/Item/{itemCode}");

        this.useItem += useItem;
    }

    public virtual void UseItem(object param)
    {
        if (useItem != null)
        {
            useItem(param);
        }
    }
}