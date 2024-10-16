
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

    public override T GetData<T>()
    {
        ItemData.EntityData = base.GetData<EntityData>();
        return (T)(object)ItemData;
    }

    public override void SetData<T>(T data)
    {
        if(data is ItemData itemData) 
        {
            ItemData = itemData;
        }
    }
}
