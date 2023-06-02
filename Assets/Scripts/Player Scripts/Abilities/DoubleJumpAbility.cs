using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : MonoBehaviour
{
    public PlayerMovement movement;
    public int maxJumps = 2;
    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        jumpCount = maxJumps - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void Jump()
    {
        jumpCount -= 1;
    }

    public void ResetJumpCount()
    {
        jumpCount = maxJumps - 1;
    }

    public bool CanJump()
    {
        return jumpCount >= 0;
    }
}
