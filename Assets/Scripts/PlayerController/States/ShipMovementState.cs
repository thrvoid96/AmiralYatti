using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ShipScripts;

public class ShipMovementState : IState
{
    private Player _player;
    private Ship ship;
    private NavMeshAgent _navMeshAgent;

    private RaycastHit hit;
    private Vector3 TouchPos;

    public ShipMovementState(Player player, NavMeshAgent navMeshAgent)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
    }

    public void OnEnter()
    {
        _player.gridSpawner.setVisibility(false);
        _player.movSpotObject.SetActive(false);
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        if (_navMeshAgent == null)
        {
            GetComponents();
        }
        else
        {
            MoveToPosition();
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
                _navMeshAgent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                ship = hit.collider.gameObject.GetComponent<Ship>();
                _player.gridSpawner.setVisibility(true);
                _player.movSpotObject.SetActive(true);
              
                for(int i = 0; i < ship.compartments.Count-1; i++)
                {
                    _player.moveSpotDisplayers[i].SetActive(true);
                }
            }
        }
    }

    private void MoveToPosition()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.waterMask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            _player.movSpotObject.transform.position = new Vector3(posX, 1, posZ);

            if (Input.GetMouseButtonDown(1))
            {
                _navMeshAgent.destination= new Vector3(_player.movSpotObject.transform.position.x, 1, _player.movSpotObject.transform.position.z);
                _player.gridSpawner.setVisibility(false);
                _player.movSpotObject.SetActive(false);
                for (int i = 0; i < ship.compartments.Count - 1; i++)
                {
                    _player.moveSpotDisplayers[i].SetActive(false);
                }
                _navMeshAgent = null;
            }
        }
    }
}
