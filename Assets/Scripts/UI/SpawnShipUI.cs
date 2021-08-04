using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script attached to ShipSelectUI gameobject. Adds the type of ship when a button is selected.
public class SpawnShipUI : MonoBehaviour
{
    private Player _player;
    private List<GameObject> allShips = new List<GameObject>();

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SpawnShip(string tag)
    {
        if (GameObject.FindGameObjectWithTag(tag) == null)
        {
            ObjectPooler.instance.SetEntirePool("Grid", true);
            ObjectPooler.instance.SpawnFromPool(tag, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("You have already spawned that type of ship!");
        }
    }

}
