using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
