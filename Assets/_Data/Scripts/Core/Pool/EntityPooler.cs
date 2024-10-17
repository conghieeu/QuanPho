using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EntityPooler : MonoBehaviour
{
    [SerializeField] List<Transform> prefabs;
    [SerializeField] List<Entity> entities;

    public List<Entity> Entities
    {
        get => entities;
        set
        {
            entities = value;
            OnPoolChange?.Invoke();
        }
    }

    public UnityAction OnPoolChange;

    private void OnValidate()
    {
        // Load item co sang trong pool
        Entities.Clear();
        foreach (Transform child in transform)
        {
            Entities.Add(child.GetComponent<Entity>());
        }
    }

    /// <summary> Xoá object khỏi pool và đánh dấu là có thể tái sử dụng  </summary>
    public virtual void DisableEntity(Entity entity)
    {
        entity.gameObject.SetActive(false);
    }

    public Entity ReloadEntityByEntityData(EntityData entityData)
    {
        Entity entity = GetEntityByID(entityData.ID);

        if (entity) // id co san
        {
            entity.GetComponent<Entity>().SetData(entityData);
        }
        else // id chua co
        {
            GetOrCreateObjectPool(entityData.TypeID).SetData(entityData);
        }

        return entity;
    }

    /// <summary> Kiểm tra xem pool có chứa entity với ID cụ thể hay không  </summary>
    public virtual bool IsContainEntityByID(string id)
    {
        foreach (var entity in Entities)
        {
            if (entity.EntityData.ID == id)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary> Lấy entity từ pool theo ID </summary>
    public virtual Entity GetEntityByID(string id)
    {
        if (id == "") return null;

        foreach (var entity in Entities)
        {
            if (entity.EntityData.ID == id) return entity;
        }
        return null;
    }

    /// <summary> Tái sử dụng object nhàn rỗi hoặc tạo mới object từ pool, default(Vector3) là mặc định sẽ là Vector.Zero </summary>
    public Entity GetOrCreateObjectPool(TypeID typeID, Vector3 spawnPosition = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        Entity entity = GetReusableEntity(typeID); // tai su dung

        if (entity == null)
        {
            entity = CreateNewEntity(typeID); // tao cai moi
        }

        if (entity)
        {
            entity.transform.rotation = rotation;
            entity.transform.position = spawnPosition;
            entity.IsRecyclable = false;
            entity.gameObject.SetActive(true);

            entity.GenerateIdentifier();
            return entity;
        }
        else
        {
            Debug.LogWarning($"Item với typeID {typeID} tạo từ pool không thành công");
            return null;
        }
    }

    /// <summary> Tạo entity mới </summary>
    private Entity CreateNewEntity(TypeID typeID)
    {
        foreach (var prefab in prefabs)
        {
            Entity entity = prefab.GetComponent<Entity>();

            if (entity && entity.EntityData.TypeID == typeID)
            {
                entity = Instantiate(entity, transform);
                Entities.Add(entity);
                return entity;
            }
        }

        return null;
    }

    /// <summary> Tìm object nhàn rỗi trong pool theo typeID  </summary>
    private Entity GetReusableEntity(TypeID typeID)
    {
        foreach (var entity in Entities)
        {
            if (entity.EntityData.TypeID == typeID && entity.IsRecyclable)
            {
                return entity;
            }
        }
        return null;
    }

}
