using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Slot))]
public class ItemCore : MonoBehaviour
{
    private Slot MySlot;

    SlotData Data {
        get {
            return MySlot.Data;
        }
    }

    private float TimeMx = -1f;
    private int IntegrityMax = -1;

    private void Start()
    {
        MySlot = GetComponent<Slot>();
        if(!MySlot.Positionable)
        {
            enabled = false;
        }
        MySlot.SlotSwaped = () =>
        {
            if (MySlot.item != null)
            {
                TimeMx = MySlot.itemController.Index(MySlot.item.ID).LifeTime;
                IntegrityMax = MySlot.itemController.Index(MySlot.item.ID).Integrity;
            }
        };
    }

    void Update()
    {
        if(Data.Item != null)
        {
            if (!Data.Item.Permanent)
            {
                if(Data.Item != null && Data.Item.LifeTime != -1f)
                {
                    if (Data.Item.LifeTime > 0f)
                        DecomposeByTime();
                }

                if (Data.Item != null && Data.Item.Integrity != -1)
                {
                    if (Data.Item.Integrity > 0)
                        DecomposeByUse();
                }
            }
        } 
    }

    public void Decompose()
    {
        bool disposed = MySlot.RemoveItem();
        if (!disposed)
        {
            MySlot.Data.LifeTime = TimeMx;
            MySlot.Data.Integrity = IntegrityMax;
        }
        else
        {
            TimeMx = 0f;
        }
    }

    void DecomposeByUse()
    {
        float val = Data.Integrity / IntegrityMax;
        int tick = 0;

        if (val >= 1f && val > 0.75f)
        {
            tick = 0;
        }
        else if (val <= 0.75f && val > 0.5f)
        {
            tick = 1;
        }
        else if (val <= 0.5f && val > 0.25f)
        {
            tick = 2;
        }
        else if (val <= 0.25f && val > 0f)
        {
            tick = 3;
        }
        else if (val <= 0f)
        {
            tick = -1;
        }

        MySlot.TickCircle(tick);

        if(Data.Integrity == 0)
        {
            Decompose();
        }

    }

    void DecomposeByTime()
    {
        Data.StackLife(Time.deltaTime);
        float val = Data.LifeTime / TimeMx;
        int tick = 0;

        if (val >= 1f && val > 0.75f)
        {
            tick = 0;
        }
        else if (val <= 0.75f && val > 0.5f)
        {
            tick = 1;
        }
        else if (val <= 0.5f && val > 0.25f)
        {
            tick = 2;
        }
        else if (val <= 0.25f && val > 0f)
        {
            tick = 3;
        } else if(val <= 0f)
        {
            tick = -1;
        }

        MySlot.TickCircle(tick);
        
        if(Data.LifeTime <= 0f)
        {
            Decompose();
        }
    }
}
