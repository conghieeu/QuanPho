using System.Collections.Generic;
using UnityEngine;

namespace Unity.FPS.Game
{
    // Lớp này chịu trách nhiệm thay thế các prefab trong game
    public class PrefabReplacer : MonoBehaviour
    {
        // Định nghĩa một cấu trúc để lưu trữ thông tin về sự thay thế prefab
        [System.Serializable]
        public struct ReplacementDefinition
        {
            // Prefab nguồn cần được thay thế
            public GameObject SourcePrefab;
            // Prefab đích sẽ thay thế prefab nguồn
            public GameObject TargetPrefab;
        }

        // Biến để xác định có cần đảo ngược thứ tự thay thế hay không
        public bool SwitchOrder;
        // Danh sách các định nghĩa thay thế, chứa các cặp SourcePrefab và TargetPrefab
        public List<ReplacementDefinition> Replacements = new List<ReplacementDefinition>();
    }
}
