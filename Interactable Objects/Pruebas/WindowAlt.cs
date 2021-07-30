using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAlt : InteractionManager
{
    [Header("Texts")]
    /*
    [SerializeField] private string[] _optionsWatchOpen = null;
    [SerializeField] [TextArea] private string[] _watchClosed = null;

    [SerializeField] private string _askWatchCloseJump = null;
    [SerializeField] private string[] _optionsWatchCloseJump = null;
    [SerializeField] [TextArea] private string[] _watchOpened = null;

    [SerializeField] private string _askReallyJump = null;
    [SerializeField] private string[] _optionsJumpDont = null;
    [SerializeField] [TextArea] private string[] _dontJump = null;
    [SerializeField] [TextArea] private string[] _badIdea = null;
    [SerializeField] [TextArea] private string[] _reallyJump = null;
    */

    [Header("Debug")]
    [SerializeField] protected int _currentState;

    [SerializeField] public Interaction[] Interactions;

    void Start()
    {
        _currentState = 0;
    }

    protected override IEnumerator StartInspection()
    {
        Interactions[_currentState].DoSomeLogic();
        yield return null;
        StateManager.SetState(StateManager.ExplorationState);
    }
}
