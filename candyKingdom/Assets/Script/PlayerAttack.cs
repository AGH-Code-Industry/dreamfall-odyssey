using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public float attackRange = 0.5f;
    [SerializeField] public LayerMask enemyLayers;
    [SerializeField] public int damage;
    [SerializeField] private float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.K) && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            MonsterHealth monsterHealth = enemy.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.Take_Damage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}



 /*void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.K))
        {
            anim.SetTrigger("isAttacking");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                MonsterHealth monsterHealth = enemy.GetComponent<MonsterHealth>();
                if (monsterHealth != null)
                {
                    monsterHealth.Take_Damage(damage);
                }
            }
        }
    }
    */