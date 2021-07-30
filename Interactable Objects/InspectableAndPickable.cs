using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class InspectableAndPickable : InteractionManager
{
    [Header("Texts")] [SerializeField] private string _question = null;
    [SerializeField] private string[] _options = null;

    [SerializeField] [TextArea] private string[] _onPick = null;
    [SerializeField] [TextArea] private string[] _onOption2 = null;

    [SerializeField] [TextArea] private string[] _onOption1 = null;

    [Header("Settings")]
    [SerializeField] private AudioClip _pickUpClip = null;

    [SerializeField] private bool _pickable = true;

    protected override IEnumerator StartInspection()
    {
        if (_options.Length > 0)
        {
            yield return StartCoroutine(AskQuestion(_question, _options));

            switch (CurrentOption)
            { 
                case 0: //pick
                    if (_pickable) yield return StartCoroutine(PickAndDestroy());
                    else
                    {
                        yield return StartCoroutine(IterateTexts(_onOption1));
                        StateManager.SetState(StateManager.ExplorationState);
                    }
                    break;

                case 1: //leave
                    yield return StartCoroutine(IterateTexts(_onOption2));
                    DialogManager.ExitTextBox();
                    break;

                case 2:
                    break;
            }
        }
        else
        {
            yield return StartCoroutine(PickAndDestroy());
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    private IEnumerator PickAndDestroy()
    {
        AudioSource.PlayClipAtPoint(_pickUpClip, transform.position);

        yield return StartCoroutine(IterateTexts(_onPick));
        DialogManager.ExitTextBox();

        yield return OnPickSpecifics();

        StateManager.SetState(StateManager.ExplorationState);
        Destroy(this.gameObject);
    }

    public virtual IEnumerator OnPickSpecifics() // to be overriden by children class for specific behaviours
    {
        yield return null;
    }
}
