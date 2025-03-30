using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
    public int skeletonMaxHealth = 30;
    public int skeletonCurrentHealth;
    public Vector2 skeletonInitialPosition;

    public GameObject JumpCrystalPrefab;
    public GameObject EmeraldPrefab;
    public GameObject HealthCrystalPrefab;
    public DropType dropType;
    public enum DropType
    {
        JumpCrystal,
        Emerald,
        HealthCrystal
    }

    private Animator animator;

    private void Start()
    {
        skeletonCurrentHealth = skeletonMaxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        skeletonCurrentHealth -= damage;
        animator.SetBool("isHit", true);
        Debug.Log($"Skeleton otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {skeletonCurrentHealth}");

        if (skeletonCurrentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            Drop();
        }
    }

    public void EndHit()
    {
        animator.SetBool("isHit", false);
    }

    public void Die()
    {
        Debug.Log("Skeleton zgin¹³!");
        Destroy(gameObject);
    }

    private void Drop()
    {
        if (dropType == DropType.JumpCrystal)
        {
            Instantiate(JumpCrystalPrefab, transform.position, Quaternion.identity);
        }
        else if (dropType == DropType.Emerald)
        {
            Instantiate(EmeraldPrefab, transform.position, Quaternion.identity);
        }
        else if (dropType == DropType.HealthCrystal)
        {
            Instantiate(HealthCrystalPrefab, transform.position, Quaternion.identity);
        }
    }
}