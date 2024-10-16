using System;
using UnityEngine;

/// <summary> Đảm nhiệm việc kiểm soát item và tạo ra item gì </summary>
public class ItemPooler : EntityPooler
{
    public void CreateItemByItemData(ItemData itemData)
    {
        Entity entity = GetEntityByID(itemData.EntityData.ID); 

        if (entity) // id co san
        { 
            entity.GetComponent<Item>().SetData<ItemData>(itemData);
        }
        else // id chua co
        {
            GetOrCreateObjectPool(itemData.EntityData.TypeID).GetComponent<Item>().SetData<ItemData>(itemData);
        }

    }
}
