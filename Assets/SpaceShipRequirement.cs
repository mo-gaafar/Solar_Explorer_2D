using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipRequirement : MonoBehaviour
{
    [SerializeField]Collider2D collider;
    bool FuelCheck;
    bool Key;

    void ActivateCollider()
    {
        if(FuelCheck && Key)
        {
            collider.enabled = true;
        }
    }

    public void SetFuelCheck(bool value)
    {
        FuelCheck=value;
        ActivateCollider();
    }

    public void SetKey(bool value)
    {
        Key = value;
        ActivateCollider();
    }
}
