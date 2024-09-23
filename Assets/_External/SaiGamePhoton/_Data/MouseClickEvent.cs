using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaiGame
{
    public class MouseClickEvent : MonoBehaviour
    {
        protected void OnMouseDown()
        {
            PhotonNumber photonNumber = transform.parent.GetComponent<PhotonNumber>();
            photonNumber.OnClaim();
        }
    }
}