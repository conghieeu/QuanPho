using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    public List<NavMeshSurface> _navMeshSurfaces = new List<NavMeshSurface>(); 

    public void RebuildNavMeshes()
    {
        foreach (NavMeshSurface navMeshSurface in _navMeshSurfaces)
        {
            if (navMeshSurface != null)
            {
                navMeshSurface.BuildNavMesh();
            }
        }
    }
}
