
using System.Collections.Generic;
using UnityEngine;
using CompartmentScripts;
using UnityEngine.AI;
using System.Collections;

//Script attached to any Ship type gameobject. Controls the all common behaviours between ships.

namespace ShipScripts
{
    public abstract class Ship : MonoBehaviour
    {
        public enum ShipType { Battleship, Cruiser, Destroyer }
        public ShipType shipType;
        public float speed = 3.5f;
        public float rotationSpeed = 0.5f;

        public int maxHealth;
        private int currentHealth;

        private Player player;
        private NavMeshObstacle navMeshObstacle;
        private List<int> randomNumbers = new List<int>();

        public List<Compartment> compartments = new List<Compartment>();
        private List<GunCompartment> gunCompartments = new List<GunCompartment>();

        protected virtual void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            navMeshObstacle.carving = true;

            var list = gameObject.GetComponentsInChildren<Compartment>();
            GetComponent<NavMeshAgent>().enabled = false;


            for (int i = 0; i < list.Length; i++)
            {
                compartments.Add(list[i]);

                maxHealth += list[i].maxHealth;

                var gunCompart = list[i].GetComponent<GunCompartment>();
                if (gunCompart)
                {
                    gunCompartments.Add(gunCompart);
                }

            }

            currentHealth = maxHealth;

        }
        //Fire guns in a random order with a delay in between. Because of same time collision bugs and making it not boring.
        public void FireGunsRandomly(Vector3 targetPosition)
        {
            RandomizeOrder(gunCompartments.Count);

            StartCoroutine(Fire(targetPosition));
        }

        private void RandomizeOrder(int numberOfCompartments)
        {
            randomNumbers.Clear();

            for (int i=0; i< numberOfCompartments; i++)
            {
                var random = Random.Range(0, numberOfCompartments);

                while (randomNumbers.Contains(random))
                {
                    random = Random.Range(0, numberOfCompartments);                    
                }
                randomNumbers.Add(random);
            }            
        }

        private IEnumerator Fire(Vector3 targetPos)
        {
            int iteration = 0;

            while (iteration < randomNumbers.Count)
            {                
                if (!gunCompartments[randomNumbers[iteration]].damaged && !gunCompartments[randomNumbers[iteration]].destroyed)
                {
                    gunCompartments[randomNumbers[iteration]].FireGun(gunCompartments[randomNumbers[iteration]].transform.position, targetPos);
                   
                }

                yield return new WaitForSeconds(1f);
                iteration++;
            }
            yield return null;
        }
    }
}
