using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Slot))]
public class UpgradeTable : MonoBehaviour
{
    public Slot mySlot;
    public Table table;

    private ItemController itemController;

    void Start()
    {
        mySlot = GetComponent<Slot>();
        itemController = new ItemController();
    }

    void Update()
    {
        if(mySlot.Data.Item != null)
        {
            table.IncrementSize();
            mySlot.RemoveItem();
        }
    }

    public bool CanDrop(int ID)
    {
        return ID == itemController.Index("Crafting Table").ID;
    }
}
