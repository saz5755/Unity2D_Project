using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotData : MonoBehaviour
{
    public Text txtPrice;
    public Image image;
    public Text txtName;

    private Action<string, string> clickAction;

    public void SetClickAction(Action<string, string> action)
    {
        clickAction = action;
    }

    public void OnClick()
    {
        if (clickAction != null)
            clickAction(txtName.text, txtPrice.text);
    }
}
