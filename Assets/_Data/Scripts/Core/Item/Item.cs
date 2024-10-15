using Mono.CSharp;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : Entity, IPointerClickHandler
{
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
    
}
