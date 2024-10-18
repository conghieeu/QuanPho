using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    public List<NavMeshSurface> NavMeshSurfaces = new List<NavMeshSurface>();

    SaveLoadManager saveLoadManager;

    private void Start()
    {
        saveLoadManager = FindFirstObjectByType<SaveLoadManager>();
        saveLoadManager.OnDataLoad += SaveLoadManager_OnDataLoad;
    }

    private void SaveLoadManager_OnDataLoad()
    { 
        RebuildNavMeshes();
    }

    private void OnValidate()
    {
        Init();
    }

    private void Init()
    {
        NavMeshSurfaces.Clear();
        NavMeshSurface[] childNavMeshSurfaces = GetComponentsInChildren<NavMeshSurface>();
        foreach (NavMeshSurface surface in childNavMeshSurfaces)
        {
            NavMeshSurfaces.Add(surface);
        }
    }

    public void RebuildNavMeshes()
    {
        foreach (NavMeshSurface navMeshSurface in NavMeshSurfaces)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
