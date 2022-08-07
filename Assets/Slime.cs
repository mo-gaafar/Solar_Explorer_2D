using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]SpriteRenderer Sr;
    float Timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer+=Time.deltaTime;
        //Fade
        Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, Timer);
        if (Mathf.Abs(1 - Timer) <= 0.02)
        {
            Destroy(gameObject);
        }

    }

    
}
