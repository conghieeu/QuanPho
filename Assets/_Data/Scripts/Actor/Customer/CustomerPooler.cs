using System;
using UnityEngine;

public class CustomerPooler : EntityPooler
{
    public void CreateCustomerByItemData(CustomerData customerData)
    {
        Entity entity = GetEntityByID(customerData.EntityData.ID);

        if (entity) // id co san
        {
            entity.GetComponent<Customer>().SetData(customerData);
        }
        else // id chua co
        {
            GetOrCreateObjectPool(customerData.EntityData.TypeID).GetComponent<Customer>().SetData(customerData);
        }
    }
}
