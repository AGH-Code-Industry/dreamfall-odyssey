using UnityEngine;

public class Emerald : MonoBehaviour
{
    public int pointValue = 1; // Liczba punktów za 1 emerald

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ZnajdŸ ScoreManager i dodaj punkty
            EmeraldScore emeraldScore = Object.FindFirstObjectByType<EmeraldScore>();
            if (emeraldScore != null)
            {
                emeraldScore.AddScore(pointValue);
            }

            Destroy(gameObject);
        }
    }
}
