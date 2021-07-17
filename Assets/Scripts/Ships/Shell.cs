using System.Collections;
using System.Collections.Generic;
using CompartmentScripts;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Player player;
    private Rigidbody rb;

    private Vector3 startPos, endPos;
    public float speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }

    public void SetStartAndEnd(Vector3 start, Vector3 end)
    {
        gameObject.SetActive(true);

        startPos = start;
        endPos = end;
    }
    private void FixedUpdate()
    {
        var distance = Vector3.Distance(startPos, endPos);
        var direction = (endPos - startPos).normalized;
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
