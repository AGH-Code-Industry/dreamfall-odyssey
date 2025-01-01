using UnityEngine;



public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public int damage = 10;

    public int maxJumps = 1;
    private int jumpCount;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttacking;

    //attack
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemiesLayer;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private PlayerHealth PlayerHealth;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log($"MaxJumps: {maxJumps}, JumpCount: {jumpCount}");
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(3, 3, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-3, 3, 1);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            // Reset licznika skok�w, je�li posta� dotyka ziemi
            jumpCount = 0;
            isJumping = false;
            anim.SetBool("isJumping", false);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            isJumping = true;
            anim.SetBool("isJumping", true);
        }

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }


        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0);
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isAttacking", true);
        }

        anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0);
    }

    public void EndAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    public void EndJump()
    {
        anim.SetBool("isJumping", false);
    }
    
    public void attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemiesLayer);

        // Iterowanie po wykrytych obiektach
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("Hit enemy!");

            // Sprawdzanie, czy obiekt ma komponent SkeletonHealth
            SkeletonHealth skeletonHealth = enemy.GetComponent<SkeletonHealth>();
            if (skeletonHealth != null)
            {
                // Zadanie obra�e� przeciwnikowi
                skeletonHealth.TakeDamage(damage);
                Debug.Log("Damage dealt to enemy!");
            }
        }

    }

    public void doubleJump()
    {
        maxJumps = 2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstantDeath"))
        {
            PlayerHealth.Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
        Gizmos.color = Color.red;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
