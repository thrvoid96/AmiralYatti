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
        protected Player player;
        protected NavMeshAgent navMeshAgent;
        public LayerMask waterMask, shipMask;
        public GridSpawner gridSpawner;
        public GameObject shipToMove, shipToPlace, movSpotObject;

        public int maxShipsToBePlaced;
        public bool shipSelected;

        public List<GameObject> ships = new List<GameObject>();
        public List<GameObject> moveSpotDisplayers = new List<GameObject>();
        public List<GameObject> shipsOnScene = new List<GameObject>();

        protected virtual void Awake()
        {
            player = GetComponent<Player>();

            _stateMachine = new StateMachine();

            InstantiateShips();
            InstantiateDisplayer();
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
            for(int i=1; i <= 3; i++)
            {
                moveSpotDisplayers.Add(Instantiate(movSpotObject, new Vector3(movSpotObject.transform.position.x + i, movSpotObject.transform.position.y, movSpotObject.transform.position.z), Quaternion.identity, movSpotObject.transform));
                moveSpotDisplayers.Add(Instantiate(movSpotObject, new Vector3(movSpotObject.transform.position.x - i, movSpotObject.transform.position.y, movSpotObject.transform.position.z), Quaternion.identity, movSpotObject.transform));
            }

            for (int i = 0; i < moveSpotDisplayers.Count; i++)
            {
                moveSpotDisplayers[i].transform.localScale = new Vector3(1, 1, 1);
                moveSpotDisplayers[i].SetActive(false);
            }
        }
    }
}