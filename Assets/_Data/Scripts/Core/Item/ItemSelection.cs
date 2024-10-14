using UnityEngine;

public class ItemSelection : MonoBehaviour
{
    public Item ItemSelect
    {
        get => itemSelect;
        set
        {
            if (itemSelect) itemSelect.QuickOutline.SetActiveOutLine(false);
            if (value) value.QuickOutline.SetActiveOutLine(true);

            itemSelect = value;
        }
    }

    Item itemSelect;

}
