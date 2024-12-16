using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D.IK;

public class enemy_ctrl : MonoBehaviour
{

    private Collider2D cc;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float wallCheck = 2.0f;
    [SerializeField] private float time_Apex = 1.0f;
    
    [SerializeField] private float time_Coyote = 1.0f;
    
    private float horizontal;
    public float jumpStrenght = 16f;
    private bool facingRight = true;
    private bool onGround;
    private float airtime;
    [SerializeField] private Transform feet;
    [SerializeField] private Transform tgt_1;
    [SerializeField] private Transform tgt_2;
    private Transform current_track;
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
        current_track = tgt_1;
    }

    // Update is called once per frame
    void Update()
    {
        float orientation = Mathf.Abs(current_track.position.x - transform.position.x);
        if(orientation < 1f)
        {
            if(current_track == tgt_1)
            {
                current_track = tgt_2;
            }
            else
            {
                current_track = tgt_1;
            }

            //Debug.Log("SWITCHING");
            
        }
        horizontal = Mathf.Sign(-transform.position.x + current_track.position.x);
        
        //Debug.Log(Mathf.Abs(current_track.position.x - transform.position.x));

        if(Physics2D.Raycast(transform.position, Vector2.left * orientation, wallCheck, ground) && IsGrounded())
        {
            jumpBufferTimer = jumpBufferTime;
            rb.linearVelocityY = jumpStrenght;
        }

        if(rb.linearVelocityY < 0.02f && !IsGrounded() && airtime >0)
        {
            airtime -= Time.deltaTime;
            rb.linearVelocityY = 0.01f;
            jumpBufferTime = 0f;
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
