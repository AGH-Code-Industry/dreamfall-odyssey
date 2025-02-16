using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text keyText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text deathsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateKeyUI(int count)
    {
        keyText.text = "Keys: " + count + "/3";
    }
    public void UpdateHPUI(int health)
    {
        healthText.text = "HP: " + health;
    }
    public void UpdateDeathsUI(int count)
    {
        deathsText.text = "Deaths: " + count;
    }
}
