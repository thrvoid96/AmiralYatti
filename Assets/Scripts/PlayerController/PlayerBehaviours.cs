using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace PlayerScripts
{
    public abstract class PlayerBehaviours : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected LineRenderer lineRenderer;
        protected Player player;
        protected NavMeshAgent navMeshAgent;
        public LayerMask waterMask, shipMask;
        public GridSpawner gridSpawner;
        public GameObject shipToMove, shipToPlace, movSpotObject, fireSpotObject,shell;

        public List<GameObject> ships = new List<GameObject>();
        public List<GameObject> moveSpotDisplayers = new List<GameObject>();
        public List<GameObject> fireSpots = new List<GameObject>();
        public List<GameObject> shells = new List<GameObject>();
        public List<GameObject> shipsOnScene = new List<GameObject>();

        protected virtual void Awake()
        {
            player = GetComponent<Player>();

            _stateMachine = new StateMachine();            

            InstantiateShips();
            InstantiateShells();
            InstantiateDisplayer();
            InstantiateFireSpots();
        }

        private void InstantiateShips()
        {
            for (int i = 0; i < ships.Count; i++)
            {
                shipsOnScene.Add(Instantiate(ships[i], new Vector3(0, 0, 0), Quaternion.identity));
                shipsOnScene.Add(Instantiate(ships[i], new Vector3(0, 0, 0), Quaternion.identity));
            }

            for (int i = 0; i < shipsOnScene.Count; i++)
            {
                shipsOnScene[i].SetActive(false);
            }
        }

        private void InstantiateDisplayer()
        {
            movSpotObject = Instantiate(movSpotObject, new Vector3(0, 0, 0), Quaternion.identity);
            var secondObj = Instantiate(movSpotObject, new Vector3(0, 0, 0), Quaternion.identity);

            for (int i=1; i <= 3; i++)
            {
                moveSpotDisplayers.Add(Instantiate(secondObj, new Vector3(movSpotObject.transform.position.x , movSpotObject.transform.position.y, movSpotObject.transform.position.z + i), Quaternion.identity, movSpotObject.transform));
                moveSpotDisplayers.Add(Instantiate(secondObj, new Vector3(movSpotObject.transform.position.x , movSpotObject.transform.position.y, movSpotObject.transform.position.z - i), Quaternion.identity, movSpotObject.transform));
            }

            for (int i = 0; i < moveSpotDisplayers.Count; i++)
            {
                moveSpotDisplayers[i].transform.localScale = new Vector3(1, 1, 1);
                moveSpotDisplayers[i].SetActive(false);
            }

            Destroy(secondObj);
        }

        private void InstantiateFireSpots()
        {
            fireSpotObject = Instantiate(fireSpotObject, new Vector3(0, 0.1f, 0), Quaternion.identity);
            var secondObj = Instantiate(fireSpotObject, new Vector3(0, 0.1f, 0), Quaternion.identity);

            for (int i = 1; i >= -1; i--)
            {
                for (int j = -1; j <= 1; j++)
                {
                    fireSpots.Add(Instantiate(secondObj, new Vector3(fireSpotObject.transform.position.x + j , fireSpotObject.transform.position.y, fireSpotObject.transform.position.z + i), Quaternion.identity, fireSpotObject.transform));
                }
            }

            for (int i = 0; i < fireSpots.Count; i++)
            {
                fireSpots[i].transform.localScale = new Vector3(1, 1, 1);
                fireSpots[i].SetActive(false);
            }

            Destroy(secondObj);
        }

        private void InstantiateShells()
        {
            for (int i = 0; i < 3; i++)
            {
                shells.Add(Instantiate(shell, new Vector3(0, 0, 0), Quaternion.identity));
                shells[i].SetActive(false);
            }

        }
    }
}