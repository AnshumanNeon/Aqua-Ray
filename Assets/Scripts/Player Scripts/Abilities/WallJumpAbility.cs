using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpAbility : MonoBehaviour
{
    public float wallJumpSpeed = 10f, distance = .5f, wallSlideSpeed;
    public LayerMask groundLayer;
    public bool onWall = false;
    public Rigidbody2D rb;

    public Vector2 direction;

    DoubleJumpAbility doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        doubleJump = GetComponent<DoubleJumpAbility>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onWall = Physics2D.Raycast(transform.position, transform.right, distance, groundLayer) || Physics2D.Raycast(transform.position, -transform.right, distance, groundLayer);

        if(onWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    public void WallJump()
    {
        rb.AddForce(Vector2.right * -direction.x * wallJumpSpeed, ForceMode2D.Impulse);
        doubleJump.ResetJumpCount();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + distance, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - distance, transform.position.y, transform.position.z));
    }
}
