public class Tool : Item
{
    public float TimeToProduce;
    public int MaterialID;

    public Tool(int ID, string Name, float TimeToProduce, int MaterialID, bool Craftable = true, bool Permanent = false, float LifeTime = -1f, int Integrity = -1) : 
                base(ID, Name, Craftable, Permanent, LifeTime, Integrity)
    {
        this.LifeTime = -1f;
        this.TimeToProduce = TimeToProduce;
        this.MaterialID = MaterialID;
        this.Permanent = false;
    }
}
