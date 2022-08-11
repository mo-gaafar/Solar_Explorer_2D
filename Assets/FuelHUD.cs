using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelHUD : MonoBehaviour
{
    [SerializeField] Slider slider ;
    [SerializeField]float MaxFuelValue = 1000;


    //private void Start()
    //{
    //    slider.maxValue = 1000;
    //    slider.value = 0;
    //}

    public void ChangeValue(float value)
    {
        float frac= value/ MaxFuelValue;
        slider.value += frac;
    }
    public void ChangeMaxValue(float MaxValue)
    {
        slider.maxValue = MaxValue;
    }
}
