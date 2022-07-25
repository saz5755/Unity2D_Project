using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopShop : PopBase
{
    [SerializeField]
    private Transform gridLayout;

    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;
        RefleshUI();
    }

    private void InitList()
    {
        Dictionary<string, GameObject> dicUnits = GameData.Instance.GetPlayerUnits();
        Dictionary<string, int> unitprice = GameData.Instance.GetShopUnits();

        int Index = 0;

        foreach (KeyValuePair<string, GameObject> kvp in dicUnits)
        {
            PlayerUnit unit = kvp.Value.GetComponent<PlayerUnit>();
            if (unit != null)
            {
                if (IsCollectUnit(kvp.Key))
                {
                    continue;
                }

                if (Index < gridLayout.childCount)
                {
                    ShopSlotData shopSlotData = gridLayout.GetChild(Index).GetComponent<ShopSlotData>();
                    shopSlotData.gameObject.SetActive(true);
                    shopSlotData.image.sprite = kvp.Value.GetComponent<SpriteRenderer>().sprite;
                    shopSlotData.txtPrice.text = unitprice[kvp.Key].ToString();
                    shopSlotData.txtName.text = kvp.Key;

                    shopSlotData.SetClickAction(OnClickSlot);
                    Index++;
                }
                else
                {
                    return;
                }
            }
        }

        for (; Index < gridLayout.childCount; Index++)
        {
            ShopSlotData shopSlotData = gridLayout.GetChild(Index).GetComponent<ShopSlotData>();
            shopSlotData.gameObject.SetActive(false);
        }
    }

    private bool IsCollectUnit(string name)
    {
        for (int i = 0; i < GameData.Instance.collectUnitNames.Count; i++)
        {
            if (GameData.Instance.collectUnitNames[i].Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    public void OnClickSlot(string key, string price)
    {
        if (price == "~" || price.Length == 0)
        {
            return;
        }

        MainSceneUIManager uiManger = parentManager.GetComponent<MainSceneUIManager>();

        if (uiManger == null)
            return;

        PopBuyCheck popBuyCheck = (PopBuyCheck)uiManger.FindPopup("PopBuyCheck");

        if (popBuyCheck == null)
        {
            return;
        }

        popBuyCheck.Close();

        if (GameData.Instance.playerScore >= int.Parse(price))
        {
            popBuyCheck.gameObject.SetActive(true);
            popBuyCheck.SetDesc(CheckMsg.BuyCheckMsg, price, key);
            popBuyCheck.SetButtonActions(OnBuyCheckOK);
            popBuyCheck.OpenUI(parentManager);
        }
        else
        {
            popBuyCheck.gameObject.SetActive(true);
                    
            popBuyCheck.SetDesc(CheckMsg.NotEnoughGold);
            popBuyCheck.SetButtonActions(null);
            popBuyCheck.OpenUI(parentManager);
        }
    }

    protected override void RefleshUI()
    {
        InitList();

        MainSceneUIManager uiManger = parentManager.GetComponent<MainSceneUIManager>();

        if (uiManger != null)
        {
            uiManger.RefleshUI();
        }
    }

    public void OnBuyCheckOK(string strPrice, string strUnitName)
    {
        GameData.Instance.collectUnitNames.Add(strUnitName);
        GameData.Instance.playerScore -= int.Parse(strPrice);
        RefleshUI();
    }
}
