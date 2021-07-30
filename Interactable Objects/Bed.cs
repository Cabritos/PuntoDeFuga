using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractionManager
{
    [Header("Texts")]
    [SerializeField] private string[] _options = null;

    [SerializeField] [TextArea] private string[] _onExam = null;
    [SerializeField] [TextArea] private string[] _onRest = null;

    [SerializeField] [TextArea] private string[] _test = null;

    [SerializeField] private GameObject _dummy = null;

    [Header("Debug")]
    [SerializeField] private int _currentState;

    protected override IEnumerator StartInspection()
    {
        yield return StartCoroutine(AskQuestion(null, _options));

        switch (CurrentOption)
        {
            case 0: //exam
                yield return StartCoroutine(IterateTexts(_onExam));
                DialogManager.ExitTextBox();
                break;

            case 1: //rest
                yield return StartCoroutine(IterateTexts(_onRest));
                DialogManager.ExitTextBox();

                var meshRenders = FindObjectOfType<ExplorationState>().gameObject.GetComponentsInChildren<MeshRenderer>();

                foreach (var meshRender in meshRenders)
                {
                    meshRender.enabled = false;
                }

                _dummy.SetActive(true);

                yield return new WaitForSeconds(5f);

                yield return StartCoroutine(IterateTexts(_test));
                DialogManager.ExitTextBox();

                foreach (var meshRender in meshRenders)
                {
                    meshRender.enabled = true;
                }

                _dummy.SetActive(false);

                break;

            case 2: //quit
                break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
