using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ShipScripts;

//The state where you can move the ships from one grid point to another. Movement ranges not implemented yet.
public class ShipMovementState : IState
{
    private Player _player;
    private Ship _ship;
    private LineRenderer _lineRenderer;
    private NavMeshAgent _navMeshAgent;
    private NavMeshObstacle _navMeshObstacle;
    private int movedShipCount;
    private bool moving;

    private RaycastHit hit;
    private Vector3 TouchPos;


    public ShipMovementState(Player player, NavMeshAgent navMeshAgent, LineRenderer lineRenderer)
    {
        _player = player;
        _navMeshAgent = navMeshAgent;
        _lineRenderer = lineRenderer;
    }

    public void OnEnter()
    {
        ObjectPooler.instance.SetEntirePool("Grid", false);
    }

    public void OnExit()
    {
        UIManager.instance.changeDisplayText("Turn1", "Combat State");
        UIManager.instance.animatedisplayUI(true,0f);
        UIManager.instance.animatedisplayUI(false, 2f);
    }

    public void Tick()
    {
        if (_navMeshAgent == null)
        {
            GetComponents();
        }
        else
        {
            if (!moving)
            {
                RotateMoveSpot();
                MoveToPosition();
            }
            else
            {
                DrawPath(_navMeshAgent.path);
                RotateTowardsFinalPositionWhenCloseToDestination(_ship.rotationSpeed);
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
                _ship = hit.collider.gameObject.GetComponent<Ship>();

                if (_ship.canMove == false)
                {
                    Debug.LogError("this ship has already moved!");
                    _ship = null;
                    return;
                }

                _navMeshObstacle = hit.collider.gameObject.GetComponent<NavMeshObstacle>();
                _navMeshObstacle.enabled = false;

                _navMeshAgent = hit.collider.gameObject.GetComponent<NavMeshAgent>();                
                _navMeshAgent.enabled = true;
               
                _lineRenderer = hit.collider.gameObject.GetComponent<LineRenderer>();

                _navMeshAgent.updateRotation=false;
                ObjectPooler.instance.SetEntirePool("Grid", true);
                _player.destinationObject.SetActive(true);
 

                _player.adjustDestinationObject(_ship.compartments.Count, true, _ship.transform.rotation);
            }
        }
    }

    private void RotateMoveSpot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
        _player.destinationObject.transform.Rotate(0, 90, 0);
        }
        
    }

    //When getting close to destination, rotate the ship towards finalDestination
    private void RotateTowardsFinalPositionWhenCloseToDestination(float rotationSpeed)
    {
        //Check distance
        if (_navMeshAgent.remainingDistance < 1f)
        {
            _navMeshAgent.updateRotation = false;

            //If the rotations are not close enough, keep rotating
            if (Mathf.Abs(_ship.transform.rotation.eulerAngles.y - _player.destinationObject.transform.rotation.eulerAngles.y) > 1f)
            {
                //Case where the ship is looking up right
                if (_ship.transform.rotation.eulerAngles.y <= 90)
                {
                    RotateCase90270(rotationSpeed);
                }
                //Case where the ship is looking down right
                else if (_ship.transform.rotation.eulerAngles.y <= 180)
                {
                    RotateCase180360(rotationSpeed);
                }
                //Case where the ship is looking down left
                else if (_ship.transform.rotation.eulerAngles.y <= 270)
                {
                    RotateCase90270(-rotationSpeed);
                }
                //Case where the ship is looking up left
                else
                {
                    RotateCase180360(-rotationSpeed);
                }
            }
            //If the ship has almost rotated completely, finish rotation
            else           
            {              
                _ship.transform.rotation = _player.destinationObject.transform.rotation;

                _navMeshObstacle.enabled = true;
                _navMeshAgent.enabled = false;
                _navMeshAgent = null;

                movedShipCount++;
                _ship.canMove = false;
                _ship = null;

                moving = false;

                if (movedShipCount == 3)
                {
                    _player.state++;
                }
            }
        }
    }

    private void RotateCase90270(float rotSpeed)
    {
        //Case where the final position is in up right
        if (_player.destinationObject.transform.rotation.eulerAngles.y <= 90)
        {
            sameAngleSpecialCase(rotSpeed, 0);
        }
        //Case where the final position is in down right
        else if (_player.destinationObject.transform.rotation.eulerAngles.y <= 180)
        {
            _ship.gameObject.transform.Rotate(0, rotSpeed, 0);
        }
        //Case where the final position is in down left
        else if (_player.destinationObject.transform.rotation.eulerAngles.y <= 270)
        {

            sameAngleSpecialCase(-rotSpeed, 180 * rotSpeed / Mathf.Abs(rotSpeed));
        }
        //Case where the final position is in up left
        else
        {
            _ship.gameObject.transform.Rotate(0, -rotSpeed, 0);
        }
    }

    private void RotateCase180360(float rotSpeed)
    {
        //Case where the final position is in up right
        if (_player.destinationObject.transform.rotation.eulerAngles.y <= 90)
        {
            _ship.gameObject.transform.Rotate(0, -rotSpeed, 0);
        }
        //Case where the final position is in down right
        else if (_player.destinationObject.transform.rotation.eulerAngles.y <= 180)
        {
            sameAngleSpecialCase(rotSpeed, 0);
        }
        //Case where the final position is in down left
        else if (_player.destinationObject.transform.rotation.eulerAngles.y <= 270)
        {
            _ship.gameObject.transform.Rotate(0, rotSpeed, 0);
        }
        //Case where the final position is in up left
        else
        {
            sameAngleSpecialCase(-rotSpeed, 180 * rotSpeed / Mathf.Abs(rotSpeed));
        }
    }

    private void sameAngleSpecialCase(float rotSpd, float additive)
    {
        //Special fix where the ship and the final position rotation is in the same area
        if (_player.destinationObject.transform.rotation.eulerAngles.y < _ship.transform.rotation.eulerAngles.y + additive)
        {
            _ship.gameObject.transform.Rotate(0, -rotSpd, 0);
        }
        else
        {
            _ship.gameObject.transform.Rotate(0, rotSpd, 0);
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

            _player.destinationObject.transform.position = new Vector3(posX, 1f, posZ);
            _navMeshAgent.destination = new Vector3(_player.destinationObject.transform.position.x, 0f, _player.destinationObject.transform.position.z);

            _navMeshAgent.speed = 0f;
            _navMeshAgent.updateRotation = false;

            DrawPath(_navMeshAgent.path);

            if (Input.GetMouseButtonDown(0))
            {
                
                _navMeshAgent.updateRotation = true;
                _navMeshAgent.speed = _ship.speed;

                ObjectPooler.instance.SetEntirePool("Grid", false);

                _player.destinationObject.SetActive(false);

                _player.adjustDestinationObject(_ship.compartments.Count, false, _player.destinationObject.transform.rotation);

                moving = true;
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
