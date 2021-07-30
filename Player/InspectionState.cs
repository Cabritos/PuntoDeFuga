using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionState : State
{
    private InteractionManager _currentInteraction;

    public override void Enter(State previousState)
    {
        FindObjectOfType<CameraMovement>().CameraLock = true;

        if (previousState != StateManager.ExplorationState) return;
        if (GameManager.CurrentInteractionManager == null) return;
        
        GameManager.CurrentInteractionManager.InspectionEnter();
    }

    public override void StateUpdate()
    {
        return;
    }

    public override void StateFixedUpdate()
    {
        return;
    }

    public override void Exit(State nextState)
    {
        if (nextState == StateManager.DecisionState)
        {

        }
        else if (nextState == StateManager.PausedState)
        {
            return;
        }
        else
        {
            OptionsManager.ExitOptionsBox();
            DialogManager.ExitTextBox();
            GameManager.CurrentInteractionManager = null;
        }
    }
}
