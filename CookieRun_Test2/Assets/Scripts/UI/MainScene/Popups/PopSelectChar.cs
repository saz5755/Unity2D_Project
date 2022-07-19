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

    List<GameObject> playerUnits = new List<GameObject>();

    PlayerUnit selectUnit;

    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;
        GameObject[] loedPlayers = Resources.LoadAll<GameObject>("Unit/hero/Prefab");
        
        foreach(GameObject go in loedPlayers)
        {
            PlayerUnit unit = go.GetComponent<PlayerUnit>();
            if (unit != null)
            {
                for(int i = 0; i < GameData.Instance.collectUnitNames.Count; i++)
                {
                    if (GameData.Instance.collectUnitNames[i].Equals(unit.name))
                    {
                        playerUnits.Add(go);
                    }
                }
            }
        }

        currentPage = 0;

        selectUnit = Instantiate(playerUnits[currentPage], posUnit).GetComponent<PlayerUnit>();

        RefleshUI();
    }

    protected override void RefleshUI()
    {
        txtUnitName.text = playerUnits[currentPage].name;

        
    }

    public void OnClickPrev()
    {
        if (currentPage > 0)
            currentPage--;

        if (currentPage == 0)
            currentPage = playerUnits.Count - 1;

        if (selectUnit.name != playerUnits[currentPage].GetComponent<PlayerUnit>().name)
        {
            Destroy(selectUnit);
            selectUnit = Instantiate(playerUnits[currentPage], posUnit).GetComponent<PlayerUnit>();
        }

        RefleshUI();
    }

    public void OnClickNext()
    {
        if (currentPage < playerUnits.Count)
            currentPage++;

        if (currentPage == playerUnits.Count)
            currentPage = 0;

        if (selectUnit.name != playerUnits[currentPage].GetComponent<PlayerUnit>().name)
        {
            Destroy(selectUnit);
            selectUnit = Instantiate(playerUnits[currentPage], posUnit).GetComponent<PlayerUnit>();
        }

        RefleshUI();
    }
}
