using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 initialPosition; // To store the initial position of the player
    private int deathsCount = 0;
    //public Transform respawnPoint;
    //public GameObject playerPrefab; // Prefab to instantiate upon respawn

    void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position; // Store the player's starting position
        UIManager.Instance.UpdateHPUI(maxHealth);
        UIManager.Instance.UpdateDeathsUI(deathsCount); // Update UI with count of deaths on start
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            UIManager.Instance.UpdateHPUI(0);
            Die();
        }
        UIManager.Instance.UpdateHPUI(currentHealth);
    }

    void Die()
    {
        // Handle player death
        Respawn();
    }

    void Respawn()
    {
        // Reset player position to the initial position
        transform.position = initialPosition;

        // Reset health
        currentHealth = maxHealth;

        // Update UI with count of deaths
        UIManager.Instance.UpdateDeathsUI(++deathsCount);

        // Update UI with health
        UIManager.Instance.UpdateHPUI(maxHealth);

        //// Destroy current player
        //Destroy(gameObject);

        //// Instantiate a new player at the respawn point
        //GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        //newPlayer.GetComponent<PlayerHealth>().respawnPoint = respawnPoint; // Set the respawn point for the new player
        //newPlayer.GetComponent<PlayerHealth>().playerPrefab = playerPrefab; // Set the prefab for the new player
    }
}
