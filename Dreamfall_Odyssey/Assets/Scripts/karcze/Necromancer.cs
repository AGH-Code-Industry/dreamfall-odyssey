using UnityEngine;

public class Necromancer : MyEnemyAI
{
    private Animator anim;
    private Rigidbody2D rb;

    private float lastDirectionX;
    private bool facingRight = true;

    // Do spella
    public GameObject spellPrefab;
    public GameObject SkeletonPrefab;
    public Transform spellPoint;
    public float spellRate = 0.5f;
    public float spellSpeed = 6f;
    private float nextSpellTime;

    // do resurrection
    private bool resurrected1 = false;
    private bool resurrected2 = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 0.75f;
        detectionRange = 9f;
        attackRange = 4f;
    }

    protected override void ChaseState()
    {

        if (player == null) return; // Jeśli nie ma gracza, zakończ działanie

        // Przeciwnik goni gracza
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (direction.x > 0 && !facingRight) // Ruch w prawo
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight) // Ruch w lewo
        {
            Flip();
        }
    }

    protected override void AttackState()
    {
        if (Time.time >= nextSpellTime)
        {
            CastSpell();
            nextSpellTime = Time.time + 1f / spellRate;
            NecromancerHealth necromancerHealth = GetComponent<NecromancerHealth>();
            if (necromancerHealth.necromancerCurrentHealth <= 20 && !resurrected1)
            {
                Resurrect();
                resurrected1 = true;
            }
            if (necromancerHealth.necromancerCurrentHealth <= 10 && !resurrected2)
            {
                Resurrect();
                resurrected2 = true;
            }
        }
    }

    private void CastSpell()
    {
        if (player == null) return;

        Vector2 directionToPlayer = (player.position - spellPoint.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        GameObject necromancerSpell = Instantiate(spellPrefab, spellPoint.position, spellPoint.rotation);
        Rigidbody2D rb = necromancerSpell.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = directionToPlayer * spellSpeed;
        }

        Destroy(necromancerSpell, 5f);
    }
    
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void Resurrect()
    {
        GameObject resurrectedSkeleton = Instantiate(SkeletonPrefab, spellPoint.position, spellPoint.rotation);
    }

}
