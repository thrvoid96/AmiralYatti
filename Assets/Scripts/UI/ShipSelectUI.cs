using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelectUI : MonoBehaviour
{
    private Player _player;
    private List<GameObject> allShips = new List<GameObject>();

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SelectBattleship()
    {
        for(int i=0; i < _player.shipsOnScene.Count; i++)
        {
            if (_player.shipsOnScene[i].CompareTag("Battleship"))
            {
                allShips.Add(_player.shipsOnScene[i]);
            }
        }

        _player.shipToMove = allShips[0];
        _player.shipToMove.SetActive(true);

        _player.shipToPlace = allShips[1];
        _player.shipToPlace.SetActive(false);

        _player.shipSelected = true;

        allShips.Clear();
    }

    public void SelectCruiser()
    {
        for (int i = 0; i < _player.shipsOnScene.Count; i++)
        {
            if (_player.shipsOnScene[i].CompareTag("Cruiser"))
            {
                allShips.Add(_player.shipsOnScene[i]);
            }
        }

        _player.shipToMove = allShips[0];
        _player.shipToMove.SetActive(true);

        _player.shipToPlace = allShips[1];
        _player.shipToPlace.SetActive(false);

        _player.shipSelected = true;

        allShips.Clear();
    }

    public void SelectDestroyer()
    {
        for (int i = 0; i < _player.shipsOnScene.Count; i++)
        {
            if (_player.shipsOnScene[i].CompareTag("Destroyer"))
            {
                allShips.Add(_player.shipsOnScene[i]);
            }
        }

        _player.shipToMove = allShips[0];
        _player.shipToMove.SetActive(true);

        _player.shipToPlace = allShips[1];
        _player.shipToPlace.SetActive(false);

        _player.shipSelected = true;

        allShips.Clear();
    }
}
