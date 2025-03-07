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
    // public Transform groundCheck2;
    public LayerMask groundMask;
    public bool grounded;
    public Vector2 startingPosition = new Vector2(-2.0f, -0.2f);

    float xInput;
    float yInput;

    [SerializeField]
    private BoxCollider2D bc;
    private Vector2 colliderSize;
    [SerializeField]
    private float slopeCheckDistance;
    private float slopeDownAngle;
    private Vector2 slopeNormalPerp;
    private bool isOnSlope;
    private float slopeDownAngeOld;
    private float slopeSideAngle;

    [SerializeField]
    private PhysicsMaterial2D noFriction;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;

    private bool isJumping;

    [SerializeField]
    private float maxSlopeAngle;
    private bool canWalkoOnSlope;

    // public Transform rayCastOrigin;
    // public RaycastHit2D Hit2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliderSize = bc.size;
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
        SlopeCheck();
        // ApplyFriction();
        MoveWithInput();

        // Debug.Log($"xInput: {xInput}, Velocity: {body.linearVelocity}, Velocity(x): {body.linearVelocity.x}, Velocity(y): {body.linearVelocity.y}, Grounded: {grounded}");
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundMask);


        Debug.DrawRay(checkPos, transform.right * slopeCheckDistance, Color.red);  // Rysuje promieñ do przodu
        Debug.DrawRay(checkPos, -transform.right * slopeCheckDistance, Color.blue); // Rysuje promieñ do ty³u

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if(slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    /* private void StickToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f,  groundMask);

        if(hit.collider != null)
        {
            transform.position = new Vector2(transform.position.x, hit.point.y + 1.2f);
        }
    } */

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundMask);

        Debug.DrawRay(checkPos, Vector2.down * slopeCheckDistance, Color.green); // Rysuje promieñ w dó³

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            
            if (slopeDownAngle > 10)
            {
                isOnSlope = true;
            }

            slopeDownAngeOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkoOnSlope = false;
        }
        else
        {
            canWalkoOnSlope = true;
        }

        if (isOnSlope && xInput == 0.0f && canWalkoOnSlope)
        {
            body.sharedMaterial = fullFriction;
        }
        else
        {
            body.sharedMaterial = null;
        }
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
            if(grounded && !isOnSlope && !isJumping)
            {
                body.linearVelocity = new Vector2(groundSpeed * xInput, 0.0f);
            }
            else if(grounded && isOnSlope && !isJumping && canWalkoOnSlope)
            {
                //StickToGround();
                body.linearVelocity = new Vector2(groundSpeed * slopeNormalPerp.x * -xInput, groundSpeed * slopeNormalPerp.y * -xInput);
            }
            else if(!grounded) 
            {
                body.linearVelocity = new Vector2(groundSpeed * xInput, body.linearVelocity.y);
            }
            // body.isKinematic = false;
            // float increment = xInput;
            // float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -groundSpeed, groundSpeed);
            // body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);
            // body.linearVelocity = new Vector2(xInput * groundSpeed, body.linearVelocity.y);
            
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(7*direction, 7, 7);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded && !isJumping)
        {
            isJumping = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        }
        
        if (body.linearVelocity.y <= 0.0f)
        {
            isJumping = false;
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        // Hit2D = Physics2D.Raycast(rayCastOrigin.position, -Vector2.up, 100f, groundMask);

        // if (Hit2D != false)
        // {
        //     Vector2 temp = groundCheck2.position;
        //     temp.y = Hit2D.point.y;
        //     groundCheck2.position = temp;
        // }
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
