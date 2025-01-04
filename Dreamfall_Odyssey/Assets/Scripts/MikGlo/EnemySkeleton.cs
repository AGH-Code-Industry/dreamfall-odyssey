using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySkeleton : Enemy
{
    public int damage = 100; // Damage value to apply to the player
    public float damageInterval = 1f; // Time in seconds between damage applications
    private Coroutine damageCoroutine;

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Check if the collided object has a Player component
    //    PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
    //    if (playerHealth != null)
    //    {
    //        playerHealth.TakeDamage(damage); // Call the player's damage method
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            damageCoroutine = StartCoroutine(ApplyContinuousDamage(playerHealth));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (playerHealth != null && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private IEnumerator ApplyContinuousDamage(PlayerHealth playerHealth)
    {
        while (true)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
