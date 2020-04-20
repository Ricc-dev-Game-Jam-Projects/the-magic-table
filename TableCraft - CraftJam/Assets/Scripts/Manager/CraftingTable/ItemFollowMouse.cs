using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollowMouse : MonoBehaviour
{
    public bool Following = false;
    public Slot SlotHolding;
    
    public void StartFollow(Slot slot)
    {
        if(slot.item == null)
        {
            return;
        }
        Following = true;
        SlotHolding = slot;
        GetComponent<SpriteRenderer>().sprite = slot.spriteRenderer.sprite;
    }

    public void SwapItem(Slot slot)
    {
        if(!slot.Positionable || SlotHolding == slot || SlotHolding == null)
        {
            return;
        }
        
        if (slot.Data.Item != null && slot.Data.Item.ID == SlotHolding.Data.Item.ID && !(slot.Data.Item is Tool))
        {
            // Adds Equals
            SlotData sData = new SlotData();
            sData.SetData(slot.Data.Item, slot.Data.Quantity + SlotHolding.Data.Quantity, 
                            integrity: slot.Data.Integrity, 
                            lifeTime: slot.Data.LifeTime);
            SlotHolding.DisposeData();
            slot.SwapItem(sData);
        } else
        {
            SlotData sData = new SlotData(SlotHolding.Data);
            SlotHolding.SwapItem(slot.Data);
            slot.SwapItem(sData);
        }
    }

    void Update()
    {
        if(Following)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 1f;
            transform.position = mousePos;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Following = false;
        }
    }
}
