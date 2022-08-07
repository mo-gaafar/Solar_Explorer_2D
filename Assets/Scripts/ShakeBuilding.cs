using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBuilding : MonoBehaviour {
    float Frequency = 0f;
    float Amplitude = 0f;
    bool shake = false;
    Vector3 OriginalPos;
    private void Start () {
        Health health = gameObject.GetComponent<Health> ();
        health.onHit.AddListener ((float damage) => {
            Shake ();
        });
        OriginalPos = transform.position;
    }

    public void Shake () {
        shake = true;
        //Shake x and y
        Frequency += 20f;
        Amplitude += 0.003f;
    }
    //TODO event onhit trigger coroutine?
    private void Update () {
        if (shake) {
            float Xval = transform.position.x + Amplitude * Mathf.Sin (Frequency * Time.time);
            float Yval = transform.position.y + Amplitude * Mathf.Sin (Frequency * Time.time);

            transform.position = new Vector2 (Xval, Yval);
        }
        //decrease Amp and frequency 
        Amplitude = Mathf.Max (0, Amplitude - Time.deltaTime / 100);
        Frequency = Mathf.Max (0, Frequency - Time.deltaTime);
        if (Amplitude == 0 || Frequency == 0) {
            shake = false;
            Amplitude = 0;
            Frequency = 0;
            transform.position = OriginalPos;
        }
    }
}