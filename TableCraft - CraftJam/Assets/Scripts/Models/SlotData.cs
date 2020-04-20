using System;

public class SlotData
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }
	public int Integrity { get; set; }
    
	public float LifeTime { get; set; }

    public SlotData(SlotData slotData)
    {
        Item = slotData.Item;
        Quantity = slotData.Quantity;
		LifeTime = slotData.LifeTime;
		Integrity = slotData.Integrity;
    }

    public SlotData()
    {
        Quantity = 0;
    }

	public bool Decrement(int Amount = 1)
	{
		Quantity -= Amount;
		if(Quantity <= 0)
		{
			return false;
		}
		return true;
	}

	public void SetData(Item item, int Quantity, int integrity, float lifeTime)
	{
		Item = item;
		this.Quantity = Quantity;
		if(item != null)
		{
			LifeTime = lifeTime;
			Integrity = integrity;
		} else
		{
			LifeTime = -1f;
			Integrity = -1;
		}
	}

	public bool StackLife(float Amount)
	{
		if (!Item.Permanent)
		{
			LifeTime -= Amount;
			if (LifeTime <= 0)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Remove a amount of integrity from the item.
	/// </summary>
	/// <param name="Amount">Amount to stack the integrity value.</param>
	/// <returns>True if it reaches 0, false if still has integrity</returns>
	public bool StackIntegrity(int Amount)
	{
		if (!Item.Permanent && Integrity >= 0)
		{
			Integrity -= Amount;
			if (Integrity <= 0)
			{
				Integrity = 0;
				return true;
			} else
			{
				return false;
			}
		}
		return true;
	}
}

