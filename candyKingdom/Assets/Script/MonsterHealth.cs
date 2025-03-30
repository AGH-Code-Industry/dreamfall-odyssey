using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int health;
    public Animator anim;
    public MonsterMovement movement;

    void Start()
    {
        health = maxHealth;
        movement = GetComponent<MonsterMovement>();
    }

    public void Take_Damage(int damage)
    {
        StartCoroutine(HandleDamage(damage));
    }

    public IEnumerator HandleDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHurt");

        if (movement != null)
        {
            StartCoroutine(movement.StopMovement(1f));
        }

        if (health <= 0)
        {
            EnemyManager.RegisterEnemyDeath();
            anim.SetBool("isDead", true);
            movement.enabled = false;
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

    }

    
}
