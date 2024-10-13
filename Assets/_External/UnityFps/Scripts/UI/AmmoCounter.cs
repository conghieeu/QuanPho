using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    /// <summary>
    /// Class quản lý hiển thị đạn trên UI
    /// </summary>
    [RequireComponent(typeof(FillBarColorChange))]
    public class AmmoCounter : MonoBehaviour
    {
        [Tooltip("CanvasGroup để làm mờ UI đạn")]
        public CanvasGroup CanvasGroup;

        [Tooltip("Hình ảnh cho biểu tượng vũ khí")] public Image WeaponImage;

        [Tooltip("Hình ảnh nền cho đạn")]
        public Image AmmoBackgroundImage;

        [Tooltip("Hình ảnh để hiển thị tỷ lệ đạn")]
        public Image AmmoFillImage;

        [Tooltip("Text cho chỉ số vũ khí")] 
        public TextMeshProUGUI WeaponIndexText;

        [Tooltip("Text cho bộ đếm đạn")] 
        public TextMeshProUGUI BulletCounter;

        [Tooltip("Text nạp đạn cho vũ khí có đạn vật lý")]
        public RectTransform Reload;

        [Header("Selection")] [Range(0, 1)] [Tooltip("Độ mờ khi vũ khí không được chọn")]
        public float UnselectedOpacity = 0.5f;

        [Tooltip("Tỷ lệ khi vũ khí không được chọn")]
        public Vector3 UnselectedScale = Vector3.one * 0.8f;

        [Tooltip("Gốc cho các phím điều khiển")] public GameObject ControlKeysRoot;

        [Header("Feedback")] [Tooltip("Thành phần để thay đổi màu khi đầy hoặc hết đạn")]
        public FillBarColorChange FillBarColorChange;

        [Tooltip("Độ sắc nét cho các chuyển động tỷ lệ đạn")]
        public float AmmoFillMovementSharpness = 20f;

        public int WeaponCounterIndex { get; set; }

        PlayerWeaponsManager m_PlayerWeaponsManager;
        WeaponController m_Weapon;

        /// <summary>
        /// Hàm Awake được gọi khi đối tượng được khởi tạo
        /// </summary>
        void Awake()
        {
            EventManager.AddListener<AmmoPickupEvent>(OnAmmoPickup);
        }

        /// <summary>
        /// Hàm xử lý sự kiện nhặt đạn
        /// </summary>
        /// <param name="evt">Sự kiện nhặt đạn</param>
        void OnAmmoPickup(AmmoPickupEvent evt)
        {
            if (evt.Weapon == m_Weapon)
            {
                BulletCounter.text = m_Weapon.GetCarriedPhysicalBullets().ToString();
            }
        }

        /// <summary>
        /// Hàm khởi tạo đối tượng AmmoCounter
        /// </summary>
        /// <param name="weapon">Vũ khí</param>
        /// <param name="weaponIndex">Chỉ số vũ khí</param>
        public void Initialize(WeaponController weapon, int weaponIndex)
        {
            m_Weapon = weapon;
            WeaponCounterIndex = weaponIndex;
            WeaponImage.sprite = weapon.WeaponIcon;
            if (!weapon.HasPhysicalBullets)
                BulletCounter.transform.parent.gameObject.SetActive(false);
            else
                BulletCounter.text = weapon.GetCarriedPhysicalBullets().ToString();

            Reload.gameObject.SetActive(false);
            m_PlayerWeaponsManager = FindFirstObjectByType<PlayerWeaponsManager>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, AmmoCounter>(m_PlayerWeaponsManager, this);

            WeaponIndexText.text = (WeaponCounterIndex + 1).ToString();

            FillBarColorChange.Initialize(1f, m_Weapon.GetAmmoNeededToShoot());
        }

        /// <summary>
        /// Hàm Update được gọi mỗi khung hình
        /// </summary>
        void Update()
        {
            float currenFillRatio = m_Weapon.CurrentAmmoRatio;
            AmmoFillImage.fillAmount = Mathf.Lerp(AmmoFillImage.fillAmount, currenFillRatio,
                Time.deltaTime * AmmoFillMovementSharpness);

            BulletCounter.text = m_Weapon.GetCarriedPhysicalBullets().ToString();

            bool isActiveWeapon = m_Weapon == m_PlayerWeaponsManager.GetActiveWeapon();

            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, isActiveWeapon ? 1f : UnselectedOpacity,
                Time.deltaTime * 10);
            transform.localScale = Vector3.Lerp(transform.localScale, isActiveWeapon ? Vector3.one : UnselectedScale,
                Time.deltaTime * 10);
            ControlKeysRoot.SetActive(!isActiveWeapon);

            FillBarColorChange.UpdateVisual(currenFillRatio);

            Reload.gameObject.SetActive(m_Weapon.GetCarriedPhysicalBullets() > 0 && m_Weapon.GetCurrentAmmo() == 0 && m_Weapon.IsWeaponActive);
        }

        /// <summary>
        /// Hàm Destroy được gọi khi đối tượng bị hủy
        /// </summary>
        void Destroy()
        {
            EventManager.RemoveListener<AmmoPickupEvent>(OnAmmoPickup);
        }
    }
}