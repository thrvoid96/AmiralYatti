using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerScripts;

//Script attached to the player. Controls the different states of what player can do.

public class Player : PlayerBehaviours
{
    [NonSerialized] public int state;
    [NonSerialized] public int shipsOnScene;

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void Start()
    {
        
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