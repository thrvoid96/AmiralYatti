using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompartmentScripts;
using System;

namespace ShipScripts
{
    public abstract class Ship : MonoBehaviour
    {
        public enum ShipType {Battleship,Cruiser,Destroyer}
        public ShipType shipType;
        public float speed = 3.5f;
        public float rotationSpeed = 0.5f;

        public List<Compartment> compartments = new List<Compartment>();

        protected virtual void Awake()
        {
            var list = gameObject.GetComponentsInChildren<Compartment>();

            for(int i=0; i < list.Length; i++)
            {
                compartments.Add(list[i]);
            }            
            
        }

    }
}
