using CuaHang;
using UnityEngine;

public class ItemDropChecker : MonoBehaviour
{
    public SensorCast SensorCheckGround;
    public SensorCast SensorCheckAround;

    /// <summary> đối tượng đang được đặt ở vị trí đúng </summary>
    public bool IsAgreeToDrag()
    {
        bool IsHitGround = false; 

        foreach (var obj in SensorCheckGround.TransformHits)
        {
            if(obj.CompareTag("Ground"))
            { 
                IsHitGround = true;
                break;
            }
        }

        return IsHitGround && SensorCheckAround.TransformHits.Count == 0;
    }
}
