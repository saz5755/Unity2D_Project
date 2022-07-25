using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CheckMsg
{
    None = 0,
    BuyCheckMsg = 1,
    NotEnoughGold = 2,
}

public class PopBuyCheck : PopBase
{
    [SerializeField]
    private Text txtDesc;
    [SerializeField]
    private Image imgUnit;

    private string unitPrice;
    private string unitName;
    private CheckMsg checkMsgNum;

    private Action<string, string> onClickOk;
    private Action<string, string> onClickCancel;

    public void SetButtonActions(Action<string, string> action1, Action<string, string> action2 = null)
    {
        onClickOk = action1;
        if (action2 != null)
        {
            onClickCancel = action2;
        }
    }

    public void SetDesc(CheckMsg eMsgNum, string strPrice = "", string strUnitName = "")
    {
        checkMsgNum = eMsgNum;
        unitPrice = strPrice;
        unitName = strUnitName;
    }

    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;

        RefleshUI();
    }

    protected override void RefleshUI()
    {
        switch (checkMsgNum)
        {
            case CheckMsg.None:
                if (imgUnit != null)
                    imgUnit.sprite = null;
                if (txtDesc != null)
                    txtDesc.text = "";
                break;
            case CheckMsg.BuyCheckMsg:
                {
                    Dictionary<string, GameObject> dicUnits = GameData.Instance.GetPlayerUnits();
                    if (imgUnit != null) imgUnit.sprite = dicUnits[unitName].GetComponent<SpriteRenderer>().sprite;
                    if (txtDesc != null) txtDesc.text = string.Format("{0}을(를) 구매하시겠습니까?\n 가격 : {1}", unitName, unitPrice);
                }
                break;

            case CheckMsg.NotEnoughGold:
                {
                    if (imgUnit != null)
                        imgUnit.sprite = null;

                    if (txtDesc != null) txtDesc.text = string.Format("재화가 충분하지 않습니다. 소지금 :{0}", GameData.Instance.playerScore.ToString());
                }
                break;
        }
    }

    public void OnClickOK()
    {
        if (onClickOk != null)
            onClickOk(unitPrice, unitName);

        Close();
    }

    public void OnClickCancel()
    {
        if (onClickCancel != null)
            onClickCancel(unitPrice, unitName);

        Close();
    }
}
