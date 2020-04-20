using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Slot))]
public class FreeWood : MonoBehaviour
{
    public float TimeAt = 0f;
    public float TimeTo = 5f;

    public int MaterialID = 0;

    public Slot MySlot;
    public TextMeshProUGUI CooldownText;

    protected string PrefixText;
    protected string DoneText;

    private void Awake()
    {
        MySlot = GetComponent<Slot>();
    }

    private void Start()
    {
        _Start();
    }

    private void Update()
    {
        FreeRawMaterial(MaterialID);
    }

    public void FreeRawMaterial(int ID)
    {
        FreeRawMaterial(ID, MySlot);
    }

    public void FreeRawMaterial(int ID, Slot mySlot)
    {
        int time = 0;
        if (mySlot.item == null)
        {
            TimeAt += Time.deltaTime;

            time = (int)Mathf.Clamp((TimeTo - TimeAt), 0f, 9999f);

            if (TimeAt >= TimeTo)
            {
                mySlot.LoadItemFromDatabase(ID);
                TimeAt = 0f;
            }
        }
        CooldownText.text = PrefixText + "\n" + (time > 0 ? time + "s" : DoneText);
    }

    protected virtual void _Start()
    {
        DoneText = "Get it!";
        PrefixText = "Free Wood...";
    }
}
