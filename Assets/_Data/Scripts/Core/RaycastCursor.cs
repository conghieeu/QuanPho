using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastCursor : MonoBehaviour
{ 
     public LayerMask LayerMask;

     InputImprove inputImprove;
     Camera cam;
     ItemSelection itemSelection;

     void Start()
     {
          itemSelection = FindFirstObjectByType<ItemSelection>();
          inputImprove = FindFirstObjectByType<InputImprove>();
          cam = Camera.main;

          inputImprove.UIClick.action.performed += ctx => OnClick();
     }

     private void OnClick()
     {
          Transform tHit = GetRaycastMouseHit().transform;
          if (tHit)
          {
               if (tHit.GetComponent<Item>())  // click on item
               {
                    itemSelection.ItemSelect = tHit.GetComponent<Item>();
                    return;
               }

          }
          else // click empty 
          {
               
          }

          itemSelection.ItemSelect = null;
     }

     /// <summary> Lấy thông tin va chạm của tia ray từ vị trí chuột trên màn hình </summary>
     public RaycastHit GetRaycastMouseHit()
     {
          RaycastHit hit = new();
          Vector2 mousePoint = inputImprove.GetMousePoint();
          Ray ray = cam.ScreenPointToRay(mousePoint);
          Physics.Raycast(ray, out hit, 100, LayerMask);
          return hit;
     }
}
