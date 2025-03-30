using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 60;
    public int currentHealth;
    public Vector2 initialPosition;

    Animator animator;

    public GameObject emerald;
    private EmeraldScore emeraldScore;

    public bool canTakeDamage = true;

    // to aktualnie trochê nie jest potrzebne, ale mo¿e jeszcze mi siê kiedyœ przyda
    public float invulnerabilityDuration = 2f;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        emeraldScore = UnityEngine.Object.FindFirstObjectByType<EmeraldScore>();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        currentHealth -= damage;
        animator.SetBool("isHit", true);
        Debug.Log($"Gracz otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {currentHealth}");

        if (currentHealth <= 0)
        {
            canTakeDamage = false;
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("Gracz zgin¹³!");
        animator.SetTrigger("Die");
    }

    public void EndHit()
    {
        animator.SetBool("isHit", false);
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    public void Drop()
    { 
        Player player = GetComponent<Player>();
        for (int i = 0; i < emeraldScore.score; i++)
        {
            float randomOffsetX = UnityEngine.Random.Range(1f, 2f);
            Vector3 spawnPosition = player.attackPoint.transform.position + new Vector3(randomOffsetX, 0, 0);
            
            Instantiate(emerald, spawnPosition, Quaternion.identity);
        }
        emeraldScore.ResetScore();
    }

    /*private void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }*/
}
