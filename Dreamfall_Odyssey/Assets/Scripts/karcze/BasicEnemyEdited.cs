using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Idle;
    public float moveSpeed = 2f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public float detectionRange = 3f;
    protected bool isAttacking;

    protected float lastAttackTime;
    protected Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Idle;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                IdleState();
                break;

            case EnemyState.Patrol:
                PatrolState();
                break;

            case EnemyState.Chase:
                ChaseState();
                break;

            case EnemyState.Attack:
                AttackState();
                break;
        }
    }

    // Funkcja dla stanu "Idle" – co robi przeciwnik, gdy nikogo nie widzi
    protected virtual void IdleState()
    {
        Debug.Log("Przeciwnik jest w stanie Idle.");

        // Tutaj mo¿na dodaæ w³asne zachowanie przeciwnika w stanie Idle
        // Na przyk³ad, przeciwnik mo¿e patrzeæ w ró¿nych kierunkach, szukaæ gracza itp.
    }

    // Funkcja dla stanu "Patrol" – co robi przeciwnik, gdy patroluje obszar
    protected virtual void PatrolState()
    {
        Debug.Log("Przeciwnik patroluje.");

        // Mo¿esz dodaæ kod do patrolowania okreœlonych punktów w grze
        // Na przyk³ad przeciwnik mo¿e chodziæ po okreœlonym obszarze lub wzd³u¿ trasy.
    }

    // Funkcja dla stanu "Chase" – co robi przeciwnik, gdy widzi gracza i go œciga
    protected virtual void ChaseState()
    {
        Debug.Log("Przeciwnik goni gracza.");

        if (player == null) return; // Jeœli nie ma gracza, zakoñcz dzia³anie

        // Przeciwnik goni gracza
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // Funkcja dla stanu "Attack" – co robi przeciwnik, gdy jest w zasiêgu ataku
    protected virtual void AttackState()
    {
        if (isAttacking) return; // Jeœli ju¿ atakujemy, nie wykonujemy kolejnego ataku

        isAttacking = true;
        lastAttackTime = Time.time;

        Debug.Log("Atakujê gracza!");

        // Dodaj w³asn¹ logikê ataku (np. zadawanie obra¿eñ graczowi)

        // Po zakoñczeniu ataku zmieniamy stan
        Invoke("EndAttack", 1f); // Czekamy chwilê przed powrotem do normalnego stanu
    }

    // Zakoñczenie ataku, wracamy do normalnego stanu
    protected virtual void EndAttack()
    {
        isAttacking = false;
        currentState = EnemyState.Idle; // Po ataku przechodzimy do stanu Idle
    }
}