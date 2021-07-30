using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TwoTextsInteraction : InteractionManager
{
    [SerializeField] [TextArea] private string[] _text1 = null;
    [SerializeField] [TextArea] private string[] _text2 = null;

    [SerializeField] private bool _isLoopable = false;

    private bool _firstPos = true;

    protected override IEnumerator StartInspection()
    {
        if (_firstPos)
        {
            yield return StartCoroutine(IterateTexts(_text1));
            _firstPos = false;
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_text2));

            if (_isLoopable)
            {
                _firstPos = true;
            }
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
