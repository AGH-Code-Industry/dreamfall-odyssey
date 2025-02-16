using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredKeys = 3;
    [SerializeField] private GameObject lockedMessage;
    [SerializeField] private Sprite openDoorSprite;

    private BoxCollider2D doorCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();

        if (lockedMessage != null)
        {
            lockedMessage.SetActive(false); // Hide the locked message at the start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.Instance.KeyCount >= requiredKeys)
            {
                OpenDoor();
            }
            else
            {
                ShowLockedMessage();
            }
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Door Opened!");

        if (openDoorSprite != null)
        {
            spriteRenderer.sprite = openDoorSprite; // Change to open door sprite
            doorCollider.enabled = false; // Disable the collider so player can walk through
        }
        else
        {
            Destroy(gameObject); // If no sprite is set, destroy the door
        }
    }

    private void ShowLockedMessage()
    {
        Debug.Log("You need 3 keys!");
        if (lockedMessage != null)
        {
            lockedMessage.SetActive(true);
            Invoke("HideLockedMessage", 2f); // Hide message after 2 seconds
        }
    }

    private void HideLockedMessage()
    {
        if (lockedMessage != null)
            lockedMessage.SetActive(false);
    }
}
