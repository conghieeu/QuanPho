using System;
using Codice.CM.WorkspaceServer.Lock;
using QFSW.QC;
using UnityEngine;

public class PlayerInteractItem : MonoBehaviour
{

    [Header("Drag Item")]
    public Transform PointHoldItem;
    public ModuleDragItem PrefabModuleDragItem;
    public ModuleDragItem ModuleItemDragging;

    InputImprove inputImprove;
    Transform avatarItemDrag;
    Item itemSelecting; 
    NavMeshManager navMeshManager;

    public Item ItemSelect
    {
        get => itemSelecting;
        set
        {
            if (itemSelecting) itemSelecting.QuickOutline.SetActiveOutLine(false);
            if (value) value.QuickOutline.SetActiveOutLine(true);

            itemSelecting = value;
        }
    }

    private void Start()
    {
        inputImprove = FindFirstObjectByType<InputImprove>();
        navMeshManager = FindFirstObjectByType<NavMeshManager>();

        inputImprove.UIClick.action.performed += ctx => DropItemSelecting();
    }

    private void DropItemSelecting()
    {
        if (ModuleItemDragging)
        {
            ModuleItemDragging.DropItem();
            Destroy(avatarItemDrag.gameObject);

            // rebuild AI surface
            navMeshManager.RebuildNavMeshes();
        }
    }

    [Command("/Drag/CreateItemDragOnItemSelecting")]
    /// <summary> Tạo item drag với avatar cua chinh item do </summary>
    public void DragItemSelecting()
    {
        // create module drag item
        if (itemSelecting)
        {
            ModuleItemDragging = Instantiate(PrefabModuleDragItem);
            ModuleItemDragging.OnCreateModule(itemSelecting);

            // create model drag item on point drag 
            avatarItemDrag = Instantiate(itemSelecting.Avatar.transform, PointHoldItem.position, PointHoldItem.rotation, PointHoldItem);
        }
    }

}
