using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CuaHang.UI
{
    [RequireComponent(typeof(Button))]
    public class BtnSwitch : MonoBehaviour
    {
        [SerializeField] GameObject panelOpen;
        [SerializeField] List<GameObject> panelsClose;

        Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OpenPanels(); 
        }

        private void OpenPanels()
        {
            foreach (var panel in panelsClose)
            {
                if (panel != null) // Kiểm tra xem đối tượng có khác null không
                {
                    panel.SetActive(false); // Tắt đối tượng
                }
            }

            if (panelOpen) panelOpen.SetActive(true);
        } 
    }

}
