using UnityEngine;

public class Emerald : MonoBehaviour
{
    public int pointValue = 10; // Liczba punktów za 1 emerald

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ZnajdŸ ScoreManager i dodaj punkty
            EmeraldScore emeraldScore = FindObjectOfType<EmeraldScore>();
            if (emeraldScore != null)
            {
                emeraldScore.AddScore(pointValue);
            }

            Destroy(gameObject);
        }
    }
}
