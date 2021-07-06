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
    private bool canMove;

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
            if (canMove)
            {
                RotateMoveSpot();
                MoveToPosition();
            }
            else
            {
                RotateTowardsDisplayerWhenCloseToDestination();
            }
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
                _navMeshAgent.updateRotation=false;
                _player.gridSpawner.setVisibility(true);
                _player.movSpotObject.SetActive(true);
                canMove = true;
              
                for(int i = 0; i < ship.compartments.Count-1; i++)
                {
                    _player.moveSpotDisplayers[i].SetActive(true);
                }
            }
        }
    }

    private void RotateMoveSpot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
        _player.movSpotObject.transform.Rotate(0, 90, 0);
        }
        
    }

    private void RotateTowardsDisplayerWhenCloseToDestination()
    {

        if (_navMeshAgent.remainingDistance < 1f)
        {
            _navMeshAgent.updateRotation = false;

            if (ship.transform.rotation.eulerAngles.y < 0)
            {
                if (ship.transform.rotation.eulerAngles.y < _player.movSpotObject.transform.rotation.eulerAngles.y)
                {
                    ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    Debug.LogError("here1");

                }
                else
                {
                    ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    Debug.LogError("here2");
                }
            }
            else if(ship.transform.rotation.eulerAngles.y > 0)
            {
                if (ship.transform.rotation.eulerAngles.y < _player.movSpotObject.transform.rotation.eulerAngles.y)
                {
                    ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    Debug.LogError("here3");

                }
                else
                {
                    ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    Debug.LogError("here4");
                }
            }

            if (Mathf.Abs(ship.transform.rotation.eulerAngles.y - _player.movSpotObject.transform.rotation.eulerAngles.y) < 1f)
            {              
                ship.transform.rotation = Quaternion.Euler(_player.movSpotObject.transform.rotation.eulerAngles.x, _player.movSpotObject.transform.rotation.eulerAngles.y, _player.movSpotObject.transform.rotation.eulerAngles.z);
                _player.movSpotObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                Debug.LogError("here5");
                _navMeshAgent = null;
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

                _navMeshAgent.updateRotation = true;

                _player.gridSpawner.setVisibility(false);
               
                _player.movSpotObject.SetActive(false);

                canMove = false;

                for (int i = 0; i < ship.compartments.Count - 1; i++)
                {
                    _player.moveSpotDisplayers[i].SetActive(false);
                }
            }
        }
    }
}
