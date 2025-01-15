using UnityEngine;
using TMPro;

public class EmeraldScore : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Aktualny wynik: " + score);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Emeralds: " + score;
    }
}
