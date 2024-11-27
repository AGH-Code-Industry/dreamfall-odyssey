using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public enum EnemyState {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public EnemyState currentState = EnemyState.Idle;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public float detectionRange = 5f;
    private bool isAttacking;

    private float lastAttackTime;
    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        var distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown) {
            currentState = EnemyState.Attack;
        }
        else if (distanceToPlayer <= detectionRange) {
            currentState = EnemyState.Chase;
        }
        else {
            currentState = EnemyState.Idle;
        }
        
        switch (currentState) {
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
    protected virtual void IdleState() {
        Debug.Log("Przeciwnik jest w stanie Idle.");

        // Tutaj można dodać własne zachowanie przeciwnika w stanie Idle
        // Na przykład, przeciwnik może patrzeć w różnych kierunkach, szukać gracza itp.
    }

    // Funkcja dla stanu "Patrol" – co robi przeciwnik, gdy patroluje obszar
    protected virtual void PatrolState() {
        Debug.Log("Przeciwnik patroluje.");

        // Możesz dodać kod do patrolowania określonych punktów w grze
        // Na przykład przeciwnik może chodzić po określonym obszarze lub wzdłuż trasy.
    }

    // Funkcja dla stanu "Chase" – co robi przeciwnik, gdy widzi gracza i go ściga
    protected virtual void ChaseState() {
        Debug.Log("Przeciwnik goni gracza.");

        if (player == null) return; // Jeśli nie ma gracza, zakończ działanie

        // Przeciwnik goni gracza
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // Funkcja dla stanu "Attack" – co robi przeciwnik, gdy jest w zasięgu ataku
    protected virtual void AttackState() {
        if (isAttacking) return; // Jeśli już atakujemy, nie wykonujemy kolejnego ataku

        isAttacking = true;
        lastAttackTime = Time.time;

        Debug.Log("Atakuję gracza!");

        // Dodaj własną logikę ataku (np. zadawanie obrażeń graczowi)

        // Po zakończeniu ataku zmieniamy stan
        Invoke("EndAttack", 0.5f); // Czekamy chwilę przed powrotem do normalnego stanu
    }

    // Zakończenie ataku, wracamy do normalnego stanu
    protected virtual void EndAttack() {
        isAttacking = false;
        currentState = EnemyState.Idle; // Po ataku przechodzimy do stanu Idle
    }
}