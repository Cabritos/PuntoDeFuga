using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RandomPickup : InteractionManager
{
    [SerializeField] [TextArea] private string[] _onPick = null;
    [SerializeField] [TextArea] private string[] _afterPick = null;

    private bool _wasPicked = false;

    [SerializeField] private AudioClip _pickUpClip = null;

    protected override IEnumerator StartInspection()
    {
        if (!_wasPicked)
        {
            AudioSource.PlayClipAtPoint(_pickUpClip, transform.position);
            yield return StartCoroutine(IterateTexts(_onPick));
            _wasPicked = true;
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_afterPick));
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
