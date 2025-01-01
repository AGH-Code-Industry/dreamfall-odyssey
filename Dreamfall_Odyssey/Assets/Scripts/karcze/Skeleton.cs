using JetBrains.Annotations;
using UnityEngine;

public class Skeleton : EnemyAI
{
    private Animator anim;
    public int skeletonDamage = 5;
    private Rigidbody2D rb;
    private float lastDirectionX;
    private SkeletonHealth SkeletonHealth;

    private void Awake()
    {
        SkeletonHealth = GetComponent<SkeletonHealth>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void AttackState()
    {

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(skeletonDamage);
                anim.SetBool("isAttacking", true);
            }
        }
    }
    protected override void ChaseState()
    {
        Debug.Log("Przeciwnik goni gracza.");

        if (player == null) return; // Jeśli nie ma gracza, zakończ działanie

        // Oblicz kierunek w 2D
        Vector2 direction = (player.position - transform.position).normalized;

        // Uzyskaj dostęp do komponentu Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            anim.SetBool("isMoving", true);

        }

        if (direction.x > 0 && lastDirectionX <= 0) // Przeciwnik zaczyna iść w prawo
        {
            Flip();
        }
        else if (direction.x < 0 && lastDirectionX >= 0) // Przeciwnik zaczyna iść w lewo
        {
            Flip();
        }

        lastDirectionX = direction.x;
        anim.SetBool("isAttacking", false);
    }

    protected override void IdleState()
    {
        Debug.Log("Przeciwnik jest w stanie Idle.");
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InstantDeath"))
        {
            SkeletonHealth.Die();
        }
    }
}
