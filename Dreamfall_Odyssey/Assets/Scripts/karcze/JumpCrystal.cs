using UnityEngine;

public class JumpCrystal : MonoBehaviour
{
    // zebranie obiektu odpala double jump

    public UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.doubleJump();
            }

            if (uiManager != null)
            {
                uiManager.ShowMessageJump("Double jump unlocked!");
            }


            Destroy(gameObject);
        }
    }
}
