using UnityEngine;

public class MArrow : MonoBehaviour
{
    public int damage = 50;  // Obrażenia, które zadaje strzała

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Możesz dodać dodatkową logikę, np. zadawanie obrażen
        // Check if the collided object has a Player component
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage); // Call the player's damage method
        }

        // Zniszczenie strzały po kolizji
        Destroy(gameObject);
    }
}

