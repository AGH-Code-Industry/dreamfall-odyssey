using UnityEngine;

public class NecromancerSpell : MonoBehaviour
{
    public int spellDamage = 10;
    private bool facingRight = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb != null)
        {
            if (rb.linearVelocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (rb.linearVelocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (player != null)
            {
                playerHealth.TakeDamage(spellDamage);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
