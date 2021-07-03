using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacementState : IState
{   
    private RaycastHit hit;
    private Vector3 TouchPos;
    private bool shipPlaced;

    private Player _player;

    public ShipPlacementState(Player player)
    {
        _player = player;
    }
  
    public void Tick()
    {
        if (!shipPlaced)
        {
            MoveShipAround();
            PlaceShip();
        }
        else
        {
            ReplaceShip();
        }
    }

    public void OnEnter()
    {
        _player.shipToMove = _player.InstantiateShip(0);
        _player.shipToMove.SetActive(true);

        _player.shipToPlace = _player.InstantiateShip(0);
        _player.shipToPlace.SetActive(false);
    }

    public void OnExit()
    {
    }

    private void MoveShipAround()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.waterMask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            _player.shipToMove.transform.position = new Vector3(posX, 1, posZ);
        }
    }

    private void PlaceShip()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _player.shipToPlace.transform.position = new Vector3(_player.shipToMove.transform.position.x, 0, _player.shipToMove.transform.position.z);
            _player.shipToMove.SetActive(false);
            _player.shipToPlace.SetActive(true);
            _player.gridSpawner.setVisibility(false);
            shipPlaced = true;
        }
    }

    private void ReplaceShip()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
        {

            if (Input.GetMouseButtonDown(0))
            {
                _player.shipToMove.transform.position = new Vector3(_player.shipToPlace.transform.position.x, 1, _player.shipToPlace.transform.position.z);
                _player.shipToMove.SetActive(true);
                _player.shipToPlace.SetActive(false);
                _player.gridSpawner.setVisibility(true);
                shipPlaced = false;

            }
        }
    }
}
