using System.Collections;
using System.Collections.Generic;
using CompartmentScripts;
using UnityEngine;

//Script attached to Shell gameobject. Controls the behaviours of shell firing out of a GunCompartment.

public class Shell : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;

    private Vector3 endPos;
    public float speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        rb.velocity = transform.up * 10f;

        transform.position = start;
        endPos = end;
    }
    private void FixedUpdate()
    {
        var distance = Vector3.Distance(transform.position, endPos);
        var direction = (endPos - transform.position).normalized;

        rb.AddRelativeForce(speed * distance * Time.deltaTime * direction);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Compartment"))
        {
            gameObject.SetActive(false);
            var compartment = collider.gameObject.GetComponent<Compartment>();
        }
        /*
        else if (collider.CompareTag("Water"))
        {
            animator.SetBool("Watersplash",true);
        }
        */
    }
}
