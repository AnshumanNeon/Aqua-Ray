using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    public float accel = 13, deccel = 16, velPower = .96f, frictionAmount = .22f, maxSpeed = 7f;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpCutMultiplier;
    public float jumpCoyoteTime, jumpBufferTime;
    public float gravity, fallGravityMultiplier;
    public float fallClamp = -20f;

    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public GameObject characterHolder;
    public DialougeUI dialougeUI;
    public Interactable interactable { get; set; }
    
    [Header("Physics")]
    public float linearDrag = 4f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundCheckRadius;
    public Transform groundCheck;
    float lastGroundTimer, lastJumpTimer;
    bool facingRight = true;
    bool isJumping;
    Controls controls;

    void Awake()
    {
        controls = new Controls();
        
        controls.Player.JumpDown.performed += args => OnJump(args);
        controls.Player.JumpUp.performed += args => OnJumpUp(args);
    }

    void OnJumpUp(InputAction.CallbackContext args)
    {
        if(rb.velocity.y > 0 && isJumping)
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }

        lastJumpTimer = 0;
    }

    void OnJump(InputAction.CallbackContext args)
    {
        lastJumpTimer = jumpBufferTime;
    }

    void OnEnable()
	{
		controls.Player.Enable();
	}

    void OnDisable()
	{
		controls.Player.Disable();
	}

    // Update is called once per frame
    void Update()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(!wasOnGround && onGround)
        {
            StartCoroutine(SqueezeJump(1.25f, .8f, .05f));
        }

        if(!isJumping && onGround) lastGroundTimer = jumpCoyoteTime;

        animator.SetBool("onGround", onGround);

        lastGroundTimer -= Time.deltaTime;
        lastJumpTimer -= Time.deltaTime;

        if(dialougeUI != null)
        {
            if(dialougeUI.IsOpen)
            {
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            interactable?.Interact(this);
        }

        if(isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
        }

        if(lastJumpTimer > 0 && lastGroundTimer > 0 && !isJumping)
        {
            Jump();
            animator.SetTrigger("jump");
        }

        direction = controls.Player.Movement.ReadValue<Vector2>();

        if(direction.x != 0 && onGround)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    void FixedUpdate()
    {
        moveCharacter();

        modifyPhysics();
    }

    void moveCharacter()
    {
        float targetSpeed = direction.x * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > .01f) ? accel : deccel;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            facingRight = !facingRight;

            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.AddForce(Vector2.right * Mathf.Sign(rb.velocity.x) * deccel * Time.deltaTime);
        }

        if(onGround && direction.x == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if(onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                rb.drag = linearDrag * 0.25f;
            }
        }
        else
        {
            rb.drag = linearDrag * 0.1f;
        }

        if(rb.velocity.y < 0)
        {
            rb.gravityScale = gravity * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravity;
        }

        if(rb.velocity.y < fallClamp)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallClamp);
        }

        if(Physics.Raycast(transform.position, Vector3.right, .5f))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Jump()
    {
        lastGroundTimer = 0;
        lastJumpTimer = 0;
        isJumping = true;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        
        StartCoroutine(SqueezeJump(.8f, 1.2f, .1f));
    }

    IEnumerator SqueezeJump(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);

        float t = 0f;
        while(t <= 1f)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }

        t = 0f;
        while(t <= 1)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}