using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler), typeof(AudioSource))]
public class PlayerCharacterController : MonoBehaviour
{
    public float MoveSpeed = 1;

    PlayerInputHandler playerInputHandler;
    CharacterController characterController;
    Camera cam;

    private void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        Vector2 moveInput = playerInputHandler.GetMoveInput();
        Vector3 moveDirection = Vector3.zero;

        // get camera direction, di chuyen theo huong cua camera
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        Vector3 forwardRelative = moveInput.y * camForward;
        Vector3 rightRelative = moveInput.x * camRight;
        moveDirection = (forwardRelative + rightRelative).normalized;
        moveDirection.y = 0;

        // set velocity 
        characterController.Move(moveDirection * MoveSpeed * Time.deltaTime);

    }
}
