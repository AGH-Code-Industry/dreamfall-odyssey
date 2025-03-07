using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed;
    public bool flipIsActive = true;

    public Vector3 target;
    public Rigidbody2D body;
    public float direction = -1.0f;

    public LayerMask groundMask;
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
    // private bool canWalkoOnSlope;

    void Start()
    {
        target = pointA.position;
        colliderSize = bc.size;
    }

    void Update()
    {
        
    }

    protected void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y - colliderSize.y + 1.0f);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundMask);

        Debug.DrawRay(checkPos, transform.right * slopeCheckDistance, Color.red);  // Rysuje promień do przodu
        Debug.DrawRay(checkPos, -transform.right * slopeCheckDistance, Color.blue); // Rysuje promień do tyłu

        if (slopeHitFront)
        {
            //Debug.Log("slopeHitFront");
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            //Debug.Log("slopeHitBack");
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            //Debug.Log("Else");
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundMask);

        Debug.DrawRay(checkPos, Vector2.down * slopeCheckDistance, Color.green); // Rysuje promień w dół

        if (hit)
        {
            Debug.Log("dziala");
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            Debug.Log("Angle: " + slopeDownAngle);
            if (slopeDownAngle > 10)
            {
                Debug.Log("IS ON SLOPE");
                isOnSlope = true;
            }

            slopeDownAngeOld = slopeDownAngle;
           
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        /*if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkoOnSlope = false;
        }
        else
        {
            canWalkoOnSlope = true;
        }*/

        /*if (isOnSlope && xInput == 0.0f && canWalkoOnSlope)
        {
            body.sharedMaterial = fullFriction;
        }
        else
        {
            body.sharedMaterial = null;
        }*/
    }

    public void Patrol()
    {
        SlopeCheck();

        //Debug.Log("TARGET:" + target);
        // Debug.Log("Transform.position:" + Vector3.Distance(transform.position, target));
        /*transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (flipIsActive)
        {
            FlipTowards(target);
        }
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
            // transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }*/
        // Debug.Log("Velocity: " + body.linearVelocity);
        // Wyznaczenie kierunku ruchu

        // Ustawienie pr�dko�ci w kierunku celu
        //body.linearVelocity = new Vector2(direction * speed, body.linearVelocity.y);
        if (isOnSlope)
        {
            body.linearVelocity = new Vector2(speed*1.5f * slopeNormalPerp.x * -direction, speed * slopeNormalPerp.y * -direction);
            Debug.Log("Joł");
        }
        else if (!isOnSlope)
        {
            Debug.Log("direction: " + direction);
            body.linearVelocity = new Vector2(direction * speed, 0.0f);
        }

        // Sprawdzenie odleg�o�ci do celu
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            // Zmiana celu na przeciwny punkt
            target = target == pointA.position ? pointB.position : pointA.position;
            direction = -direction;

            // Debugging: Informacja o zmianie celu
            Debug.Log("Target reached. New target: " + target);
        }

        // Obracanie przeciwnika w kierunku ruchu
        if (flipIsActive)
        {
            FlipTowards(target);
        }
    }

    protected void FlipTowards(Vector3 targetPosition)
    {
        // Debug.Log("Grzyb: "+transform.position);
        // Debug.Log("Target: " + targetPosition);
        // Obraca przeciwnika w zale�no�ci od kierunku
        if (targetPosition.x > transform.position.x)
        {
            // Ruch w prawo
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y); // Skaluj w prawo
            direction = 1;
            // Debug.Log("PRAWO");
        }
        if (targetPosition.x < transform.position.x)
        {
            // Ruch w lewo
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y); // Skaluj w lewo
            direction = -1;
            // Debug.Log("LEWO");
        }
    }
}
