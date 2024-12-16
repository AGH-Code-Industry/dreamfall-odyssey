using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.IK;

public class player_ctrler : MonoBehaviour
{

    private Collider2D cc;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float time_Apex = 1.0f;
    
    [SerializeField] private float time_Coyote = 1.0f;
    
    private float horizontal;
    public float jumpStrenght = 16f;
    private bool facingRight = true;
    private bool onGround;
    private float airtime;
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask ground;
    private bool jumpBuffer = false;
    public float jumpBufferTime = 0.1f;
    private float jumpBufferTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        airtime = time_Apex;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); 


        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics2D.OverlapCircle(feet.position, 2f, ground))
            {
                jumpBufferTimer = jumpBufferTime;
            }
        }
        if(jumpBufferTimer > 0)
        {
            jumpBuffer = true;
            jumpBufferTimer -= Time.deltaTime;
        }
        else
            jumpBuffer = false;


        if(rb.linearVelocityY < 0.02f && !IsGrounded() && airtime >0)
        {
            airtime -= Time.deltaTime;
            rb.linearVelocityY = 0.01f;
            jumpBufferTime = 0f;
        }

        if(Input.GetKeyUp(KeyCode.Space)&& IsGrounded())
        {
            rb.linearVelocityY = jumpStrenght;
            airtime = time_Apex;
        }
        if(jumpBuffer && IsGrounded())
        {
            rb.linearVelocityY = rb.linearVelocityY*0.5f;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, 0.2f, ground);
    }

    private void Flip()
    {
        if(facingRight && horizontal <0f || !facingRight && horizontal >0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
