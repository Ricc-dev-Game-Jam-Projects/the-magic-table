using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public bool GenerateData;
    public bool Done = false;

    private string DataPath = "Data\\";
    private string ItemPath;
    private string BlueprintPath;

    private DataReader dataReader = new DataReader(false);


    void Start()
    {
        if(!GenerateData)
        {
            Done = true;
            return;
        }

        ItemPath = DataPath + "Items\\";
        BlueprintPath = DataPath + "Blueprints\\";

        ItemController itemController = new ItemController();
        BlueprintController blueprintController = new BlueprintController();

        Item woodenLog = new Item(0, "Wooden Log", false, false, 30f)
        {
            ImagePath = "WoodenLog",
        };

        Item woodStick = new Item(1, "Wood Stick", true, false, 60f)
        {
            ImagePath = "WoodStick",
        };

        Item stoneAxe = new Tool(2, "Stone Hammer", Craftable: true, Permanent: false, Integrity: 100, TimeToProduce: 3f, MaterialID: 3)
        {
            ImagePath = "WoodenAxe",
        };

        Item stone = new Item(3, "Stone", Craftable: false, Permanent: false, LifeTime: 130f)
        {
            ImagePath = "Stone",
        };

        Item refinedStone = new Item(4, "Refined Stone", Craftable: true, Permanent: true)
        {
            ImagePath = "RefinedStone",
        };

        Item barricade = new Item(5, "Barricade", Craftable: true, Permanent: true)
        {
            ImagePath = "Nothing",
        };

        Item woodenPlank = new Item(6, "Wooden Plank", Craftable: true, LifeTime: 280)
        {
            ImagePath = "WoodPlank",
        };

        Item craftingTable = new Item(7, "Crafting Table", Craftable: true, Permanent: true)
        {
            ImagePath = "CraftingTable",
        };

        Item woodenHammer = new Tool(8, "Wooden Hammer", Craftable: true, Permanent: false, Integrity: 50, TimeToProduce: 5f, MaterialID: 3)
        {
            ImagePath = "WoodenHammer"
        };

        Blueprint BstoneAxe = new Blueprint(0, 
                                    new Dictionary<int, int> { { woodStick.ID, 3 }, { refinedStone.ID, 1 } }, 
                                    new Dictionary<int, int>() { { stoneAxe.ID, 1 } });
        
        Blueprint BwoodStick = new Blueprint(1, new Dictionary<int, int> { { woodenLog.ID, 5 } },
                                    new Dictionary<int, int>() { { woodStick.ID, 5 } });

        Blueprint BrefinedStone = new Blueprint(2, new Dictionary<int, int> { { stone.ID, 1 } },
                                    new Dictionary<int, int>() { { refinedStone.ID, 1 } });
        
        Blueprint Bbarricade = new Blueprint(3, new Dictionary<int, int> { { woodenPlank.ID, 5 } },
                                    new Dictionary<int, int>() { { barricade.ID, 1 } });
        
        Blueprint BwoodenPlank = new Blueprint(4, new Dictionary<int, int> { { woodenLog.ID, 1 }, { stoneAxe.ID, 1 } },
                                    new Dictionary<int, int>() { { woodenPlank.ID, 5 } });

        Blueprint BcraftingTable = new Blueprint(5, new Dictionary<int, int> { { woodStick.ID, 4 }, { woodenPlank.ID, 5 } },
                                    new Dictionary<int, int>() { { craftingTable.ID, 1 } });

        Blueprint BwoodenHammer = new Blueprint(6, new Dictionary<int, int> { { woodStick.ID, 3 }, { woodenLog.ID, 1 } },
                                            new Dictionary<int, int>() { { woodenHammer.ID, 1 } });


        itemController.Create(woodenLog);
        itemController.Create(woodStick);
        itemController.Create(stoneAxe);
        itemController.Create(stone);
        itemController.Create(refinedStone);
        itemController.Create(barricade);
        itemController.Create(woodenPlank);
        itemController.Create(craftingTable);
        itemController.Create(woodenHammer);

        blueprintController.Create(BwoodStick);
        blueprintController.Create(BstoneAxe);
        blueprintController.Create(BrefinedStone);
        blueprintController.Create(Bbarricade);
        blueprintController.Create(BwoodenPlank);
        blueprintController.Create(BcraftingTable);
        blueprintController.Create(BwoodenHammer);



        //foreach (object item in itemController.Index())
        //{
        //    dataReader.CreateJsonFromObject(ItemPath + (item as Item).ID, item);
        //}

        //foreach (Blueprint blueprint in blueprintController.Index())
        //{
        //    dataReader.CreateJsonFromObject(BlueprintPath + blueprint.ID, blueprint);
        //}

        Done = true;
    }
}