using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopSelectChar : PopBase
{
    [SerializeField]
    Text txtUnitName;

    [SerializeField]
    Transform posUnit;

    private int currentPage = 0;

    List<string> lstUintNames = new List<string>();
    List<Image> playerUnitImages = new List<Image>();

    Image selectUnitImage;
    GameObject selectObject;

    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;
        Dictionary<string, GameObject> dicUnits = GameData.Instance.GetPlayerUnits();
        ResetList();

        foreach (KeyValuePair<string, GameObject> kvp in dicUnits)
        {
            PlayerUnit unit = kvp.Value.GetComponent<PlayerUnit>();
            if (unit != null)
            {
                for(int i = 0; i < GameData.Instance.collectUnitNames.Count; i++)
                {
                    if (GameData.Instance.collectUnitNames[i].Equals(kvp.Key))
                    {
                        Image unitImg = (new GameObject()).AddComponent<Image>();
                        unitImg.sprite = kvp.Value.GetComponent<SpriteRenderer>().sprite;
                        playerUnitImages.Add(unitImg);
                        lstUintNames.Add(kvp.Key);

                        Destroy(unitImg);
                    }
                }
            }
        }

        currentPage = 0;

        if (selectUnitImage == null)
        {
            selectUnitImage = (new GameObject()).AddComponent<Image>();
        }

        if (posUnit != null)
        {
            selectUnitImage.sprite = playerUnitImages[currentPage].sprite;
            if (selectObject == null)
            {
                selectObject = Instantiate(selectUnitImage, posUnit).gameObject;
                selectObject.name = "UnitImage";
            }
            
        }

        RefleshUI();
    }

    protected override void RefleshUI()
    {
        if (txtUnitName.text != lstUintNames[currentPage])
        {
            txtUnitName.text = lstUintNames[currentPage];
            selectObject.GetComponent<Image>().sprite = playerUnitImages[currentPage].sprite;
        }
    }

    private void ResetList()
    {
        lstUintNames.Clear();
        playerUnitImages.Clear();
    }

    public void OnClickPrev()
    {
        if (currentPage > 0)
            currentPage--;

        if (currentPage < 0)
            currentPage = playerUnitImages.Count - 1;

        RefleshUI();
    }

    public void OnClickNext()
    {
        if (currentPage < playerUnitImages.Count)
            currentPage++;

        if (currentPage == playerUnitImages.Count)
            currentPage = 0;

        RefleshUI();
    }

    public void OnClickSelectBtn()
    {
        for (int i = 0; i < GameData.Instance.collectUnitNames.Count; i++)
        {
            if (GameData.Instance.collectUnitNames[i].Equals(lstUintNames[currentPage]))
            {
                GameData.Instance.lastSelectUnit = i;
                parentManager.GetComponent<MainSceneUIManager>().RefleshUI();
                break;
            }
        }
        ResetList();
        Close();
    }
}
