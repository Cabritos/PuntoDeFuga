using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OneTextInteraction : InteractionManager
{
    [SerializeField] [TextArea] private string[] _text = null;

    protected override IEnumerator StartInspection()
    {
        yield return StartCoroutine(IterateTexts(_text));
        
        StateManager.SetState(StateManager.ExplorationState);
    }
}