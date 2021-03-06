using UnityEngine;
using CompartmentScripts;

//Script attached to GunCompartment gameobject. Controls the behaviours of this compartment on a ship.

public class GunCompartment : Compartment
{
    [SerializeField]private GameObject shellToFire;
    private Shell shell;

    private void Start()
    {
        shellToFire = Instantiate(shellToFire);
        shellToFire.SetActive(false);
        shell = shellToFire.GetComponent<Shell>();
    }

    public void FireGun(Vector3 start, Vector3 end)
    {
        shellToFire.SetActive(true);
        shell.SetStartAndEnd(start,end);
    }

}
