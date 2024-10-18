using System;
using UnityEngine;
using UnityEngine.AI;

public class ModuleDragItem : MonoBehaviour
{
    [SerializeField] Material GreenMaterial, RedMaterial;
    public Vector3 TileOffset;
    public float GridSnap = 1;
    public bool EnableSnap;
    public Transform Model;
    public float RotationSpeed = 1;

    NavMeshManager navMeshManager;
    InputImprove inputImprove;
    Item itemDragging;
    PlayerCursorRay raycastCursor;
    ItemDropChecker itemDropChecker;

    private void Start()
    {
        raycastCursor = FindFirstObjectByType<PlayerCursorRay>();
        navMeshManager = FindFirstObjectByType<NavMeshManager>();
        inputImprove = FindFirstObjectByType<InputImprove>();
        itemDropChecker = GetComponentInChildren<ItemDropChecker>(); 
    }

    private void Update()
    {
        DragItem();
        RotateModels();
        SetMaterial();
    }

    public void OnCreateModule(Item item)
    {
        if (item == null) return;

        itemDragging = item;
        item.gameObject.SetActive(false);
        item.QuickOutline.SetActiveOutLine(false);
        Instantiate(item.Avatar, transform.position, transform.rotation, Model);
    }

    /// <summary> Button quay item drag sẽ gọi </summary>
    public void OnClickRotation(float angle)
    {
        float currentAngle = Model.localEulerAngles.y;
        float roundedAngle = Mathf.Round(currentAngle / 10.0f) * 10.0f;
        float rotationAngle = Mathf.Round(angle * RotationSpeed / 10.0f) * 10.0f;
        float newAngle = roundedAngle + rotationAngle;
        Model.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    public void DropItem()
    {
        if (this == null) return;

        if (IsCanDrop())
        {
            itemDragging.transform.position = transform.position;
            itemDragging.transform.rotation = Model.localRotation;
            itemDragging.gameObject.SetActive(true);
            navMeshManager.RebuildNavMeshes();
            Destroy(gameObject);
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
        float currentAngle = Mathf.Round(Model.localEulerAngles.y);
        float newAngle = currentAngle + (inputImprove.GetScrollWheel().y * RotationSpeed);

        Model.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    private void SetMaterial()
    {
        Material material = GreenMaterial;

        if (IsCanDrop() == false)
        {
            material = RedMaterial;
        }

        foreach (Renderer model in Model.GetComponentsInChildren<Renderer>())
        {
            model.material = material;
        }
    }

    private bool IsCanDrop()
    {
        if (itemDropChecker && itemDropChecker.IsAgreeToDrag()) return true;
        return false;
    }
}
