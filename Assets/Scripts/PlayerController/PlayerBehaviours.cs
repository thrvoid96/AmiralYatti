using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

//Not ideal potato code.

namespace PlayerScripts
{
    public abstract class PlayerBehaviours : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected LineRenderer lineRenderer;
        protected NavMeshAgent navMeshAgent;

        [SerializeField] protected UIManager uIManager;

        public LayerMask waterMask, shipMask;
        public GameObject selectedShip, destinationObject, fireSpotObject;
        private List<GameObject> destObjects = new List<GameObject>();
        private List<GameObject> fireObjects = new List<GameObject>();



        protected virtual void Awake()
        {
            _stateMachine = new StateMachine();

            getChildObjects(destinationObject, destObjects);
            getChildObjects(fireSpotObject, fireObjects);

            destinationObject.SetActive(false);
            fireSpotObject.SetActive(false);

        }

        private void getChildObjects(GameObject obj, List<GameObject> list)
        {
            foreach (Transform child in obj.transform)
            {
                list.Add(child.gameObject);
            }

            for (int i = 0; i < list.Capacity - 1; i++)
            {
                try
                {
                    if (!list[i].CompareTag("DestinationDisplayer") && !list[i].CompareTag("FireSpot"))
                    {
                        list.RemoveAt(i);
                    }
                }
                catch
                {
                    Debug.LogError("Ýndex range");
                }
            }
        }
           
        public void adjustDestinationObject(int compartmentCount,bool activity, Quaternion rotation)
        {
            destinationObject.transform.rotation = rotation;

            for(int i=0; i<compartmentCount-1; i++)
            {
                destObjects[i].SetActive(activity);
            }
        }
    }
}