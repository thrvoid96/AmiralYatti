using UnityEngine;

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
