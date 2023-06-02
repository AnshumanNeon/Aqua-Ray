using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float range;

    Material mat;

    float fadeAmount;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= range)
        {
            if(fadeAmount < 1)
            {
                fadeAmount = Mathf.Clamp01(fadeAmount + Time.deltaTime);
                mat.SetFloat("_Fade", fadeAmount);
            }
        }
        else
        {
            if(fadeAmount > 0)
            {
                fadeAmount = Mathf.Clamp01(fadeAmount - Time.deltaTime);
                mat.SetFloat("_Fade", fadeAmount);
            }
        }
    }
}
