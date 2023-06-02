using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSwitcher : MonoBehaviour
{
    public Lock lock1, lock2;
    public Color mainColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lock1.sprite.color = mainColor;
        lock2.sprite.color = mainColor;
    }

    public void Switch()
    {
        lock1.Switch();
        lock2.Switch();
    }
}
