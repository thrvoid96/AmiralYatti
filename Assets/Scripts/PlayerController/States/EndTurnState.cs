using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnState : IState
{

    private Player _player;
    public EndTurnState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
