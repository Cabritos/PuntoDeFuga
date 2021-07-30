using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInt : InteractionManager
{
    [Header("Textos")]
    [SerializeField] [TextArea] private string[] _interactionText1 = null;

    [SerializeField] private string _question1 = null;
    [SerializeField] private string[] _options1 = null;

    [SerializeField] [TextArea] private string[] _answers1 = null;
    [SerializeField] [TextArea] private string[] _answers2 = null;
    [SerializeField] [TextArea] private string[] _answers3 = null;

    [Header("Sólo para monitoreo")]
    [SerializeField] private int _currentState = 0;

    public override void Awake()
    {
        base.Awake();
    }

    protected override IEnumerator StartInspection()
    {
        switch (_currentState)
        {
            case 0:
                yield return StartCoroutine(IterateTexts(_interactionText1));
                _currentState++;
                break;

            case 1:
                yield return StartCoroutine(AskQuestion(_question1, _options1));
                switch (CurrentOption)
                {
                    case 0:
                        yield return StartCoroutine(IterateTexts(_answers1));
                        break;
                    case 1:
                        yield return StartCoroutine(IterateTexts(_answers2));
                        break;
                    case 2:
                        yield return StartCoroutine(IterateTexts(_answers3));

                        transform.gameObject.SetActive(false);
                        break;
                }
                break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
