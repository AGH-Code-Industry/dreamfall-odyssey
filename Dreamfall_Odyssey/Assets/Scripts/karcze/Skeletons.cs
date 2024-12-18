using UnityEngine;

public class Skeleton : EnemyAI
{
    public float patrolRange = 1f;
    private Vector2 initialPosition;
    private bool movingRight = true;
    public int damage = 5;

    private Animator anim;
    private bool isMoving = false;

    private void Awake()
    {
        initialPosition = transform.position;
        anim = GetComponent<Animator>();
    }


    protected override void PatrolState()
    { 
        float leftLimit = initialPosition.x - patrolRange;
        float rightLimit = initialPosition.x + patrolRange;

        isMoving = true;

        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                Flip();
            }
        }

        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
        }
    }


    protected override void AttackState()
    {
        isMoving = false;
        if (anim != null)
        {
            anim.SetBool("isMoving", isMoving);
        }

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void Flip()
    {
        
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
