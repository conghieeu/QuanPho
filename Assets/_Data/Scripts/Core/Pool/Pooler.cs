using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Pooler : MonoBehaviour
{
    [SerializeField] List<Transform> prefabs;
    [SerializeField] List<Entity> entities;

    public UnityAction OnPoolChange;

    public List<Entity> Entities { get => ObjectPools; private set => ObjectPools = value; }

    protected List<Entity> ObjectPools
    {
        get => entities;
        set
        {
            entities = value;
            OnPoolChange?.Invoke();
        }
    }

    private void OnValidate()
    {
        // Load item co sang trong pool
        ObjectPools.Clear();
        foreach (Transform child in transform)
        {
            ObjectPools.Add(child.GetComponent<Entity>());
        }
    }

    /// <summary> Xoá object khỏi pool và đánh dấu là có thể tái sử dụng  </summary>
    public virtual void DisableEntity(Entity entity)
    {
        entity.gameObject.SetActive(false);
    }

    /// <summary> Kiểm tra xem pool có chứa object với ID cụ thể hay không  </summary>
    public virtual bool ContainsID(string id)
    {
        foreach (var obj in Entities)
        {
            if (obj.ID == id) return true;
        }
        return false;
    }

    /// <summary> Lấy object từ pool theo ID </summary>
    public virtual Entity GetObjectByID(string id)
    {
        if (id == "") return null;

        foreach (var obj in Entities)
        {
            if (obj.ID == id) return obj;
        }
        return null;
    }

    /// <summary> Tái sử dụng object nhàn rỗi hoặc tạo mới object từ pool, default(Vector3) là mặc định sẽ là Vector.Zero </summary>
    public Entity GetOrCreateObjectPool(EntityType typeID, Vector3 spawnPosition = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        Entity entity = GetReusableObject(typeID);

        if (entity) // tái sử dụng
        {
            entity.transform.rotation = rotation;
            entity.transform.position = spawnPosition;
            entity.IsRecyclable = false;
            entity.gameObject.SetActive(true);
        }
        else // Create New 
        {
            foreach (var prefab in prefabs)
            {
                Entity entity = prefab.GetComponent<Entity>();

                if (entity && entity.TypeID == typeID)
                {
                    entity = Instantiate(entity, spawnPosition, rotation, transform);
                    ObjectPools.Add(entity);
                    break;
                }
            }
        }

        if (entity)
        {
            entity.GenerateIdentifier();
            return entity;
        }
        else
        {
            Debug.LogWarning($"Item {typeID} Này Tạo từ pool không thành công");
            return null;
        }
    }

    /// <summary> Tìm object nhàn rỗi trong pool theo typeID  </summary>
    private Entity GetReusableObject(EntityType typeID)
    {
        foreach (var objectPool in ObjectPools)
        {
            if (objectPool.TypeID == typeID && objectPool.IsRecyclable)
            {
                return objectPool;
            }
        }
        return null;
    }

}
