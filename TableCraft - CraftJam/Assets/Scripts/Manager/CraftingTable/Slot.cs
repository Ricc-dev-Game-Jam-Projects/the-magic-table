using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer circleRenderer;
    public TextMeshProUGUI ItemQuantity;
    public SlotData Data;
    public Sprite[] CircleSprites;
    public UnityAction SlotSwaped;
    public ItemController itemController;

    public int CircleCount {
        get {
            return CircleSprites.Length;
        }
    }
    public Item item {
        get {
            return Data?.Item;
        }
    }
    public int Quantity {
        get {
            return Data.Quantity;
        }
    }

    public bool Positionable = true;
    private string path = "Images/";

    private void Start()
    {
        if(Data == null)
        {
            Data = new SlotData();
        }
        itemController = new ItemController();

        circleRenderer.sprite = null;

        LoadItem();
    }

    public void LoadItemFromDatabase(int id, int Quantity = 1)
    {
        Item t = itemController.Index(id);
        Data.SetData(t, Quantity, t.Integrity, t.LifeTime);
        if(item != null)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(path + item.ImagePath);
        } else
        {
            Debug.Log("Item null");
        }
        ItemQuantity.text = this.Quantity.ToString();
    }

    public void TickCircle(int index)
    {
        if(index == -1)
        {
            circleRenderer.sprite = null;
            return;
        }

        circleRenderer.sprite = CircleSprites[index >= CircleCount ? CircleCount - 1 : index];
    }

    public void LoadItem()
    {
        if(item == null)
        {
            DisposeData();
        } else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(path + item.ImagePath);
            ItemQuantity.text = Quantity.ToString();
        }
    }

    public void SwapItem(SlotData slotData)
    {
        Data = new SlotData(slotData);
        LoadItem();
        SlotSwaped?.Invoke();
    }

    public bool RemoveItem(int Quantity = 1)
    {
        if(!Data.Decrement(Quantity))
        {
            DisposeData();
            return true;
        }
        LoadItem();
        return false;
    }

    public bool Free()
    {
        return Data.Item == null;
    }

    public void DisposeData()
    {
        spriteRenderer.sprite = null;
        circleRenderer.sprite = null;
        ItemQuantity.text = "";
        Data = new SlotData();
    }
}
