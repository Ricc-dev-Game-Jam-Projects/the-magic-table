using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCore : FreeWood
{
    public Slot MaterialSlot;

    private bool GotIt;
    private ItemController itemController;

    protected override void _Start()
    {
        base._Start();

        itemController = new ItemController();
        DoneText = "Get it!";
    }

    void Update()
    {
        if (MySlot.item != null)
        {
            Tool t = MySlot.item as Tool;

            TimeTo = t.TimeToProduce;
            PrefixText = t.Name + " collects " + itemController.Index(t.MaterialID).Name;

            if (!GotIt)
            {
                MySlot.Data.StackIntegrity(2);                
            }

            GotIt = true;

            FreeRawMaterial(t.MaterialID, MaterialSlot);
        } else
        {
            GotIt = false;
            TimeAt = 0f;
        }
    }
}
