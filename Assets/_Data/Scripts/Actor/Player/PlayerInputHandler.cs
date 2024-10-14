using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    InputImprove inputImprove;

    private void Start()
    {
        inputImprove = FindFirstObjectByType<InputImprove>();
    }

    public Vector3 GetMoveInput()
    {   
        return inputImprove.GetMoveVector();
    }
}
