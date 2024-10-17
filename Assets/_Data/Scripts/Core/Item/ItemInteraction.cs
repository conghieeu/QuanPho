using Codice.CM.WorkspaceServer.Lock;
using QFSW.QC;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public ModuleDragItem PrefabModuleDragItem;

    Item itemSelect;

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

    [Command]
    /// <summary> Tạo item drag với avatar cua chinh item do </summary>
    public void CreateItemDrag()
    {
        if (itemSelect)
        {
            Instantiate(PrefabModuleDragItem).OnCreateModule(itemSelect);

        }
    }

}
