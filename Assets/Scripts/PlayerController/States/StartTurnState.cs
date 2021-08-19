using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnState : IState
{

    private Player _player;
    private UIManager _uIManager;
    private float time = 4f;

    public StartTurnState(Player player, UIManager uIManager)
    {
        _player = player;
        _uIManager = uIManager;
    }

    public void OnEnter()
    {
        _uIManager.changeDisplayText("Turn 1", "Placement$State");
        _uIManager.animatedisplayUI(true, 0f);
        _uIManager.animatedisplayUI(false, 2f);
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        if (time <= 0f)
        {
            _player.state++;
        }
        else
        {
            time -= Time.deltaTime;
        }

    }
}
