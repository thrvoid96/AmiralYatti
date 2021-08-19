using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script attached to ShipSelectUI gameobject. Adds the type of ship when a button is selected.
public class SpawnShipUI : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private UIManager uIManager;

    public void SpawnShip(string tag)
    {
        if (uIManager.checkShipCountOnScene()<3)
        {
            if (GameObject.FindGameObjectWithTag(tag) == null)
            {
                ObjectPooler.instance.SetEntirePool("Grid", true);
                var obj = ObjectPooler.instance.SpawnFromPool(tag, new Vector3(0, 0, 0), Quaternion.identity);

                uIManager.setSelectedShip(obj);
                uIManager.addShipCountOnScene();

                if(uIManager.checkShipCountOnScene() == 3)
                {
                    uIManager.changeDisplayText("Warning!", "You have placed maximum amount of ships!");
                    uIManager.animatedisplayUI(true, 0f);
                    uIManager.animatedisplayUI(false, 2f);
                }

            }
            else
            {
                uIManager.changeDisplayText("Warning!", "You have already spawned that type of ship!");
                uIManager.animatedisplayUI(true, 0f);
                uIManager.animatedisplayUI(false, 2f);
            }
        }

    }

}
