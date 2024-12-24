using UnityEngine;
using UnityEngine.U2D;

public class Rocket : MonoBehaviour
{
    public int facing = 1;
    public float speed = 4f;
    public Transform sprite;
    public Rigidbody2D rb;

    private bool touching_player;

    private void Start()
    {
        sprite.localScale = new Vector3(facing, 1, 1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player plr = col.GetComponent<Player>();
            plr.Die();
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag == "Ground") {
            Explode();
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag == "Player")
        {
            Player player = col.GetComponent<Player>();
            player.extra_vel = rb.linearVelocity;
        }
    }

    public void Explode()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(-speed * facing, 0, 0);
    }
}
