using Unity.Android.Gradle.Manifest;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(AudioSource))]
public class PlayerCharacterController : MonoBehaviour
{
    public float MoveSpeed = 1;
    public float Gravity = -10;

    InputImprove inputImprove;
    CharacterController characterController;
    Camera cam;

    private void Start()
    {
        inputImprove = FindFirstObjectByType<InputImprove>();
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        Vector2 moveInput = inputImprove.GetMoveVector();
        Vector3 moveDirection = Vector3.zero;
        
        // get camera direction, di chuyen theo huong cua camera
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        
        Vector3 forwardRelative = moveInput.y * camForward;
        Vector3 rightRelative = moveInput.x * camRight;
        moveDirection = forwardRelative + rightRelative;
        moveDirection.y = 0;
        moveDirection.Normalize(); 

        // set velocity
        Vector3 moveVelocity = moveDirection * MoveSpeed;
        moveVelocity.y = Gravity;
        characterController.Move(moveVelocity * Time.deltaTime);
    }

}
