using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script attached to every single Compartment type gameobject. Controls the behaviours of this comaprtments on a ship.

namespace CompartmentScripts {
    public abstract class Compartment : MonoBehaviour
    {
        public bool damaged, destroyed;

        public int maxHealth;
        private int currentHealth;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
        }

        public void ChangeHealth(int value)
        {

        }
    }
}
