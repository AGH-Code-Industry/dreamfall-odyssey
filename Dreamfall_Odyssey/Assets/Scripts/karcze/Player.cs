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
    private bool isAttacking;

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
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(3, 3, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-3, 3, 1);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            jumpCount = 0;
            anim.SetBool("isJumping", false);
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
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
            anim.SetBool("isJumping", false);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isAttacking", true);
        }

        if (Input.GetMouseButton(1))
        {
            Block();
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

        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("Hit enemy!");

            SkeletonHealth skeletonHealth = enemy.GetComponent<SkeletonHealth>();
            if (skeletonHealth != null)
            {
                skeletonHealth.TakeDamage(damage);
                Debug.Log("Damage dealt to skeleton!");
            }

            NecromancerHealth necromancerHealth = enemy.GetComponent<NecromancerHealth>();
            if (necromancerHealth != null)
            {
                necromancerHealth.TakeDamage(damage);
                Debug.Log("Damage dealt to necromancer!");
            }
        }

    }

    public void Block()
    {
        anim.SetBool("isBlocking", true);
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.canTakeDamage = false;
    }

    public void EndBlock()
    {
        anim.SetBool("isBlocking", false);
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.canTakeDamage = true;
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
