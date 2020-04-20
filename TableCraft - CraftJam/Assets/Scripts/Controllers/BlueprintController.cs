using System.Collections.Generic;

public class BlueprintController
{
    public static List<Blueprint> Blueprints { get; private set; } = new List<Blueprint>();

    private string BlueprintPath = "Data/Blueprints/";
    private DataReader reader = new DataReader(false);

    public BlueprintController()
    {
        GetData();
    }

    public Blueprint[] Index()
    {
        GetData();
        return Blueprints.ToArray();
    }

    public Blueprint Index(int ID)
    {
        GetData();
        return Blueprints.Find(x => x.ID == ID);
    }

    public void Create(Blueprint blueprint)
    {
        Blueprints.Add(blueprint);
    }

    private void GetData()
    {
        return;
        if (Blueprints.Count != 0)
        {
            return;
        }
        Blueprint[] blueprints = reader.GetAllObjectFromJson<Blueprint>(BlueprintPath);
        if (blueprints != null)
        {
            Blueprints.AddRange(blueprints);
        }
    }
}