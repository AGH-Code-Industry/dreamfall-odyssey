using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 60;
    public int currentHealth;
    public Vector2 initialPosition;

    Animator animator;

    private bool canTakeDamage = true;

    // to aktualnie trochê nie jest potrzebne, ale mo¿e jeszcze mi siê kiedyœ przyda
    public float invulnerabilityDuration = 0f;

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
        Debug.Log($"Gracz otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        Invoke(nameof(ResetCanTakeDamage), invulnerabilityDuration);
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

        Invoke(nameof(ResetCanTakeDamage), invulnerabilityDuration);
    }

    private void ResetCanTakeDamage()
    {
        canTakeDamage = true;
    }
}
