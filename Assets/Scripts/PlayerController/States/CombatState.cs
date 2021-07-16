using System.Collections;
using System.Collections.Generic;
using ShipScripts;
using UnityEngine;

public class CombatState : IState
{
    private Player _player;
    private LineRenderer _firePosLineRenderer;
    private Ship _ship;
    private RaycastHit hit;
    private Vector3 TouchPos;

    private bool shipSelected;
    private int lineSegment = 100;
    private int shellCount;

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
                _firePosLineRenderer = _player.fireSpotObject.GetComponent<LineRenderer>();
                _firePosLineRenderer.positionCount = lineSegment;

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
                       
            VisualizeTrajectory(_player.fireSpotObject.transform.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player.shipMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _player.gridSpawner.setVisibility(false);
                    _player.fireSpotObject.SetActive(false);

                    for(int i=0; i< _ship.compartments.Count; i++)
                    {
                        if (_ship.compartments[i].GetComponent<GunCompartment>())
                        {                           
                            _player.shells[shellCount].SetActive(true);
                            _player.shells[shellCount].transform.position = _ship.compartments[i].transform.position;
                            _player.shells[shellCount].GetComponent<Rigidbody>().velocity = _ship.transform.up * 10f;
                            _player.shells[shellCount].GetComponent<Shell>().SetStartAndEnd(_ship.compartments[i].transform.position, _player.fireSpotObject.transform.position);
                            shellCount++;
                        }                      
                    }
                    shellCount = 0;
                    shipSelected = false;
                }
            }
        }
    }

    private void VisualizeTrajectory(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
                Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
                _firePosLineRenderer.SetPosition(i, pos);
        }
    }

    private Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 result = _ship.transform.position + (vo * time);
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + _ship.transform.position.y;

        result.y = sY;

        return result;
    }
  
}