using UnityEngine;
public class playerMove : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private bool facingRight = true;
    private PlayerData data;
    private int remainingJumps;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        data = GetComponent<PlayerData>();
        rb.gravityScale = data.gravityScale;
        remainingJumps = data.jumpCount;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        UpdateAnimator();

        if (movement.x > 0 && !facingRight)
            Flip();
        else if (movement.x < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, data.jumpForce);
            remainingJumps--;

            if (remainingJumps == 1)
            {
                animator.ResetTrigger("doubleJump");
                animator.SetTrigger("jump");
            }
            else if (remainingJumps == 0)
            {
                animator.ResetTrigger("jump");
                animator.SetTrigger("doubleJump");
            }
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isRunning", movement.x != 0);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("velocityY", rb.linearVelocity.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * data.speed * Time.fixedDeltaTime * 100f, rb.linearVelocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        sr.flipX = !sr.flipX;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            remainingJumps = data.jumpCount;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}