using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGameManager : MonoBehaviour
{
    private void Awake()
    {
        ItemManager.AddItem(new ItemData("00002", "�������", "�������뷮��", 50, (o) =>
        {
            Debug.Log("������� �������");
        }));
        ItemManager.AddItem(new ItemData("00004", "��Ȳ����", "��Ȳ��Ȳ������", 150, (o) =>
        {
            Debug.Log("��Ȳ���� �������");
        }));
        ItemManager.AddItem(new ItemData("01000", "������� ����", "ü�� + 1000000", 3000));
        ItemManager.AddItem(new ItemData("01001", "3��", "�ҹ�� �ܴܵ���", 20000));
        ItemManager.AddItem(new ItemData("02000", "������ �ǹٶ��", "�� �� ���� �濰��", 600000));
    }
}
