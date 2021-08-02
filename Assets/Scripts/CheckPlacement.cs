using UnityEngine;

//Script attached to Compartment gameobjects. Checks whether you can place or move a ship to a certain point or not.

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] private Material GreenMat, RedMat;
    public bool canPlace = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Compartment") || other.CompareTag("Obstacle"))
        {
            Debug.LogError("Enter");
            GetComponent<MeshRenderer>().material = GreenMat;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = RedMat;
            canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Compartment") || other.CompareTag("Obstacle"))
        {
            Debug.LogError("Exit");
            GetComponent<MeshRenderer>().material = RedMat;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = GreenMat;
            canPlace = true;
        }
    }

}
