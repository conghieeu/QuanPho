using System;
using UnityEngine;

/// <summary> Đảm nhiệm việc kiểm soát item và tạo ra item gì </summary>
public class ItemPooler : EntityPooler
{
    public void ReloadItemByItemData(ItemData itemData)
    { 
        Entity entity = ReloadEntityByEntityData(itemData.EntityData);
        if (entity)
        { 
            entity.GetComponent<Item>().SetData(itemData);
        }
    }
}
