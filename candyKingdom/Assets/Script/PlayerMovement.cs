using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private bool grounded;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private float Move;
    public Animator anim;
    public bool isFacingRight;

    public float KBForce; //KnockBack
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    private bool isWallSliding;
    private float wallSlidingSpeed = 3f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallJumping;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private float wallJumpingDirection;
    private Vector2 wallJumpingPower = new Vector2(5f, 8f);
    private bool canWallJump = true;

    public Vector3 lastCheckpointPosition;

    private void Awake()
    {
        isFacingRight = true;
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y <= -12)
        {
            StartCoroutine(Death());
            return; 
        }

        if (KBCounter <= 0)
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
            Move = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                Jump();
            }

            if (Mathf.Abs(Move) >= 0.1f || Mathf.Abs(Move) <= -0.1f)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            if(KnockFromRight == true)
            {
                body.linearVelocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                body.linearVelocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }

        WallSlide();
        WallJump();

        if (!isFacingRight && Move > 0)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0)
        {
            Flip();
        }

    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        grounded = false;
        anim.SetBool("isJumping", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetBool("isJumping", false);

            canWallJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            lastCheckpointPosition = transform.position;

        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = localScale.x  * (-1f); 
        transform.localScale = localScale;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        bool isMovingLeftOrRight = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;  

        if (IsWalled()  && isMovingLeftOrRight)  
        {
            isWallSliding = true;
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, -wallSlidingSpeed);  
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            if (Input.GetKey(KeyCode.Space) && wallJumpingCounter > 0f && canWallJump)
            {
                wallJumpingDirection = -transform.localScale.x;
                body.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                canWallJump = false;
                wallJumpingCounter = 0f;
                StartCoroutine(ResetWallJump());
            }
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
    }

    private IEnumerator ResetWallJump()
    {
        yield return new WaitForSeconds(wallJumpingDuration);
        canWallJump = true;
    }

    private IEnumerator Death()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        //Destroy(gameObject);
        transform.position = lastCheckpointPosition;
        anim.SetBool("isDead", false);
    }

}
