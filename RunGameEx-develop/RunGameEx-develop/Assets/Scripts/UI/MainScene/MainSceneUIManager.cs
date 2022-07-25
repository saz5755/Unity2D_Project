using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour
{
    [SerializeField]
    private List<PopBase> popUps = new List<PopBase>();

    private PopBase curPop;

    [SerializeField]
    private Text txtGold;

    [SerializeField]
    private Text txtPlayerName;

    [SerializeField]
    private Transform curUnitPos;

    [SerializeField]
    private Text txtSelectUnitName;

    [SerializeField]
    private Image unitImage;

    [SerializeField]
    private Image eventImage;


    private void Awake()
    {
        if (popUps!=null)
        {
            for (int i = 0; i < popUps.Count; i++)
            {
                if (popUps[i].gameObject.activeSelf == true)
                    popUps[i].gameObject.SetActive(false);
            }
        }

        if (curUnitPos != null)
        {
            if (unitImage == null)
            {
                Image unit = (new GameObject()).AddComponent<Image>();
                Transform StartTransform = curUnitPos;
                unitImage = Instantiate(unit, StartTransform);
            }
        }

        RefleshUI();
    }

    public PopBase FindPopup(string name)
    {
        for (int i = 0; i < popUps.Count; i++)
        {
            if (popUps[i].name == name)
            {
                return popUps[i];
            }
        }

        return null;
    }

    public void OpenUI(string strPopupName)
    {
        if (curPop != null)
        {
            if (curPop.name == strPopupName)
            {
                if (curPop.gameObject.activeSelf)
                    return;

                curPop.gameObject.SetActive(true);
                curPop.OpenUI(this.gameObject);
            }
            else
            {
                curPop.Close();
                curPop = FindPopup(strPopupName);

                if (curPop != null)
                {

                    curPop.gameObject.SetActive(true);
                    curPop.OpenUI(this.gameObject);

                    return;
                }
            }
        }
        else
        {
            curPop = FindPopup(strPopupName);

            if (curPop != null)
            {
                curPop.gameObject.SetActive(true);
                curPop.OpenUI(this.gameObject);

                return;
            }
        }
    }


    public void OnClickSelectChar()
    {
        OpenUI("PopSelectChar");
    }

    public void OnClickOpenShop()
    {
        OpenUI("PopShopUI");
    }

    public void OnClickOpenPost()
    {
        PopBase popBase = FindPopup("PopPostUI");
        if (popBase != null && popBase.gameObject.activeSelf)
        {
            popBase.Close();
            return;
        }

        OpenUI("PopPostUI");
    }

    public void OnClickOpenOption()
    {
        OpenUI("PopOptionUI");
    }

    public void RefleshUI()
    {
        if (txtPlayerName != null)
        {
            txtPlayerName.text = GameData.Instance.playerName;
        }

        if (txtGold != null)
        {
            txtGold.text = GameData.Instance.playerScore.ToString();
        }

        if (txtSelectUnitName != null)
        {
            txtSelectUnitName.text = GameData.Instance.GetLastSelectUnitName();
        }

        if(unitImage != null)
        {
            unitImage.sprite = GameData.Instance.GetLastSelectUnit().GetComponent<SpriteRenderer>().sprite;
        }

        if (eventImage != null)
        {
            Color color = eventImage.color;

            if (eventImage.sprite == null)
            {
                color.a = 0f;
            }
            else
            {
                color.a = 1f;
            }

            eventImage.color = color;
        }
    }
}
