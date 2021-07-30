using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(ExplorationState))]
[RequireComponent(typeof(InspectionState))]
[RequireComponent(typeof(DecisionState))]
[RequireComponent(typeof(PausedState))]

public class StateManager : MonoBehaviour
{
    private static ExplorationState _explorationState;
    public static ExplorationState ExplorationState => _explorationState;

    private static InspectionState _inspectionState;
    public static InspectionState InspectionState => _inspectionState;

    private static DecisionState _decisionState;
    public static DecisionState DecisionState => _decisionState;

    private static PausedState _pausedState;
    public static PausedState PausedState => _pausedState;

    private static OnComputerPasswordState _onComputerPasswordState;
    public static OnComputerPasswordState OnComputerPasswordState => _onComputerPasswordState;


    public State CurrentState { get; private set; }


    void Awake()
    {
        _explorationState = GetComponent<ExplorationState>();
        _inspectionState = GetComponent<InspectionState>();
        _decisionState = GetComponent<DecisionState>();
        _pausedState = GetComponent<PausedState>();
        _onComputerPasswordState = GetComponent<OnComputerPasswordState>();
        GameManager.SetPlayer(gameObject);
    }

    void Start()
    {
        CurrentState = (_explorationState);
    }

    void Update()
    {
        if (CurrentState != null) CurrentState.StateUpdate();
    }
    
    void FixedUpdate()
    {
        if (CurrentState != null) CurrentState.StateFixedUpdate();
    }
    
    
    public void SetState(State newState)
    {
        if (newState == CurrentState) return;

        State prevState = CurrentState;

        CurrentState.Exit(newState);
        CurrentState = newState;
        //Debug.Log($"New State = {newState}");
        CurrentState.Enter(prevState);
    }
}
