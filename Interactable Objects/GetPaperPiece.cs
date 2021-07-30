using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPaperPiece : InspectableAndPickable
{
    private PaperClue _paperClue;

    [SerializeField] private bool _isLeft = false;
    [SerializeField] private bool _isRight = false;
    [SerializeField] private bool _isBottom = false;

    public override void Awake()
    {
        base.Awake();
        _paperClue = FindObjectOfType<PaperClue>();
    }

    public override IEnumerator OnPickSpecifics() // to be overriden by children class for specific behaviours
    {
        if (_isLeft)
        {
            yield return StartCoroutine(_paperClue.GetLeftPiece());
        }

        if (_isRight)
        {
            yield return StartCoroutine(_paperClue.GetRightPiece());
        }

        if (_isBottom)
        {
            yield return StartCoroutine(_paperClue.GetBottomPiece());
        }
    }
}
