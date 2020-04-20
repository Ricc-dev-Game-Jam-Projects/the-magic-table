using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List<Slot> SlotGrid;
    public GameObject SlotPrefab;

    public float xStart;
    public float yStart;
    public float OffSet;

    public int Size;

    private GridController controller;

    private void Awake()
    {
        controller = new GridController();
        SlotGrid = new List<Slot>();
        GenerateGrid();

        ItemController itemController = new ItemController();

        //Item i = itemController.Index(7);
        //Item stick = itemController.Index("Wood Stick");

        //SlotData d1 = new SlotData();
        //d1.SetData(i, 1, i.Integrity, i.LifeTime);
        //SlotData d2 = new SlotData();
        //d2.SetData(stick, 5, stick.Integrity, stick.LifeTime);

        //SlotGrid[0].SwapItem(d1);
        //SlotGrid[1].SwapItem(d2);
    }

    void GenerateGrid()
    {
        controller.Create(Size);

        DeactivateAllSlots();

        int cont = 0;
        bool Any = false;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                GameObject go = null;

                if (!Any)
                {
                    foreach(Slot s in SlotGrid)
                    {
                        if (!s.gameObject.activeInHierarchy)
                        {
                            go = s.gameObject;
                            go.SetActive(true);
                            break;
                        }
                    }
                }


                if (go == null)
                {
                    Any = true;
                    go = Instantiate(SlotPrefab);
                    go.name = "Slot" + cont;
                    SlotGrid.Add(go.GetComponent<Slot>());
                    Finger f = Finger.finger;
                    if (!f.Slots.Contains(go.GetComponent<Slot>()))
                    {
                        f.Slots.Add(go.GetComponent<Slot>());
                    }
                }
                go.transform.position = new Vector3(xStart + i * OffSet, yStart + j * OffSet, 0f);
                cont++;
            }
        }
    }

    public void DeactivateAllSlots()
    {
        foreach(Slot s in SlotGrid)
        {
            s.gameObject.SetActive(false);
        }
    }

    public void IncrementSize()
    {
        Size++;
        GenerateGrid();
    }
}
