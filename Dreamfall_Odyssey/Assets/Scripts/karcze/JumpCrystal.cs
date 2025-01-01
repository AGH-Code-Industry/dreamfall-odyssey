using UnityEngine;

public class JumpCrystal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.doubleJump();
            }

            Destroy(gameObject);
        }
    }
}
