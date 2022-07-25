using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : TObjSingleton<GameData>
{
    private GameManager gm;

    public int playerScore = 150000;
    public string playerName = "�̸�����";
    public int lastSelectUnit = 0;

    public List<string> collectUnitNames = new List<string>();

    public int minUnitPrice = 100000;

    private Dictionary<string, GameObject> playerUnits = new Dictionary<string, GameObject>();
    private Dictionary<string, int> unitShop = new Dictionary<string, int>();

    public string curMapName = "Map";

    public override void Initialize()
    {
        LoadCollectUnitData();
        LoadPlayerUnits();
    }

    private void LoadCollectUnitData()
    {
        if (collectUnitNames.Count == 0)
        {
            collectUnitNames.Add("���̳�");
        }
    }

    public void LoadPlayerUnits()
    {
        GameObject[] loedPlayers = Resources.LoadAll<GameObject>("Unit/hero/Prefab");

        int price = minUnitPrice;

        foreach (GameObject go in loedPlayers)
        {
            PlayerUnit unit = go.GetComponent<PlayerUnit>();
            if (unit != null && unit.UnitName.Length != 0)
            {
                playerUnits.Add(unit.UnitName, go);
                unitShop.Add(unit.UnitName, price);

                price += 50000;
            }
        }
    }

    public Dictionary<string, GameObject> GetPlayerUnits()
    {
        if (playerUnits.Count == 0)
        {
            LoadPlayerUnits();
        }

        return playerUnits;
    }

    public Dictionary<string, int> GetShopUnits()
    {
        return unitShop;
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
        // ������ ������ ���� ������
        if (playerUnits.Count != 0)
        {
            if (playerUnits.ContainsKey(GetLastSelectUnitName()))
            {
                return playerUnits[GetLastSelectUnitName()];
            }
            else
            {
                return playerUnits["���̳�"];
            }
        }

        return null;
    }
}
