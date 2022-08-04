using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin CBMP;
    float shakertimer;
    private void Awake()
    {
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        CBMP= cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        CBMP.m_AmplitudeGain += 2;
        Debug.Log("Amp is " + CBMP.m_AmplitudeGain);
        if (CBMP.m_AmplitudeGain >= 12){
            CBMP.m_AmplitudeGain = 12;
        }
        shakertimer =3;
    }

    private void Update()
    {
        if (shakertimer > 0)
        {
            shakertimer -= Time.deltaTime;
            CBMP.m_AmplitudeGain -= Time.deltaTime;
            if (CBMP.m_AmplitudeGain <= 0)
            {
                CBMP.m_AmplitudeGain = 0;
            }
            Debug.Log("Amp is " + CBMP.m_AmplitudeGain);
        }
        //if(shakertimer == 0)
        //{
        //    CBMP.m_AmplitudeGain = 0;
        //}
    }
}
