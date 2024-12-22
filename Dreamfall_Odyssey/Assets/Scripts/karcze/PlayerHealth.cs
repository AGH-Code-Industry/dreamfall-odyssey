using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 60;
    private int currentHealth;
    public Vector2 initialPosition;

    Animator animator;

    private bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        currentHealth -= damage;
        Debug.Log($"Gracz otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Gracz zgin¹³!");
        canTakeDamage = false;
        animator.SetTrigger("Die");
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        currentHealth = maxHealth;
        canTakeDamage = false;

        Invoke(nameof(ResetCanTakeDamage), 1f);
    }

    private void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }
}
