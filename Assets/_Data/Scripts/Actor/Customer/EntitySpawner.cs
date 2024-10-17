using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

/// <summary> Đảm nhiệm việc spawn customer ở đâu và như thế nào </summary>
public class EntitySpawner : MonoBehaviour
{
    [SerializeField] List<Entity> entitySpawn;
    [SerializeField] List<Transform> spawnPoint;
    [SerializeField] List<Transform> despawnPoints;
    [SerializeField] EntityPooler entityPooler; // là pool của entitySpawn
    [SerializeField] float randomRange = 10f; // Khoảng thời gian ngẫu nhiên thêm vào
    [SerializeField] float spawnTime = 10f; // Thời gian spawn hiện tại
    [SerializeField] float currentSpawnTime; // Bộ đếm thời gian cho spawn khách hàng 

    private void Awake()
    {
        currentSpawnTime = spawnTime;
    }

    private void FixedUpdate()
    {
        if (IsCanSpawn())
        {
            SpawnEntityOverTime();
        }
    }

    /// <summary> Lấy một điểm ngẫu nhiên từ danh sách điểm thoát </summary>
    public Transform GetRandomOutPoint()
    {
        if (despawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, despawnPoints.Count);
            return despawnPoints[randomIndex];
        }

        return null;
    }

    /// <summary> Spawn khách hàng muốn mua item </summary>
    private void SpawnEntityOverTime()
    {
        currentSpawnTime -= Time.fixedDeltaTime;

        if (currentSpawnTime <= 0)
        {
            // Đặt lại bộ đếm thời gian với yếu tố ngẫu nhiên
            currentSpawnTime = spawnTime + Random.Range(-randomRange, randomRange);

            if (entitySpawn.Count > 0 && spawnPoint.Count > 0)
            {
                SpawnRandomEntity();
            }
        }
    }

    private Entity SpawnRandomEntity()
    {
        int randomSpawnObject = Random.Range(0, entitySpawn.Count);
        int randomSpawnPoint = Random.Range(0, spawnPoint.Count);

        Entity customer = entityPooler.GetOrCreateObjectPool(entitySpawn[randomSpawnObject].EntityData.TypeID, spawnPoint[randomSpawnPoint].position);

        return customer;
    }

    [Command]
    /// <summary> Thuật toán chỉnh tỷ lệ spawn </summary>
    private void SetSpawnTimeRate(float reputation)
    {

    }

    private bool IsCanSpawn()
    {
        if (entitySpawn.Count > 0 && entityPooler && spawnPoint.Count > 0 && despawnPoints.Count > 0) return true;
        // Debug.LogWarning($"Khong dap ung dieu kien spawn", transform);
        return false;
    }
}
