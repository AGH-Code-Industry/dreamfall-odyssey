using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HPScore : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth playerHealth;


    private void Update()
    {
        if (playerHealth != null && healthText != null )
        {
            healthText.text = $"HP: {playerHealth.currentHealth} / {playerHealth.maxHealth}";

            if (playerHealth.currentHealth < 0)
            {
                healthText.text = $"HP: 0 / {playerHealth.maxHealth}";
            }
        }
    }
}
