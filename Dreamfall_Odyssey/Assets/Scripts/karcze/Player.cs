using UnityEngine;



public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float damage;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isAttacking;

    //attack
    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;

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

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            //isJumping = true;
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
        //else if (rb.linearVelocity.y < 0) // Opadanie
        //{
        //anim.SetBool("isJumping", true);
        //}

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
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        foreach(Collider2D enemyGameobject in enemy)
        {
            Debug.Log("Hit enemy");
            enemyGameobject.GetComponent<SkeletonsHealth>().skeletonHealth -= damage;
        }
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
