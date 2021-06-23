using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (gridPrefab)
        {
            GenerateGrid();
            return;
        }

        Debug.LogError("Please assign the gridPrefab component");
               
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + (scale * i), leftBottomLocation.y, leftBottomLocation.z + (scale * j)), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStats>().positionX = i;
                obj.GetComponent<GridStats>().positionY = j;
            }
        }
    }
}
