using UnityEngine;
using static SkeletonHealth;

public class Chest : MonoBehaviour
{
    private Player player;
    public GameObject chestPoint;
    public GameObject emerald;
    public GameObject sword;

    public DropType treasureType;
    public enum DropType
    {
        Sword,
        Emerald
    }

    public Sprite openChest;
    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = Object.FindAnyObjectByType<Player>();
        
        if (collision.CompareTag("Player"))
        {
            OpenChest();
            spriteRenderer.sprite = openChest;
        }
    }

    public void OpenChest()
    {
        if (!isOpened)
        {
            if (treasureType == DropType.Emerald)
            {
                for (int i = 0; i < 6; i++)
                {
                    float randomOffsetX = UnityEngine.Random.Range(-1.5f, 1.5f);
                    float randomOffsetY = UnityEngine.Random.Range(0f, 0.5f);
                    Vector3 spawnEmeraldPosition = chestPoint.transform.position + new Vector3(randomOffsetX, randomOffsetY, 0);

                    Instantiate(emerald, spawnEmeraldPosition, Quaternion.identity);
                }
            }
            else if (treasureType == DropType.Sword)
            {
                Vector3 spawnSwordPosition = chestPoint.transform.position + new Vector3(0, 1, 0);
                Instantiate(sword, spawnSwordPosition, Quaternion.identity);
            }

            isOpened = true;
        }
    }
}
