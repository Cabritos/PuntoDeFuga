using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : InteractionManager
{
    private MultiuseRoomDoor _door = null;
    [SerializeField] private GameObject _explotion = null;
    [SerializeField] private AudioClip _explotionClip = null;
    [SerializeField] private BlackHall _blackHall;

    [SerializeField] private AudioClip _fanfare = null;

    [Header("Textos")]
    [SerializeField] [TextArea] private string[] _greeting = null;
    [SerializeField] [TextArea] private string _question = null;
    [SerializeField] [TextArea] private string[] _answers = null;

    [SerializeField] [TextArea] private string[] _answerYes = null;
    [SerializeField] [TextArea] private string[] _answerNo = null;

    [Header("Sólo para monitoreo")]
    [SerializeField] private int _currentState;

    public override void Awake()
    {
        base.Awake();
        _door = FindObjectOfType<MultiuseRoomDoor>();
        if (!_blackHall) _blackHall = FindObjectOfType<BlackHall>();
    }

    protected override IEnumerator StartInspection()
    {
        switch (_currentState)
        {
            case 0:
                AudioSource.PlayClipAtPoint(_fanfare, transform.position);
                yield return StartCoroutine(IterateTexts(_greeting));
                _currentState++;
                break;


            case 1:
                yield return StartCoroutine(AskQuestion(_question, _answers));

                switch (CurrentOption)
                {
                    case 0:
                        yield return StartCoroutine(IterateTexts(_answerYes));
                        break;

                    case 1:
                        yield return StartCoroutine(IterateTexts(_answerNo));
                        break;
                }

                DialogManager.ExitTextBox();
                Instantiate(_explotion, transform.position, transform.rotation);

                AudioSource.PlayClipAtPoint(_explotionClip, transform.position, 5f);

                _door.KingBananaIsGone(_blackHall);
                StateManager.SetState(StateManager.ExplorationState);
                Destroy(gameObject);
                break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}   
