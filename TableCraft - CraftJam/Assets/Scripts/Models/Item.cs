using System;

public class Item
{
	public int ID { get; protected set; }

	public string Name { get; protected set; }

	public bool Craftable { get; protected set; }
	public bool Permanent { get; protected set; }

	public float LifeTime { get; protected set; }

	public int Integrity { get; protected set; }

	public string ImagePath = "Nothing.png";

	public Item(int ID, string Name, bool Craftable = true, bool Permanent = false, float LifeTime = -1f, int Integrity = -1)
	{
		this.ID = ID;
		this.Name = Name;
		this.Craftable = Craftable;
		this.Permanent = Permanent;
		this.LifeTime = LifeTime;
		this.Integrity = Integrity;
	}
}