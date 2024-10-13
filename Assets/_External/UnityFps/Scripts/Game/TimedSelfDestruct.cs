using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary> tự động phá hủy một đối tượng (game object) sau một khoảng thời gian nhất định kể từ khi nó được tạo ra. </summary>
    public class TimedSelfDestruct : MonoBehaviour
    {
        // Thời gian tồn tại của đối tượng trước khi bị phá hủy
        public float LifeTime = 1f;

        // Thời điểm đối tượng được tạo ra
        float m_SpawnTime;

        // Hàm Awake được gọi khi đối tượng được khởi tạo
        void Awake()
        {
            // Ghi lại thời điểm đối tượng được tạo ra
            m_SpawnTime = Time.time;
        }

        void Update()
        {
            // Kiểm tra nếu thời gian hiện tại lớn hơn thời gian tạo cộng với thời gian sống
            if (Time.time > m_SpawnTime + LifeTime)
            {
                // Phá hủy đối tượng
                Destroy(gameObject);
            }
        }
    }
}
