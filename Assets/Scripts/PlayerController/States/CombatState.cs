using System.Collections;
using System.Collections.Generic;
using ShipScripts;
using UnityEngine;

public class CombatState : IState
{
    private Player _player;
    private Ship _ship;
    private RaycastHit hit;
    private Vector3 TouchPos;

    private bool shipSelected;

    public CombatState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.gridSpawner.setVisibility(false);
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        if (!shipSelected)
        {
            GetComponents();
        }
        else
        {
            TargetToPosition();
        }
    }

    private void GetComponents()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
        {
            if (Input.GetMouseButtonDown(2))
            {
                _ship = hit.collider.gameObject.GetComponent<Ship>();

                _player.gridSpawner.setVisibility(true);
                _player.fireSpotObject.SetActive(true);

                shipSelected = true;
            }
        }
    }

    private void TargetToPosition()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.waterMask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            _player.fireSpotObject.transform.position = new Vector3(posX, 0.1f, posZ);
                       
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _player.gridSpawner.setVisibility(false);
                    _player.fireSpotObject.SetActive(false);

                    _ship.FireGunsRandomly(_player.fireSpotObject.transform.position);

                    shipSelected = false;
                }
            }
        }
    }

 
  
}