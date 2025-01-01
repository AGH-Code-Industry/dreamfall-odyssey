using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
    public int skeletonMaxHealth = 30;
    public int skeletonCurrentHealth;
    public Vector2 skeletonInitialPosition;

    public Transform dropPoint;
    public GameObject JumpCrystalPrefab;

    private Animator animator;

    private void Start()
    {
        skeletonCurrentHealth = skeletonMaxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        skeletonCurrentHealth -= damage;  // Zmniejszamy zdrowie szkieleta o obra¿enia
        Debug.Log($"Skeleton otrzyma³ {damage} obra¿eñ. Pozosta³o zdrowia: {skeletonCurrentHealth}");

        if (skeletonCurrentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            DropJump();
        }
    }

    public void Die()
    {
        Debug.Log("Skeleton zgin¹³!");
        Destroy(gameObject);
    }

    private void DropJump()
    {
        GameObject jumpCrystal = Instantiate(JumpCrystalPrefab, dropPoint.position, dropPoint.rotation);
    }
}