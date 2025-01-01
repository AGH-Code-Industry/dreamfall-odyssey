using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 60;
    public int currentHealth;
    public Vector2 initialPosition;

    Animator animator;

    private bool canTakeDamage = true;
    public float invulnerabilityDuration = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        currentHealth -= damage;
        Debug.Log($"Gracz otrzyma� {damage} obra�e�. Pozosta�o zdrowia: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        Invoke(nameof(ResetCanTakeDamage), invulnerabilityDuration);
    }

    public void Die()
    {
        Debug.Log("Gracz zgin��!");
        canTakeDamage = false;
        invulnerabilityDuration = 2f;
        animator.SetTrigger("Die");
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        currentHealth = maxHealth;
        invulnerabilityDuration = 1f;
        canTakeDamage = false;

        Invoke(nameof(ResetCanTakeDamage), invulnerabilityDuration);
    }

    private void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }
}
