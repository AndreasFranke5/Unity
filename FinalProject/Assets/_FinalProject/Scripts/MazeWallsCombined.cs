using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        // Get all MeshFilter components in children of this object
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        // Create a combined mesh for the parent object
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);

        // Add a MeshRenderer
        gameObject.AddComponent<MeshRenderer>();

        // Make the parent object static
        gameObject.isStatic = true;

        // Optionally add a collider (you can choose BoxCollider or MeshCollider)
        gameObject.AddComponent<MeshCollider>();
    }
}
