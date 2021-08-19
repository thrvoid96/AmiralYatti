using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The state where you can select ships from the UI and place them on the grid.
public class ShipPlacementState : IState
{   
    private RaycastHit hit;

    private Player _player;
    private UIManager _uIManager;

    public ShipPlacementState(Player player, UIManager uIManager)
    {
        _player = player;
        _uIManager = uIManager;
    }
  
    public void Tick()
    {
        //If the selected ship is not placed, you can move the ship around and try to place it. When placed, you can reclick ship to replace it.
        if(_player.selectedShip == null)
        {
            _uIManager.animateSpawnShipUI(true,0f);
            ReplaceShip();
        }
        else
        {
            _uIManager.animateSpawnShipUI(false,0f);
            MoveObjectOnWater(_player.selectedShip);
            RotateShip();
            PlaceShip();
        }
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        _uIManager.changeDisplayText("Turn 1", "Movement$State");
        _uIManager.animatedisplayUI(true, 0f);
        _uIManager.animatedisplayUI(false, 2f);
    }

    private void RotateShip()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _player.selectedShip.transform.Rotate(0, 90, 0);
        }

    }

    // Move ships around if mouse to screen raycast is hitting a Water layer
    private void MoveObjectOnWater(GameObject obj)
    {
        var TouchPos = Input.mousePosition;


        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.waterMask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            obj.transform.position = new Vector3(posX, 1, posZ);
        }
    }

    // Place ship if right click. Make placement grid invisible
    private void PlaceShip()
    {
        if (Input.GetMouseButtonDown(1))
        {          
            _player.selectedShip.transform.position = new Vector3(_player.selectedShip.transform.position.x, 0.1f, _player.selectedShip.transform.position.z);
            ObjectPooler.instance.SetEntirePool("Grid", false);

            if (_player.shipsOnScene < 3)
            {
                _player.selectedShip = null;
            }
            else
            {
                _player.state++;
            }
                       
        }
    }

    // Replace ship if mouse to screen raycast is hitting a Ship layer and player right clicks. Make grid visible.
    private void ReplaceShip()
    {
        var TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
        {

            if (Input.GetMouseButtonDown(2))
            {
                _player.selectedShip = hit.collider.gameObject;
                _player.selectedShip.transform.position = new Vector3(_player.selectedShip.transform.position.x, 1, _player.selectedShip.transform.position.z);
                ObjectPooler.instance.SetEntirePool("Grid", true);
            }
        }
    }
}
