using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////Script attached to Grid gameobject. Spawns a grid to be used.

public class GridSpawner : MonoBehaviour
{
    public int gridSizeX;
    public int gridSizeZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;


    private void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for(int x=0; x<gridSizeX; x++)
        {
            for(int z=0; z<gridSizeZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }

        ObjectPooler.instance.SetEntirePool("Grid", false);
    }

    private void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        ObjectPooler.instance.SpawnFromPool("Grid", positionToSpawn, rotationToSpawn);
    }

}
