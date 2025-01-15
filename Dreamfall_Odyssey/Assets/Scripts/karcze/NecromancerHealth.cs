using UnityEngine;

public class NecromancerHealth : MonoBehaviour
{
    public int necromancerMaxHealth = 50;
    public int necromancerCurrentHealth;
    public Vector2 necromancerInitialPosition;

    public GameObject HealthCrystalPrefab;

    private Animator animator;
    void Start()
    {
        necromancerCurrentHealth = necromancerMaxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        necromancerCurrentHealth -= damage;
        Debug.Log($"Necromancer otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {necromancerCurrentHealth}");

        if (necromancerCurrentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            Drop();
        }
    }

    public void Die()
    {
        Debug.Log("Necromancer zgin¹³!");
        Destroy(gameObject);
    }

    private void Drop()
    {
        Instantiate(HealthCrystalPrefab, transform.position, Quaternion.identity);
    }
}
