using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum ObjectiveType { Enemy, Fuel, StarshipKey}

public class Objective : MonoBehaviour
{
    public ObjectiveType Type;
    public UnityEvent onObjectiveStart;
    public UnityEvent onObjectiveEnd;
    public bool Active = false;
    public float requiredCount=5;
    public float CurrentCount = 0;
    public bool CheckComplete()
    {
        if(!Active ) return false;

        if(CurrentCount < requiredCount)
        {
            return false;
        }
        Active = false;
        onObjectiveEnd.Invoke();
        return true;
        
    }
    public void SetCurrentCount(float value)
    {
        CurrentCount = value;
    }

    public void SetActive(bool value)
    {
        Active = value;
    }

    //public void SetComplete(bool value)
    //{
    //    Active = value;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Active)
        {
            Active = true;
            onObjectiveStart.Invoke();

        }
        //Add Complex behaviour later if you want (like the objective stops when afar from the colliders 
    }


    private void OnDestroy()
    {
        onObjectiveEnd.Invoke();
    }
}
