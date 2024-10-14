using Mono.CSharp;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : Entity, IPointerClickHandler
{
    public QuickOutline QuickOutline;

    private void OnValidate()
    {
        QuickOutline = GetComponent<QuickOutline>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        QuickOutline.SetActiveOutLine(true);
    }
    
}
