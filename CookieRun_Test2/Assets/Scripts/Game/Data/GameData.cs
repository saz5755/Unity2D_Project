using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : TObjSingleton<GameData>
{
    private GameManager gm;

    public int playerScore = 0;
    public string playerName;
    public int lastSelectUnit = 0;

    public List<string> collectUnitNames = new List<string>();

    private Dictionary<string, GameObject> playerUnits = new Dictionary<string, GameObject>();

    public override void Initialize()
    {
        LoadCollectUnitData();
        LoadPlayerUnits();
    }

    private void LoadCollectUnitData()
    {
        if (collectUnitNames.Count == 0)
        {
            collectUnitNames.Add("다이노");
        }
    }

    public void LoadPlayerUnits()
    {
        GameObject[] loedPlayers = Resources.LoadAll<GameObject>("Unit/hero/Prefab");

        foreach (GameObject go in loedPlayers)
        {
            PlayerUnit unit = go.GetComponent<PlayerUnit>();
            if (unit != null && unit.UnitName.Length != 0)
            {
                playerUnits.Add(unit.UnitName, go);
            }
        }
    }

    public GameObject[] GetPlayerUnits()
    {
        List<GameObject> lstUnit = new List<GameObject>();
        
        // 리스트를 얻어옴

        return lstUnit.ToArray();
    }

    public GameManager GetGameManagerCompornent()
    {
        if (gm == null)
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        return gm;
    }

    public string GetLastSelectUnitName()
    {
        return collectUnitNames[lastSelectUnit];
    }

    public GameObject GetLastSelectUnit()
    {
        // 마지막 선택한 유닛 얻어오기
        if (playerUnits.Count != 0)
        {
            if (playerUnits.ContainsKey(GetLastSelectUnitName()))
            {
                return playerUnits[GetLastSelectUnitName()];
            }
            else
            {
                return playerUnits["다이노"];
            }
        }

        return null;
    }
}
