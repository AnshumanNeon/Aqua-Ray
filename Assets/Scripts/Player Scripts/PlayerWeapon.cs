using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon instance;
    public KeyCode attackButton;
    public Animator animator;

    [HideInInspector] public bool isAttacking;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(attackButton))
        {
            isAttacking = true;
        }
    }
}
