using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    bool isGrounded;
    void OnTrigerEnter2D()
    {
        Debug.Log("OnTrigerEnter2D");
        isGrounded = true;
    }
    void OnTrigerExit2D()
    {
        Debug.Log("OnTrigerExit2D");
        isGrounded = false;
    }
}
