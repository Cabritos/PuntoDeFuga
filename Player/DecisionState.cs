using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionState : State
{
    private bool _onCooldown = false;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Enter(State previousState)
    {
        _onCooldown = true;
        Invoke("InputCooldown", 0.4f);
    }

    public override void StateUpdate()
    {
        if (_onCooldown) return;
        if (InputManager.Ups()) OptionsManager.ScrollUp();
        if (InputManager.Downs()) OptionsManager.ScrollDown();
    }

    public override void StateFixedUpdate()
    {
        return;
    }

    public override void Exit(State nextState)
    {
        return;
    }

    private void InputCooldown()
    {
        _onCooldown = false;
    }
}
