using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Crafter : MonoBehaviour
{
    public static Crafter instance;

    public Table table;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        table = FindObjectOfType<Table>();
    }

    public void CraftItem(Blueprint blueprint, Slot[] slots)
    {
        Slot freeSlot = null;
        foreach (Slot s in slots)
        {
            if (s.item == null) return;
            if (s.item is Tool) break;
            if (s.item.Permanent) continue;
            for (int i = 0; i < blueprint.ItemsRequired.Count; i++)
            {
                //if(blueprint.Crafted.Keys.ElementAt(0) == s.Data.Item.ID)
                //{
                //    equals = true;
                //    freeSlot = s;
                //    break;
                //}

                int qnt = blueprint.ItemsRequired[s.item.ID];
                if (s.Quantity == qnt)
                {
                    freeSlot = s;
                    break;
                }
            }
        }
        
        ItemController itemController = new ItemController();
        Item newItem = itemController.Index(blueprint.Crafted.First().Key);

        if(freeSlot == null)
        {
            foreach (Slot s in table.SlotGrid)
            {
                if (s.Free())
                {
                    freeSlot = s;
                    break;
                }
            }
            if(freeSlot == null)
            {
                return;
            }
        } 
        
        foreach (Slot s in slots)
        {
            if(!(s.item is Tool))
            {
                int qntToRemove = blueprint.ItemsRequired[s.item.ID];
                s.RemoveItem(qntToRemove);    
            }
        }
        SlotData data = new SlotData();
        data.SetData(newItem, blueprint.Crafted.First().Value, integrity: newItem.Integrity, lifeTime: newItem.LifeTime);
        freeSlot.SwapItem(data);
    }
}
