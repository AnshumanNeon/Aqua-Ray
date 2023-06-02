using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            CamFrameChange.frameChange.onChangeFrame(transform.GetChild(0).gameObject);
        }
    }
}
