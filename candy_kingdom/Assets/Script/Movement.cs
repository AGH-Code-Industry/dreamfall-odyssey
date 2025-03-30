using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D body;
    private bool grounded;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private float Move;
    public Animator anim;
    public bool isFacingRight;

    private void Awake()
    {
        isFacingRight = true;
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        Move = Input.GetAxis("Horizontal");


        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
            
        if(Mathf.Abs(Move) >= 0.1f || Mathf.Abs(Move) <= -0.1f)
        {
            anim.SetBool("isRunning", true); 
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

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
        }
    }

    private void Flip()
    {
        Debug.Log("Flipping character...");
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x = localScale.x  * (-1f);
        transform.localScale = localScale;
    }
}
