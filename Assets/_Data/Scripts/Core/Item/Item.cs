
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : Entity, IPointerClickHandler
{
    public ItemData ItemData;
    public QuickOutline QuickOutline;
    public Avatar Avatar;

    private void OnValidate()
    {
        QuickOutline = GetComponentInChildren<QuickOutline>();
        Avatar = GetComponentInChildren<Avatar>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        QuickOutline.SetActiveOutLine(true);
    }

    public override void SetData<T>(T data)
    {
        if (data is ItemData itemData)
        {
            base.SetData<EntityData>(itemData.EntityData);
            ItemData = itemData;
        }
    }

    public override T GetData<T>()
    {
        ItemData.EntityData = base.GetData<EntityData>();

        if (ItemData is T data)
        {
            return data;
        }
        else
        {
            return default;
        }
    }
}
