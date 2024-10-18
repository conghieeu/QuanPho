using QFSW.QC;
using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    public Transform ObjectTele;
    public Transform EndPointTele;

    [Command("/Tele/ToEndPoint")]
    public void TeleportToEndpoint()
    {
        if (ObjectTele != null && EndPointTele != null)
        {
            ObjectTele.position = EndPointTele.position;
            ObjectTele.rotation = EndPointTele.rotation; // Thêm dòng này để set rotation
        }
        else
        {
            Debug.LogWarning("ObjectTele hoặc EndPointTele chưa được gán.");
        }
    }

}
