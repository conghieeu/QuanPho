using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Unity.FPS.Game
{
    /// <summary>
    /// Lớp này thay thế các instance của một prefab cụ thể trong cảnh với một đối tượng khác.
    /// </summary>
    [ExecuteInEditMode]
    public class PrefabReplacerOnInstance : MonoBehaviour
    {
        public GameObject TargetPrefab; // Prefab mục tiêu cần thay thế

        /// <summary>
        /// Hàm Awake được gọi khi đối tượng được khởi tạo. 
        /// Trong chế độ Editor, nó tìm kiếm tất cả các instance của prefab trong cảnh và thay thế chúng nếu khớp với TargetPrefab.
        /// </summary>
        void Awake()
        {
#if UNITY_EDITOR
            List<GameObject> allPrefabObjectsInScene = new List<GameObject>();
            // Tìm tất cả các đối tượng Transform trong cảnh
            foreach (Transform t in GameObject.FindObjectsByType<Transform>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                // Kiểm tra xem đối tượng có phải là gốc của một instance prefab không
                if (PrefabUtility.IsAnyPrefabInstanceRoot(t.gameObject))
                {
                    allPrefabObjectsInScene.Add(t.gameObject);
                }
            }

            // Duyệt qua tất cả các đối tượng prefab trong cảnh
            foreach (GameObject go in allPrefabObjectsInScene)
            {
                // Lấy đối tượng nguồn của instance prefab
                GameObject instanceSource = PrefabUtility.GetCorrespondingObjectFromSource(go);

                // Kiểm tra nếu nguồn của instance khớp với TargetPrefab
                if (instanceSource == TargetPrefab)
                {
                    // Đặt đối tượng hiện tại vào vị trí của đối tượng cần thay thế
                    transform.SetParent(go.transform.parent);
                    transform.position = go.transform.position;
                    transform.rotation = go.transform.rotation;
                    transform.localScale = go.transform.localScale;

                    // Đăng ký hành động hủy đối tượng cho Undo
                    Undo.DestroyObjectImmediate(go);

                    Debug.Log("Replaced prefab in scene");
                    DestroyImmediate(this); // Hủy đối tượng hiện tại sau khi thay thế
                    break;
                }
            }
#endif
        }
    }
}
