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

    // Funkcja dla stanu "Idle" � co robi przeciwnik, gdy nikogo nie widzi
    protected virtual void IdleState()
    {
        Debug.Log("Przeciwnik jest w stanie Idle.");

        // Tutaj mo�na doda� w�asne zachowanie przeciwnika w stanie Idle
        // Na przyk�ad, przeciwnik mo�e patrze� w r�nych kierunkach, szuka� gracza itp.
    }

    // Funkcja dla stanu "Patrol" � co robi przeciwnik, gdy patroluje obszar
    protected virtual void PatrolState()
    {
        Debug.Log("Przeciwnik patroluje.");

        // Mo�esz doda� kod do patrolowania okre�lonych punkt�w w grze
        // Na przyk�ad przeciwnik mo�e chodzi� po okre�lonym obszarze lub wzd�u� trasy.
    }

    // Funkcja dla stanu "Chase" � co robi przeciwnik, gdy widzi gracza i go �ciga
    protected virtual void ChaseState()
    {
        Debug.Log("Przeciwnik goni gracza.");

        if (player == null) return; // Je�li nie ma gracza, zako�cz dzia�anie

        // Przeciwnik goni gracza
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // Funkcja dla stanu "Attack" � co robi przeciwnik, gdy jest w zasi�gu ataku
    protected virtual void AttackState()
    {
        if (isAttacking) return; // Je�li ju� atakujemy, nie wykonujemy kolejnego ataku

        isAttacking = true;
        lastAttackTime = Time.time;

        Debug.Log("Atakuj� gracza!");

        // Dodaj w�asn� logik� ataku (np. zadawanie obra�e� graczowi)

        // Po zako�czeniu ataku zmieniamy stan
        Invoke("EndAttack", 1f); // Czekamy chwil� przed powrotem do normalnego stanu
    }

    // Zako�czenie ataku, wracamy do normalnego stanu
    protected virtual void EndAttack()
    {
        isAttacking = false;
        currentState = EnemyState.Idle; // Po ataku przechodzimy do stanu Idle
    }
}