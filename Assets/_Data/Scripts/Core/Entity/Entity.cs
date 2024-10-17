using UnityEngine;
public class Entity : MonoBehaviour, ISaveData
{
    public EntityData EntityData;
    public bool IsRecyclable;

    [ContextMenu("Generate Identifier")]
    public void GenerateIdentifier()
    {
        EntityData.ID = System.Guid.NewGuid().ToString();
    }

    public void DisableEntity()
    {
        IsRecyclable = true;
        gameObject.SetActive(false);
    }


    public virtual T GetData<T>()
    {
        EntityData.Position = transform.position;
        EntityData.Rotation = transform.rotation;
        return (T)(object)EntityData;
    }

    public virtual void SetData<T>(T data)
    {
        if (data is EntityData entityData && entityData.ID == EntityData.ID)
        {
            EntityData = entityData;
        }
        else
        {
            return;
        }
    }

    public virtual void LoadData()
    {
        transform.position = EntityData.Position;
        transform.rotation = EntityData.Rotation;

        if (EntityData.ParentID != "")
        {
            Debug.Log("Thong bao can thuc hien set item cha cho item nay", transform);
        }
    }
}
