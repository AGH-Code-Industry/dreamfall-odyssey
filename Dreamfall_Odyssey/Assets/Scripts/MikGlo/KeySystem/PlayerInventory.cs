using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    public int KeyCount { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddKey()
    {
        KeyCount++;
        UIManager.Instance.UpdateKeyUI(KeyCount);
    }
}
