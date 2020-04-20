using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blueprint
{
	public Dictionary<int, int> ItemsRequired { get; set; }

	public Dictionary<int, int> Crafted { get; set; }

	public int ID { get; set; }

	public Blueprint(int ID, Dictionary<int, int> ItemsRequired, Dictionary<int, int> ItemCrafted)
	{
		this.ID = ID;
		this.ItemsRequired = ItemsRequired;
		this.Crafted = ItemCrafted;
	}

	public KeyValuePair<int, int> TryToCraft(Dictionary<int, int> items)
	{
		KeyValuePair<int, int> nothing = new KeyValuePair<int, int>(-1, -1);

		if (items.Keys.Count < ItemsRequired.Count)
		{
			return nothing;
		}
		foreach(KeyValuePair<int,int> item in items)
		{
			bool found = false;
			foreach(KeyValuePair<int, int> itemsR in ItemsRequired)
			{
				if(itemsR.Key == item.Key && item.Value >= itemsR.Value)
				{
					found = true;
					break;
				}
			}

			if(!found)
			{
				return nothing;
			}
		}

		return Crafted.First();
	}
}
