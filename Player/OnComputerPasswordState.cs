using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnComputerPasswordState : State
{
    [SerializeField] private ComputerPassword _computerPassword;

    [SerializeField] private Camera _camera;
    private CameraMovement _cameraMovement;

    public override void Awake()
    {
        base.Awake();

        if (!_computerPassword)
        {
            _computerPassword = FindObjectOfType<ComputerPassword>();
        }

        if (_camera == null) _camera = Camera.main;
        _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    public override void Enter(State previousState)
    {
        _cameraMovement.CameraLock = false;
    }

    public override void StateUpdate()
    {
        if (InputManager.Up()) _computerPassword.ScrollUp();
        if (InputManager.Down()) _computerPassword.ScrollDown();
        if (InputManager.Left()) _computerPassword.ScrollLeft();
        if (InputManager.Right()) _computerPassword.ScrollRight();

        if (InputManager.Action()) _computerPassword.CheckPassword();
        if (InputManager.Cancel()) _computerPassword.Exit(); //TODO revisar
    }

    public override void StateFixedUpdate()
    {
        return;
    }

    public override void Exit(State nextState)
    {
        return;
    }
}
