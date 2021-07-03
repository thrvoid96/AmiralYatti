using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerScripts;

public class Player : PlayerBehaviours
{

    public LayerMask waterMask, shipMask;
    public GridSpawner gridSpawner;
    public GameObject shipToMove, shipToPlace;    

    public int maxShipsToBePlaced;
    public bool shipSelected;
    private void Update()
    {
        _stateMachine.Tick();
    }

    private void Start()
    {

        var shipPlacement = new ShipPlacementState(this);

        //At(patrol, enemyIdle, PatrolCheck());



        //_stateMachine.AddAnyTransition(enemyDead, ZeroHP());


        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        _stateMachine.SetState(shipPlacement);

        //Func<bool> IdleDelay() => () => enemyIdle.idleTime >= 3f;

    }

    

}