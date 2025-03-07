using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;
    public float attackDistance;
    public Transform playerTransform;
    public bool isAttacking;
    public Animator animator;

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetFloat("Blend", 0.5f);
        }
    }

    /*private void OnTriggerExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetFloat("Blend", 1f);
        }
    }*/
}
