using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerScripts
{
    public abstract class PlayerBehaviours : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected Player player;

        public List<GameObject> ships = new List<GameObject>();
        public List<GameObject> shipsOnScene = new List<GameObject>();

        protected virtual void Awake()
        {
            player = GetComponent<Player>();

            _stateMachine = new StateMachine();

            InstantiateShips();

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
    }
}