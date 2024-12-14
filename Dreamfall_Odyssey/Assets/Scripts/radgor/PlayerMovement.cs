using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public float acceleration;
    public float groundSpeed;
    public float jumpSpeed;
    // [Range(0f, 1f)]
    // public float groundDecay;
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public bool grounded;
    public Vector2 startingPosition = new Vector2(-2.0f, -0.2f);

    float xInput;
    float yInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        HandleJump();
        ResetPosition();
        //Vector2 direction = new Vector2(xInput, yInput).normalized;
        //body.linearVelocity = direction * speed;
    }

    private void FixedUpdate()
    {
        CheckGround();
        // ApplyFriction();
        MoveWithInput();

        // Debug.Log($"xInput: {xInput}, Velocity: {body.linearVelocity}, Velocity(x): {body.linearVelocity.x}, Velocity(y): {body.linearVelocity.y}, Grounded: {grounded}");
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            // float increment = xInput;
            // float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            // body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);
            body.linearVelocity = new Vector2(xInput * groundSpeed, body.linearVelocity.y);
            
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(8*direction, 8, 8);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    // DEBUG
    void ResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            Debug.Log("Set Starting Position..");
            transform.position = startingPosition;
        }
    }
    /* void ApplyFriction()
    {
        if (grounded && xInput == 0 && body.linearVelocity.y <= 0)
        {
            body.linearVelocity *= groundDecay;
        }
    } */
}
