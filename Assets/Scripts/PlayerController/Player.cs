using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerScripts;

//Script attached to the player. Controls the different states of what player can do.

public class Player : PlayerBehaviours
{
    private int state;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            state++;
        }

        _stateMachine.Tick();
      
    }

    private void Start()
    {
        destinationObject.SetActive(false);
        fireSpotObject.SetActive(false);

        var startTurn = new StartTurnState(this);
        var shipPlacement = new ShipPlacementState(this);
        var shipMovement = new ShipMovementState(this, navMeshAgent, lineRenderer);
        var combat = new CombatState(this);
        var endTurn = new EndTurnState(this);

        At(startTurn, shipPlacement, turnStarted());
        At(shipPlacement, shipMovement, endOfPlacement());
        At(shipMovement, combat, endOfMovement());
        At(combat, endTurn, endOfCombat());

        //_stateMachine.AddAnyTransition(enemyDead, ZeroHP());


        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        _stateMachine.SetState(startTurn);

        Func<bool> turnStarted() => () => state == 1;
        Func<bool> endOfPlacement() => () => state == 2;
        Func<bool> endOfMovement() => () => state == 3;
        Func<bool> endOfCombat() => () => state == 4;

    }
    
}