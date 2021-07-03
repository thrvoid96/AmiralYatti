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

        protected virtual void Awake()
        {
            player = GetComponent<Player>();

            _stateMachine = new StateMachine();

        }
    }
}