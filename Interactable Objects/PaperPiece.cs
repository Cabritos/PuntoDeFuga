using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPiece : InteractionManager
{
    [Header("Texts")]
    [SerializeField] private string _questionSingular = null;
    [SerializeField] private string _questionPlural = null;
    [SerializeField] private string[] _options = null;

    private PaperClue _paperClue;

    public override void Awake()
    {
        base.Awake();
        _paperClue = FindObjectOfType<PaperClue>();
    }

    protected override IEnumerator StartInspection()
    {
        if (PaperClue.Count > 0)
        {
            yield return StartCoroutine(AskQuestion(_questionSingular, _options));
        }
        else
        {
            yield return StartCoroutine(AskQuestion(_questionPlural, _options));
        }

        switch (CurrentOption)
        {
            case 0: //pick
                yield return StartCoroutine(_paperClue.DisplayPieces());
                DialogManager.ExitTextBox();
                break;

            case 1: //leave
                break;
        }
        
        StateManager.SetState(StateManager.ExplorationState);
    }
}
