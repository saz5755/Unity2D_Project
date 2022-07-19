using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGameManager : MonoBehaviour
{
    private void Awake()
    {
        ItemManager.AddItem(new ItemData("00002", "노란포션", "노랑노랑노량진", 50, (o) =>
        {
            Debug.Log("노랑포션 사용했음");
        }));
        ItemManager.AddItem(new ItemData("00004", "주황포션", "주황주황해진다", 150, (o) =>
        {
            Debug.Log("주황포션 사용했음");
        }));
        ItemManager.AddItem(new ItemData("01000", "워모그의 갑옷", "체력 + 1000000", 3000));
        ItemManager.AddItem(new ItemData("01001", "3뚝", "뚝배기 단단데스", 20000));
        ItemManager.AddItem(new ItemData("02000", "강동구 피바라기", "내 손 안의 흑염룡", 600000));
    }
}
