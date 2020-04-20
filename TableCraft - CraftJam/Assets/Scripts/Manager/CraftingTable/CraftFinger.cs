using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftFinger : MonoBehaviour
{
    public Finger finger;
    public Crafter crafter;

    private Blueprint[] blueprints;

    private void Start()
    {
        finger = Finger.finger;
        crafter = Crafter.instance;
        BlueprintController blueprintController = new BlueprintController();
        blueprints = blueprintController.Index();
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Slot[] slots = finger.SelectedSlots.ToArray();

            Dictionary<int, int> itemsToCraft = new Dictionary<int, int>();

            foreach(Slot slot in slots)
            {
                if(slot.item != null)
                {
                    if (itemsToCraft.ContainsKey(slot.item.ID))
                    {
                        itemsToCraft[slot.item.ID] += slot.Quantity;
                    } else
                    {
                        itemsToCraft.Add(slot.item.ID, slot.Quantity);
                    }
                }
            }

            if(itemsToCraft.Count == 0)
            {
                Debug.Log("No items");
                return;
            }

            foreach (Blueprint blueprint in blueprints)
            {
                if (blueprint.TryToCraft(itemsToCraft).Key != -1)
                {
                    crafter.CraftItem(blueprint, slots);
                    break;
                }
            }
        }
    }


}
