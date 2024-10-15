
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuaHang
{
    /// <summary> Sử dụng Physics.BoxCastAll để phát hiện va chạm </summary>
    public class SensorCast : MonoBehaviour
    {
        public List<Transform> TransformHits;
        public LayerMask LayerMask;
        public Vector3 Size;

        private void Update()
        {
            GetCastHits();
        }

        /// <summary> Gọi liên tục để lấy va chạm </summary>
        public List<RaycastHit> GetCastHits()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, Size / 2f, transform.forward, transform.rotation, 0f, LayerMask);
            TransformHits = hits.Select(hit => hit.transform).ToList();
            return hits.ToList();
        }

        // Vẽ box hit ra khi click vào thì thấy được box hit
        private void OnDrawGizmosSelected()
        {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Size);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Size);
        }
    }
}