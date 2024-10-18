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
        if (typeof(T) == typeof(CustomerData))
        {
            CustomerData.EntityData = base.GetData<EntityData>(); 
            return (T)(object)CustomerData;
        }
        return default;
    }
}
