 using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 1;

    Rigidbody rd;
    InputImprove inputImprove;
    Camera cam;

    private void Start()
    {
        inputImprove = FindAnyObjectByType<InputImprove>();

        cam = Camera.main;
        rd = GetComponent<Rigidbody>();
    }

    private void Movement()
    {
        Vector3 moveDirection = Vector3.zero;
        Vector2 inputVectorMovement = inputImprove.GetMoveVector();

        // get camera direction, di chuyen theo huong cua camera
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        Vector3 forwardRelative = inputVectorMovement.y * camForward;
        Vector3 rightRelative = inputVectorMovement.x * camRight;
        moveDirection = (forwardRelative + rightRelative).normalized;

        // movement 
        Vector3 velocity = new Vector3(moveDirection.x, rd.linearVelocity.y, moveDirection.z) * MoveSpeed;

        rd.linearVelocity = velocity;

        // Trường hợp đang kéo thả Item nào đó
        if (rd.linearVelocity.magnitude > 0)
        {
            velocity.y = 0;
            transform.forward = velocity;
        }
    }
}
