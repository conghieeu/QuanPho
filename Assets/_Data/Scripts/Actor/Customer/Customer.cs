using UnityEngine;

public class Customer : Entity
{
    public CustomerData CustomerData;

    public override void SetData<T>(T data)
    {
        if (data is CustomerData characterData)
        {
            base.SetData<EntityData>(characterData.EntityData);
            CustomerData = characterData;
        }
    }

    public override T GetData<T>()
    {
        CustomerData.EntityData = base.GetData<EntityData>();

        if (CustomerData is T data)
        {
            return data;
        }
        else
        {
            return default;
        }
    }
}
