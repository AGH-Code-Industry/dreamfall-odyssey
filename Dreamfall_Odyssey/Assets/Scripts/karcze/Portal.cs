using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Sprite portal;
    private Player player;
    private SpriteRenderer spriteRenderer;
    private NecromancerHealth necromancerHealth;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        necromancerHealth = FindAnyObjectByType<NecromancerHealth>();
    }

    private void Update()
    {
        if (necromancerHealth.isDefeated == true)
        {
            spriteRenderer.sprite = portal;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = Object.FindAnyObjectByType<Player>();
        if (collision.CompareTag("Player") || necromancerHealth.isDefeated == true)
        {
            // przechodzenie do kolejnen sceny
        }
    }

}
