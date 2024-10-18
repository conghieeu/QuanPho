using System.Collections.Generic;
using System.Linq;
using Mono.CSharp; 
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public Transform Models;
    public QuickOutline QuickOutline;

    private void Start()
    {
        QuickOutline = GetComponent<QuickOutline>();
    }
  
    public List<Mesh> GetAllChildMeshes()
    {
        List<Mesh> childMeshes = new List<Mesh>();
        foreach (Transform child in transform)
        {
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                childMeshes.Add(meshFilter.sharedMesh);
            }
        }
        return childMeshes;
    }
}
