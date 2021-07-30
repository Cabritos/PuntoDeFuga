using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
    private PauseMenu _pauseMenu;

    public override void Awake()
    {
        base.Awake();
        _pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public override void Enter(State previousState)
    {
        FindObjectOfType<CameraMovement>().CameraLock = true;
        _pauseMenu.OpenMenu();
    }

    public override void StateUpdate()
    {
        if (InputManager.Ups()) _pauseMenu.ScrollUp();
        if (InputManager.Downs()) _pauseMenu.ScrollDown();
        if (InputManager.Action()) _pauseMenu.ActivateSelected();

        if (InputManager.Cancel()) StateManager.SetState(StateManager.ExplorationState);
        if (InputManager.Pause()) StateManager.SetState(StateManager.ExplorationState);
    }

    public override void StateFixedUpdate()
    {
        return;
    }

    public override void Exit(State nextState)
    {
        _pauseMenu.ExitPauseMenu();
    }
}
