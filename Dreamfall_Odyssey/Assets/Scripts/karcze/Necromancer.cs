using UnityEngine;

public class Necromancer : EnemyAI
{
    private Animator anim;
    private Rigidbody2D rb;

    private float lastDirectionX;
    private bool facingRight = true;

    // Do spella
    public GameObject spellPrefab;
    public Transform spellPoint;
    public float spellRate = 1f;
    public float spellSpeed = 6f;
    private float nextSpellTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveSpeed = 0.75f;
        detectionRange = 8f;
        attackRange = 5f;
    }

    protected override void ChaseState()
    {
        Debug.Log("Przeciwnik goni gracza.");

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

}
