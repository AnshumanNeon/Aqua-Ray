using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    float shakeTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = intensity;

        shakeTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if(shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin channelPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                channelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
}
