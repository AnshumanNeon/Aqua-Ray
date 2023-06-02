using System;
using System.Collections.Generic;
using UnityEngine;

public class CamFrameChange : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    public static CamFrameChange frameChange;
    public event Action<GameObject> changeFrame;
    public void onChangeFrame(GameObject obj)
    {
        changeFrame.Invoke(obj);
    }

    // Start is called before the first frame update
    void Awake()
    {
        frameChange = this;

        changeFrame += args => ChangeCamera(args);
    }

    void ChangeCamera(GameObject cam)
    {
        if(cameras.Contains(cam))
        {
            foreach(GameObject camera in cameras)
            {
                camera.SetActive(false);
            }

            cam.SetActive(true);
        }
    }
}
