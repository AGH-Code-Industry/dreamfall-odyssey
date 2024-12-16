using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

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
    
    [SerializeField] private LayerMask hazard;
    [SerializeField] private GameObject dead;
    private bool jumpBuffer = false;
    private bool lockChar = false;
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
        if(Physics2D.OverlapBox(transform.position, Vector2.one,0f,hazard))
        {
            rb.freezeRotation = false;
            rb.linearVelocityY = 20f;
            rb.linearVelocityX = Random.Range(-20f,20f);
            rb.angularVelocity = Random.Range(-200f,200f);
            lockChar = true;
            dead.SetActive(true);
        }

        horizontal = Input.GetAxisRaw("Horizontal"); 

        if(jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        else
            jumpBuffer = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
            jumpBuffer = true;
        }

        if(rb.linearVelocityY < 0.02f && !IsGrounded() && airtime >0)
        {
            airtime -= Time.deltaTime;
            rb.linearVelocityY = 0.01f;
            jumpBufferTime = 0f;
        }

        if(jumpBuffer && IsGrounded() && !lockChar)
        {
            rb.linearVelocityY = jumpStrenght;
            airtime = time_Apex;
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0f)
        {
            rb.linearVelocityY = rb.linearVelocityY*0.5f;
        }
    }

    private void FixedUpdate()
    {
        if(!lockChar)
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
