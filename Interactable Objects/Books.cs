using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Books : InteractionManager
{
    [Header("Texts")]
    [SerializeField] private string[] _options1 = null;
    [SerializeField] private string[] _options2 = null;


    [SerializeField] [TextArea] private string[] _onExam = null;

    [Header("Settings")]
    [SerializeField] private AudioClip _move = null;
    [SerializeField] private float _positionZ = 0;

    [SerializeField] private GameObject _paper = null;
    [SerializeField] private Books _book = null;

    [SerializeField] private bool _movable;
    private bool _moved = false;

    protected override IEnumerator StartInspection()
    {
        if (_moved)
        {
            yield return StartCoroutine(AskQuestion(null, _options2));

            switch (CurrentOption)
            {
                case 0: //exam
                    yield return StartCoroutine(IterateTexts(_onExam));
                    break;

                case 1:
                    break;

            }

            StateManager.SetState(StateManager.ExplorationState);
            yield break;
        }

        if (_movable)
        {
            yield return StartCoroutine(AskQuestion(null, _options1));

            switch (CurrentOption)
            {
                case 0: //exam
                    yield return StartCoroutine(IterateTexts(_onExam));
                    StateManager.SetState(StateManager.ExplorationState);
                    break;

                case 1: //move
                    transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                        _positionZ);
                    _moved = true;
                    AudioSource.PlayClipAtPoint(_move, transform.position);

                    if (_book != null)
                    {
                        _book.MakeMovable();
                    }
                    else
                    {
                        _paper.SetActive(true);
                    }
                    break;

                case 2:
                    break;
            }
        }
        else
        {
            yield return StartCoroutine(AskQuestion(null, _options2));

            switch (CurrentOption)
            {
                case 0: //exam
                    yield return StartCoroutine(IterateTexts(_onExam));
                    break;

                case 1:
                    break;

            }

            StateManager.SetState(StateManager.ExplorationState);
            yield break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    public void MakeMovable()
    {
        _movable = true;
    }
}
