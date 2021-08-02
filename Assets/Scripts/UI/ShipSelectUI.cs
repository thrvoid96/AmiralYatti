using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script attached to ShipSelectUI gameobject. Adds the type of ship when a button is selected.
public class ShipSelectUI : MonoBehaviour
{
    private Player _player;
    private List<GameObject> allShips = new List<GameObject>();

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SelectShip(string tag)
    {
        for(int i=0; i < _player.shipsOnScene.Count; i++)
        {
            if (_player.shipsOnScene[i].CompareTag(tag))
            {
                allShips.Add(_player.shipsOnScene[i]);
            }
        }

        _player.shipToMove = allShips[0];
        _player.shipToMove.SetActive(true);

        _player.shipToPlace = allShips[1];
        _player.shipToPlace.SetActive(false);

        allShips.Clear();
    }

    
}
