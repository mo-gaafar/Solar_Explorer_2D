using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum ObjectiveType { Enemy, Collection}

public class Objective : MonoBehaviour
{
    public ObjectiveType Type;

    public bool Active = false;
    public int requiredCount=5;
    public int CurrentCount = 0;
    public bool CheckComplete()
    {
        if(CurrentCount < requiredCount)
        {
            return false;
        }
        Active = false;
        return true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Active = true;
        //Add Complex behaviour later if you want (like the objective stops when afar from the colliders 
    }
}
