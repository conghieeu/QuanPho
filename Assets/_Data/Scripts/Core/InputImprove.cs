using UnityEngine;
using UnityEngine.InputSystem;

public class InputImprove : MonoBehaviour
{
    public InputActionReference UIClick;
    public InputActionReference UIPoint;
    public InputActionReference PlayerMove;
    public InputSystem_Actions InputActions;

    public Vector2 GetMoveVector()
    {
        return PlayerMove.action.ReadValue<Vector2>();
    }

    public Vector2 GetMousePoint()
    {
        return UIPoint.action.ReadValue<Vector2>();
    }
}
