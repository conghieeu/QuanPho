using System;
using UnityEngine;

public class CustomerPooler : EntityPooler
{
    public void ReloadItemByItemData(CustomerData customerData)
    {
        Entity entity = ReloadEntityByEntityData(customerData.EntityData);
        if (entity)
        {
            entity.GetComponent<Customer>().SetData(customerData);
        }
    }
}
