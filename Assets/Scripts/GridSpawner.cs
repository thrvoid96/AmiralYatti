using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////Script attached to Grid gameobject. Spawns a grid to be used.

public class GridSpawner : MonoBehaviour
{
    public List<GameObject> ItemsToPickFrom;
    public List<GameObject> spawnedObjects;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;


    private void Awake()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for(int x=0; x<gridX; x++)
        {
            for(int z=0; z<gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    private void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, ItemsToPickFrom.Capacity);
        GameObject clone = Instantiate(ItemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
        spawnedObjects.Add(clone); 
    }

    public void setVisibility(bool value)
    {
        for(int i=0; i< spawnedObjects.Count ; i++)
        {
            spawnedObjects[i].SetActive(value);
        }        

    }
}
