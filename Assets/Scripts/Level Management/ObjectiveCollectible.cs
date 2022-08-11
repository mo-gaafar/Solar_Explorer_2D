using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectiveCollectible : MonoBehaviour
{
    public UnityEvent <float> onPickup;
    [SerializeField] float value=100;
    void Start()
    {
        //if (gameObject.tag == "Fuel")
        //{
        //    onPickup.AddListener();
        //}
        //if(gameObject.tag == "StarShipKey")
        //{

        //    onPickup.AddListener();
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        { 
            onPickup.Invoke(value);
            Destroy(gameObject);
        }
        
    }
}
