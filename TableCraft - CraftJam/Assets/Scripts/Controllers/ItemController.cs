using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController
{
    public static List<Item> Items { get; private set; } = new List<Item>();

    private string ItemPath = "Data/Items/";
    private DataReader reader = new DataReader(false);

    public ItemController()
    {
        GetData();
        Debug.Log("Items");
    }

    public Item[] Index()
    {
        GetData();
        return Items.ToArray();
    }

    public Item Index(int ID)
    {
        GetData();
        Item te = Items.Find(x => x.ID == ID);
        return te;
    }
    public Item Index(string ItemName)
    {
        GetData();
        Item te = Items.Find(x => x.Name == ItemName);
        return te;
    }

    public void Create(Item item)
    {
        Items.Add(item);
    }

    private void GetData()
    {
        return;
        if (Items.Count != 0)
        {
            return;
        }
        List<Item> items = reader.GetAllObjectFromJson<Item>(ItemPath).ToList();
        Tool[] tools = reader.GetAllObjectFromJson<Tool>(ItemPath).Where(x => x.MaterialID != 0 && x.TimeToProduce != 0f).ToArray();

        if (items != null)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int qnt = tools.Where(x => x.ID == items[i].ID).Count();
                if (qnt > 0)
                {
                    items.Remove(items[i]);
                    i--;
                }
            }
            Items.AddRange(items);
            Items.AddRange(tools);
        }
    }
}