using Photon.Pun;
using TMPro;
using UnityEngine;

namespace SaiGame
{
    public class PhotonPlayer : MonoBehaviourPun, IPunObservable
    {
        public static PhotonPlayer me;
        public TextMeshPro nickNameLable;
        public string photonNickName = "offline";
        public int numberCount = 0;

        [SerializeField] Camera _camera;

        PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _camera = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            this.OwnerController();
            this.LoadOwnerNickName();
        }

        // Hàm này được gọi để đồng bộ hóa dữ liệu giữa các client
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            Debug.Log("OnPhotonSerializeView");
            if (stream.IsWriting) // Nếu là người gửi dữ liệu
            {
                this.StreamWriting(stream);
            }
            else // Nếu là người nhận dữ liệu
            {
                this.StreamReading(stream, info);
            }
        }

        // Gửi dữ liệu đến các client khác
        protected virtual void StreamWriting(PhotonStream stream)
        {
            stream.SendNext(this.numberCount);
        }

        // Nhận dữ liệu từ các client khác
        protected virtual void StreamReading(PhotonStream stream, PhotonMessageInfo info)
        {
            this.numberCount = (int)stream.ReceiveNext();
        }

        // Kiểm tra và điều khiển đối tượng nếu nó thuộc về người chơi hiện tại
        protected virtual void OwnerController()
        {
            if (this._photonView.ViewID != 0 && !this._photonView.IsMine)
            {
                _camera.gameObject.SetActive(false);
            }
            else
            {
                // this.FollowMousePos();
                _camera.gameObject.SetActive(true);
            }
        }

        // Tải và hiển thị tên người chơi
        protected virtual void LoadOwnerNickName()
        {
            this.nickNameLable.text = this.photonNickName + ": " + this.numberCount;
            if (this._photonView.ViewID == 0) return;
            this.photonNickName = this._photonView.Owner.NickName;
        }

        // Di chuyển đối tượng theo vị trí chuột
        protected virtual void FollowMousePos()
        {
            Vector3 mouseInput = Input.mousePosition;
            mouseInput.z = Camera.main.nearClipPlane;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseInput);
            Vector3 xPosLimit = GameManager.instance.xPosLimit;
            Vector3 yPosLimit = GameManager.instance.yPosLimit;
            Vector3 newPos = mousePos;
            newPos.z = 0;

            if (newPos.x > xPosLimit.y) newPos.x = xPosLimit.y;
            if (newPos.x < xPosLimit.x) newPos.x = xPosLimit.x;

            if (newPos.y > yPosLimit.y) newPos.y = yPosLimit.y;
            if (newPos.y < yPosLimit.x) newPos.y = yPosLimit.x;

            transform.position = newPos;
        }

        // Hàm này được gọi để phá hủy đối tượng nếu nó thuộc về người chơi hiện tại
        protected virtual void DestroyPlayer()
        {
            if (this._photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

    }

}