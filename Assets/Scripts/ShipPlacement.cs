using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacement : MonoBehaviour
{
    
    [SerializeField] private LayerMask waterMask,shipMask;
    [SerializeField] Vector3 TouchPos;
    [SerializeField] private MeshRenderer GridRenderer;
    [SerializeField] private Material gridMaterial, gridNullMaterial, bbMaterial;
    private bool shipPlaced;
    private GameObject shipToMove, shipToPlace;
    private RaycastHit hit;

    public List<GameObject> ships = new List<GameObject>();

    private void Start()
    {
        shipToMove = Instantiate(ships[0], new Vector3(0, 0, 0), Quaternion.identity);
        shipToMove.SetActive(true);
        
        shipToPlace = Instantiate(ships[0], new Vector3(0, 0, 0), Quaternion.identity);
        shipToPlace.SetActive(false);
    }

    private void Update()
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

    private void MoveShipAround()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, waterMask))
        {
            int posX = (int)Mathf.Round(hit.point.x);
            int posZ = (int)Mathf.Round(hit.point.z);

            shipToMove.transform.position = new Vector3(posX, 1, posZ);
            GridRenderer.material = gridMaterial;
        }
    }

    private void PlaceShip()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shipToPlace.transform.position = new Vector3(shipToMove.transform.position.x,0,shipToMove.transform.position.z);
            shipToMove.SetActive(false);
            shipToPlace.SetActive(true);
            GridRenderer.material = gridNullMaterial;
            shipPlaced = true;
        }
    }

    private void ReplaceShip()
    {
        TouchPos = Input.mousePosition;
        //TouchPos = Input.GetTouch(0).position;
        Ray ray = Camera.main.ScreenPointToRay(TouchPos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, shipMask))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                shipToMove.transform.position = new Vector3(shipToPlace.transform.position.x, 1, shipToPlace.transform.position.z);
                shipToMove.SetActive(true);
                shipToPlace.SetActive(false);
                GridRenderer.material = gridMaterial;
                shipPlaced = false;
                
            }
        }
    }

}
