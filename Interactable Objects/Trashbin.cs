using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashbin : InteractionManager
{
    [SerializeField] [TextArea] private string[] _text1 = null;
    [SerializeField] [TextArea] private string[] _text2 = null;

    [SerializeField] private AudioClip _pickUpClip = null;

    private bool _firstPos = true;

    public override void Awake()
    {
        base.Awake();
    }

    protected override IEnumerator StartInspection()
    {
        if (_firstPos)
        {
            yield return StartCoroutine(IterateTexts(_text1));
            AudioSource.PlayClipAtPoint(_pickUpClip, transform.position);
            GameManager.HasTvControl = true;
            _firstPos = false;
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_text2));
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
