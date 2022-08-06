using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin CBMP;
    float shakertimer = 1;
    float varyingdecrement = 0;
    int MaxAmplitude = 5;
    private void Awake () {
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera> ();
        CBMP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
    }

    public void Shake () {
        CBMP.m_AmplitudeGain += 1.5f;
        // Debug.Log("Amp is " + CBMP.m_AmplitudeGain);
        if (CBMP.m_AmplitudeGain >= MaxAmplitude) {
            CBMP.m_AmplitudeGain = MaxAmplitude;
        }
        shakertimer = Mathf.Min (7, shakertimer + 1);
        varyingdecrement = 0;
    }
    //at any point 
    // at shakertime=0 ->amp =0
    // at shakertime=3 -> amp =maxamp

    private void Update () {
        if (shakertimer > 0) {
            shakertimer -= Time.deltaTime;
            CBMP.m_AmplitudeGain -= varyingdecrement + CBMP.m_AmplitudeGain * Time.deltaTime / shakertimer;
            if (CBMP.m_AmplitudeGain <= 0) {
                CBMP.m_AmplitudeGain = 0;
            }
            // Debug.Log("Amp is " + CBMP.m_AmplitudeGain);
        }
        //if(shakertimer == 0)
        //{
        //    CBMP.m_AmplitudeGain = 0;
        //}
        varyingdecrement = Mathf.Max (0.0001f, Time.deltaTime);
    }
}