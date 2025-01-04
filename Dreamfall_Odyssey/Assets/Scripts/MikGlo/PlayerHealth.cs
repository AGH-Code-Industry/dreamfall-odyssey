using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 initialPosition; // To store the initial position of the player
    //public Transform respawnPoint;
    //public GameObject playerPrefab; // Prefab to instantiate upon respawn

    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position; // Store the player's starting position
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death
        Debug.Log("Player died");
        Respawn();
    }

    void Respawn()
    {
        // Reset player position to the initial position
        transform.position = initialPosition;

        // Reset health
        currentHealth = maxHealth;

        //// Destroy current player
        //Destroy(gameObject);

        //// Instantiate a new player at the respawn point
        //GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        //newPlayer.GetComponent<PlayerHealth>().respawnPoint = respawnPoint; // Set the respawn point for the new player
        //newPlayer.GetComponent<PlayerHealth>().playerPrefab = playerPrefab; // Set the prefab for the new player
    }
}
