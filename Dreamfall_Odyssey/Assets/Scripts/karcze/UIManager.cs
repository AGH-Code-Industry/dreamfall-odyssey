using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text messageTextJump;
    public TMP_Text messageTextHealth;
    public float displayTime = 3f;

    private void Start()
    {
        messageTextJump.alpha = 0;
        messageTextHealth.alpha = 0;
    }

    public void ShowMessageJump(string message)
    {
        messageTextJump.text = message;
        messageTextJump.alpha = 255;
        Invoke(nameof(HideMessage), displayTime);
    }

    public void ShowMessageHealth(string message)
    {
        messageTextHealth.text = message;
        messageTextHealth.alpha = 255;
        Invoke(nameof(HideMessage), displayTime);
    }

    private void HideMessage()
    {
        messageTextJump.alpha = 0;
        messageTextHealth.alpha = 0;
    }
}
