using UnityEngine;

public class HealthCrystal : MonoBehaviour
{
    // Odnawia hp gracza
    public UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.maxHealth = 80;
                playerHealth.currentHealth = playerHealth.maxHealth;
            }

            if (uiManager != null)
            {
                uiManager.ShowMessageHealth("HP + 20 \nHP restored!");
            }

            Destroy(gameObject);
        }
    }
}
