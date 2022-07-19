using UnityEngine;
using System;

public class Inventory
{
    public int gold;
    
    public ItemCell[] cells = new ItemCell[6];

    public Inventory()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new ItemCell();
        }
    }

    public void AddItem(string code, int count)
    {
        int index = Array.FindIndex(cells, e => e.data.itemCode == code);
        if (index != -1)
        {
            cells[index].itemCount += count;
        }
        else
        {
            index = Array.FindIndex(cells, e => e.itemCount == 0);
            if (index != -1)
            {
                cells[index].data = ItemManager.GetItem(code);
                cells[index].itemCount = count;
            }
        }
    }

    public void Remove(string code, int removeCount)
    {
        int index = Array.FindIndex(cells, e => e.data.itemCode == code);
        if (index != -1)
        {
            int itemCount = cells[index].itemCount;
            if (itemCount <= removeCount)
            {
                cells[index].Initialize();
            }
            else
            {
                cells[index].itemCount -= removeCount;
            }
        }
    }

    public void SwapItem(int dragIndex, int dropIndex)
    {
        ItemCell cell = new ItemCell();

        cell.SetCell(cells[dragIndex]);
        cells[dragIndex].SetCell(cells[dropIndex]);
        cells[dropIndex].SetCell(cell);
    }

    public void UseItem(string code, object param = null, int count = 1)
    {
        int index = Array.FindIndex(cells, e => e.data.itemCode == code);
        if (index != -1) {
            for (int i = 0; i < count; i++)
            {
                if (cells[index].itemCount > 0)
                {
                    cells[index].data.UseItem(param);
                    Remove(code, 1);
                }
            }
        }
    }
}