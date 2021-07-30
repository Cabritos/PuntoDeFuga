using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(InputManager))]
public abstract class State : MonoBehaviour
{
    protected static StateManager StateManager;
    protected static InputManager InputManager;
    protected static GameManager GameManager;
    protected static DialogManager DialogManager;
    protected static OptionsManager OptionsManager;

    public virtual void Awake()
    {
        StateManager = GetComponent<StateManager>();
        InputManager = GetComponent<InputManager>();
        GameManager = FindObjectOfType<GameManager>();
        DialogManager = FindObjectOfType<DialogManager>();
        OptionsManager = FindObjectOfType<OptionsManager>();
    }

    public abstract void Enter(State previousState);

    public abstract void StateUpdate();
    public abstract void StateFixedUpdate();
    public abstract void Exit(State nextState);
}