using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////Script attached to HealthUI gameobject. Controls the behaviours of the UI above ship.

public class HealthUI : MonoBehaviour
{
    private void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}
