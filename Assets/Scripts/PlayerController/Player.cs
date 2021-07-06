using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerScripts;

public class Player : PlayerBehaviours
{

    
    private void Update()
    {
        _stateMachine.Tick();
    }

    private void Start()
    {

        var shipPlacement = new ShipPlacementState(this);
        var shipMovement = new ShipMovementState(this, navMeshAgent, lineRenderer);
        var combat = new CombatState(this);

        //At(patrol, enemyIdle, PatrolCheck());

        //_stateMachine.AddAnyTransition(enemyDead, ZeroHP());


        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        //_stateMachine.SetState(shipPlacement);
        _stateMachine.SetState(shipMovement);

        //Func<bool> IdleDelay() => () => enemyIdle.idleTime >= 3f;

    }

    

}