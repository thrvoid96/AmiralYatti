using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script attached to ShipSelectUI gameobject. Adds the type of ship when a button is selected.
public class SpawnShipUI : MonoBehaviour
{
    public Animator animator;

    public void SpawnShip(string tag)
    {
        if (UIManager.instance.checkShipCountOnScene()<3)
        {
            if (GameObject.FindGameObjectWithTag(tag) == null)
            {
                ObjectPooler.instance.SetEntirePool("Grid", true);
                var obj = ObjectPooler.instance.SpawnFromPool(tag, new Vector3(0, 0, 0), Quaternion.identity);

                UIManager.instance.setSelectedShip(obj);
                UIManager.instance.addShipCountOnScene();

                if(UIManager.instance.checkShipCountOnScene() == 3)
                {
                    Debug.LogError("You have placed maximum amount of ships!");
                }

            }
            else
            {
                Debug.LogError("You have already spawned that type of ship!");
            }
        }

    }

}
