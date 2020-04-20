using UnityEngine;

[RequireComponent(typeof(Slot))]
public class Trash : MonoBehaviour
{
    public Slot MySlot;

    public float TimeToDestroy = 2f;

    private float timeAt = 0f;
    
    void Start()
    {
        MySlot = GetComponent<Slot>();
        MySlot.Positionable = true;
    }

    void Update()
    {
        if(MySlot.Data != null)
        {
            timeAt += Time.deltaTime;
            if (timeAt >= TimeToDestroy)
            {
                MySlot.RemoveItem();
                timeAt = 0f;
            }
        
        } else
        {
            timeAt = 0f;
        }
    }
}
