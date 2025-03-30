using UnityEngine;

public class Sword : MonoBehaviour
{
    public UIManager uiManager;

    private void Start()
    {
        uiManager = Object.FindFirstObjectByType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.damage = 20;
        }

        uiManager.ShowMessageSword("You found a good sword! \ndamage +10");

        Destroy(gameObject);
    }
}
