using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public SpriteRenderer sprite;
    BoxCollider2D coll;
    bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .5f);
        }
        else
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }
    }

    public void Switch()
    {
        coll.enabled = !coll.enabled;
        isEnabled = !isEnabled;
    }
}
