using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ShipScripts;

public class ShipMovementState : IState
{
    private Player _player;
    private Ship _ship;
    private LineRenderer _lineRenderer;
    private NavMeshAgent _navMeshAgent;

    private RaycastHit hit;
    private Vector3 TouchPos;
    private bool canMove;

    public ShipMovementState(Player player, NavMeshAgent navMeshAgent, LineRenderer lineRenderer)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
        _lineRenderer = lineRenderer;
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
                DrawPath(_navMeshAgent.path);
                RotateTowardsFinalPositionWhenCloseToDestination();
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
                _ship = hit.collider.gameObject.GetComponent<Ship>();
                _lineRenderer = hit.collider.gameObject.GetComponent<LineRenderer>();

                _navMeshAgent.updateRotation=false;
                _player.gridSpawner.setVisibility(true);
                _player.movSpotObject.SetActive(true);
                canMove = true;
              
                for(int i = 0; i < _ship.compartments.Count-1; i++)
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
        Debug.LogError(_player.movSpotObject.transform.rotation.eulerAngles.y);
        }
        
    }

    private void RotateTowardsFinalPositionWhenCloseToDestination()
    {

        if (_navMeshAgent.remainingDistance < 1f)
        {
            _navMeshAgent.updateRotation = false;

            if (Mathf.Abs(_ship.transform.rotation.eulerAngles.y - _player.movSpotObject.transform.rotation.eulerAngles.y) > 1f)
            {
                if (_ship.transform.rotation.eulerAngles.y <= 90)
                {
                    if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 90)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y)
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }                           
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 180)
                    {
                        _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    } 
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 270)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y + 180)
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                    }
                    else
                    {
                        _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    }
                }
                else if (_ship.transform.rotation.eulerAngles.y <= 180)
                {
                    if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 90)
                    {
                        _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 180)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y)
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 270)
                    {
                        _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    }
                    else
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y + 180)
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                    }
                }
                else if (_ship.transform.rotation.eulerAngles.y <= 270)
                {
                    if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 90)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y)
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 180)
                    {
                        _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 270)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y - 180)
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                    }
                    else
                    {
                        _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    }
                }
                else
                {
                    if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 90)
                    {
                        _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 180)
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y)
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                    }
                    else if (_player.movSpotObject.transform.rotation.eulerAngles.y <= 270)
                    {
                        _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                    }
                    else
                    {
                        if (_player.movSpotObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y + 180)
                        {
                            _ship.gameObject.transform.Rotate(0, -0.5f, 0);
                        }
                        else
                        {
                            _ship.gameObject.transform.Rotate(0, 0.5f, 0);
                        }
                    }
                }
            }
            else           
            {              
                _ship.transform.rotation = Quaternion.Euler(_player.movSpotObject.transform.rotation.eulerAngles.x, _player.movSpotObject.transform.rotation.eulerAngles.y, _player.movSpotObject.transform.rotation.eulerAngles.z);
                _player.movSpotObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
            _navMeshAgent.destination = new Vector3(_player.movSpotObject.transform.position.x, 1, _player.movSpotObject.transform.position.z);
            _navMeshAgent.speed = 0f;
            _navMeshAgent.updateRotation = false;

            DrawPath(_navMeshAgent.path);

            if (Input.GetMouseButtonDown(1))
            {
                
                _navMeshAgent.updateRotation = true;
                _navMeshAgent.speed = _ship.speed;

                _player.gridSpawner.setVisibility(false);
               
                _player.movSpotObject.SetActive(false);

                canMove = false;

                for (int i = 0; i < _ship.compartments.Count - 1; i++)
                {
                    _player.moveSpotDisplayers[i].SetActive(false);
                }
            }
        }
    }

    private void DrawPath(NavMeshPath navMeshPath)
    {
        if (navMeshPath.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        _lineRenderer.positionCount = navMeshPath.corners.Length; //set the array of positions to the amount of corners

        for (var i = 1; i < navMeshPath.corners.Length; i++)
        {
            _lineRenderer.SetPosition(0, _ship.transform.position);
            _lineRenderer.SetPosition(i, navMeshPath.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }

}
