using UnityEngine;
using UnityEngine.InputSystem;

public class InputMenager : MonoBehaviour
{
    public static PlayerInput playerInput;

    public static Vector2 movement;
    public static bool jumpWasPressed;
    public static bool jumpIsHeld;
    public static bool jumpWasReleased;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void Update()
    {
        movement = moveAction.ReadValue<Vector2>();

        jumpWasPressed = jumpAction.WasPerformedThisFrame();
        jumpIsHeld = jumpAction.IsPressed();
        jumpWasReleased = jumpAction.WasReleasedThisFrame();

    }
}
