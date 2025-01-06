using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage = 100; // Damage value to apply to the player

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has a Player component
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Call the player's damage method
        }
    }
}
