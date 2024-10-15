using Codice.CM.WorkspaceServer.Lock;
using QFSW.QC;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public ModuleDragItem PrefabModuleDragItem;

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

    [Command]
    /// <summary> Tạo item drag với avatar cua chinh item do </summary>
    public void CreateItemDrag()
    {
        if (ItemSelect)
        {
            Instantiate(PrefabModuleDragItem).OnCreateModule(itemSelect);
        }
    }

}
