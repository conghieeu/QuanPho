using UnityEngine;
using UnityEngine.InputSystem;

public class InputImprove : MonoBehaviour
{
    public InputActionReference PlayerMove;
    public InputSystem_Actions InputActions;

    public Vector2 GetMoveVector()
    {
        return PlayerMove.action.ReadValue<Vector2>();
    }
}
