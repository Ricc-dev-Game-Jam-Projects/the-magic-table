using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public static Finger finger;
    
    public Slot LookingSlot;
    public ItemFollowMouse itemFollowMouse;
    public List<Slot> SelectedSlots;
    public List<Slot> Slots { get; set; }

    public float MinDistance = 0.45f;


    private bool Crafting = false;

    private void Awake()
    {
        if(finger == null)
        {
            finger = this;
        }
        else
        {
            Destroy(gameObject, 0f);
        }
        Slots = new List<Slot>(FindObjectsOfType<Slot>());

        itemFollowMouse = FindObjectOfType<ItemFollowMouse>();
    }

    void Start()
    {
        SelectedSlots = new List<Slot>();
    }

    private void LookFor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;
        itemFollowMouse.transform.position = mousePos;
        bool found = false;

        foreach (Slot s in Slots)
        {
            float dis = Distance(mousePos, s);

            if (dis <= MinDistance)
            {
                LookingSlot = s;
                found = true;
                if(Crafting && !SelectedSlots.Contains(s))
                {
                    SelectedSlots.Add(s);
                    s.GetComponent<SpriteRenderer>().color = new Color32(190, 255, 173, 255);
                }
                break;
            }
        }
        if (!found)
        {
            LookingSlot = null;
        }
    }

    private void Update()
    {
        LookFor();

        if (!itemFollowMouse.Following && Input.GetMouseButton(0))
        {
            if(LookingSlot != null)
            {
                itemFollowMouse.StartFollow(LookingSlot);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(LookingSlot != null)
            {
                if (LookingSlot.GetComponent<ToolCore>() != null)
                {
                    if (itemFollowMouse.SlotHolding.Data.Item as Tool != null)
                    {
                        itemFollowMouse.SwapItem(LookingSlot);
                    }
                } else if (LookingSlot.GetComponent<UpgradeTable>() != null) 
                {
                    if (LookingSlot.GetComponent<UpgradeTable>().CanDrop(itemFollowMouse.SlotHolding.Data.Item.ID))
                    {
                        itemFollowMouse.SwapItem(LookingSlot);
                    }
                } else
                {
                    itemFollowMouse.SwapItem(LookingSlot);
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            if(!Crafting)
            {
                SelectedSlots.Clear();
            }
            Crafting = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Crafting = false;
            foreach(Slot s in SelectedSlots)
            {
                s.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public static float Distance(Vector3 mousePosition, Slot s)
    {
        return Vector3.Distance(mousePosition, s.transform.position);
    }
}
