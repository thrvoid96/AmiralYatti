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
        if (!shipPlaced && _player.shipSelected)
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
        
    }

    public void OnExit()
    {
    }

    // Move ships around if mouse to screen raycast is hitting a Water layer
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

    // Place ship if right click. Make placement grid invisible
    private void PlaceShip()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _player.shipToPlace.transform.position = new Vector3(_player.shipToMove.transform.position.x, 0, _player.shipToMove.transform.position.z);
            _player.shipToMove.SetActive(false);
            _player.shipToPlace.SetActive(true);
            _player.gridSpawner.setVisibility(false);
            shipPlaced = true;
            _player.shipSelected = false;
        }
    }

    // Replace ship if mouse to screen raycast is hitting a Ship layer and player right clicks. Make grid visible.
    private void ReplaceShip()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
        {

            if (Input.GetMouseButtonDown(1))
            {
                _player.shipToMove.transform.position = new Vector3(_player.shipToPlace.transform.position.x, 1, _player.shipToPlace.transform.position.z);
                _player.shipToMove.SetActive(true);
                _player.shipToPlace.SetActive(false);
                _player.gridSpawner.setVisibility(true);
                _player.shipSelected = true;
                shipPlaced = false;

            }
        }
    }
}
