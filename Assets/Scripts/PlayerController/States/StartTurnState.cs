using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnState : IState
{

    private Player _player;
    private float time = 4f;

    public StartTurnState(Player player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        UIManager.instance.changeDisplayText("Turn 1", "Placement State");
        UIManager.instance.animatedisplayUI(true, 0f);
        UIManager.instance.animatedisplayUI(false, 2f);
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        if (time <= 0f)
        {
            UIManager.instance.changePlayerState();
        }
        else
        {
            time -= Time.deltaTime;
        }

    }
}
