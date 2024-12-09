using UnityEngine;

public class spring : MonoBehaviour
{

    public float bounce_force = 7f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounce_force);
            Player plr = col.GetComponent<Player>();
            plr.canJump = true;
        }
    }
}
