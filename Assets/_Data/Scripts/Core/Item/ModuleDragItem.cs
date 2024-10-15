using System;
using UnityEngine;
using UnityEngine.AI;

public class ModuleDragItem : MonoBehaviour
{
    [SerializeField] Material GreenMaterial, RedMaterial;
    public Vector3 TileOffset;
    public float GridSnap = 1;
    public bool EnableSnap;
    public Transform Models;
    public float RotationSpeed = 1;

    NavMeshManager navMeshManager;
    InputImprove inputImprove;
    Item itemDragging;
    RaycastCursor raycastCursor;

    private void Start()
    {
        raycastCursor = FindFirstObjectByType<RaycastCursor>();
        navMeshManager = FindFirstObjectByType<NavMeshManager>();
        inputImprove = FindFirstObjectByType<InputImprove>();
    }

    private void Update()
    {
        DragItem();
        RotateModels();
        SetMaterial();
    } 

    public void OnCreateModule(Item item)
    {
        itemDragging = item;

        item.QuickOutline.SetActiveOutLine(false);
        Avatar itemAvatar = Instantiate(item.Avatar, transform.position, transform.rotation, Models);
        itemAvatar.QuickOutline.SetActiveOutLine(false);
    }

    /// <summary> Button quay item drag sẽ gọi </summary>
    public void OnClickRotation(float angle)
    {
        float currentAngle = Models.localEulerAngles.y;
        float roundedAngle = Mathf.Round(currentAngle / 10.0f) * 10.0f;
        float rotationAngle = Mathf.Round(angle * RotationSpeed / 10.0f) * 10.0f;
        float newAngle = roundedAngle + rotationAngle;
        Models.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    public void DropItem()
    {
        if (IsCanDrop())
        {
            Destroy(this); // Delete model item
            navMeshManager.RebuildNavMeshes();
            itemDragging.transform.position = transform.position;
            itemDragging.transform.rotation = Models.localRotation;
            itemDragging.gameObject.SetActive(true);
        }
    }

    public void DragItem()
    {
        Vector3 mouseRayHitPoint = raycastCursor.GetRaycastMouseHit().point;

        //  Làm tròn vị trí temp để nó giống snap
        if (EnableSnap)
        {
            float sX = Mathf.Round(mouseRayHitPoint.x / GridSnap) * GridSnap + TileOffset.x;
            float sZ = Mathf.Round(mouseRayHitPoint.z / GridSnap) * GridSnap + TileOffset.z;
            float sY = Mathf.Round(mouseRayHitPoint.y / GridSnap) * GridSnap + TileOffset.y;
            mouseRayHitPoint = new Vector3(sX, sY, sZ);
        } 

        transform.position = mouseRayHitPoint;
    }

    /// <summary> Xoay item </summary>
    private void RotateModels()
    {
        // Model holder: lấy góc xoay mới
        float currentAngle = Mathf.Round(Models.localEulerAngles.y);
        float newAngle = currentAngle + (inputImprove.GetScrollWheel() * RotationSpeed);

        Models.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    private void SetMaterial()
    {
        Material material = GreenMaterial;

        if (IsCanDrop() == false)
        {
            material = RedMaterial;
        }

        foreach (Renderer model in Models.GetComponentsInChildren<Renderer>())
        {
            model.material = material;
        }
    }

    private bool IsCanDrop()
    {
        return GetComponentInChildren<ItemDropChecker>().IsAgreeToDrag();
    }
}
