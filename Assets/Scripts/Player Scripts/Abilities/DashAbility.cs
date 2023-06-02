using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public KeyCode dashKey;
    public PlayerMovement movement;
    public float dashSpeed, dashCooldown;

    Rigidbody2D rb;

    bool canDash, isDashing;

    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(dashKey) && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dir = movement.direction;
            StartCoroutine(StopDashing());
        }

        if(isDashing)
        {
            Dash();
        }

        if(movement.onGround)
        {
            canDash = true;
        }
    }

    void Dash()
    {
        if(dir.x < 0)
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        else if(dir.x > 0)
        {
            rb.velocity = Vector2.right * dashSpeed;
        }

        if(dir.y < 0)
        {
            rb.velocity = Vector2.down * dashSpeed;
        }
        else if(dir.y > 0)
        {
            rb.velocity = Vector2.up * dashSpeed;
        }

        return;
    }

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        trailRenderer.emitting = false;
    }
}
