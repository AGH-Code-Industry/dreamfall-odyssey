using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private bool isOnLadder = false;
    public float climbSpeed = 8.0f;
    private float playerGravityScale;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerGravityScale = rb.gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isOnLadder = true;
            rb.gravityScale = 0; // Disable gravity when on the ladder
            rb.linearVelocity = Vector2.zero; // Stop movement
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isOnLadder = false;
            rb.gravityScale = playerGravityScale; // Re-enable gravity when leaving ladder
        }
    }

    private void Update()
    {
        if (isOnLadder)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
        }
    }
}
