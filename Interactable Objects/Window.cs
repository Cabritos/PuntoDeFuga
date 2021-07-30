using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : InteractionManager
{
    [SerializeField] private Intro _intro = null;
    [SerializeField] private BlackScreen _blackScreen = null;

    [Header("Texts")]
    [SerializeField] private string[] _optionsWatchOpen = null;
    [SerializeField] [TextArea] private string[] _watchClosed = null;
    
    [SerializeField] private string _askWatchCloseJump = null;
    [SerializeField] private string[] _optionsWatchCloseJump = null;
    [SerializeField] [TextArea] private string[] _watchOpened = null;

    [SerializeField] private string _askReallyJump = null;
    [SerializeField] private string[] _optionsJumpDont = null;
    [SerializeField] [TextArea] private string[] _dontJump = null;
    [SerializeField] [TextArea] private string[] _badIdea = null;
    [SerializeField] [TextArea] private string[] _reallyJump = null;

    [Header("Settings")]
    [SerializeField] private GameObject _windowPane = null;

    [SerializeField] private AudioClip _windowOpen = null;
    [SerializeField] private AudioClip _windowClose = null;

    [Header("Debug")]
    [SerializeField] private int _currentState;
    [SerializeField] private int _openTriedCounter;

    void Start()
    {
        _currentState = 0;
        _openTriedCounter = 0;
    }

    protected override IEnumerator StartInspection()
    {
        switch (_currentState)
        {
            case 0:
                yield return StartCoroutine(AskQuestion(null, _optionsWatchOpen));

                switch (CurrentOption)
                {
                    case 0: //mirar
                        yield return StartCoroutine(IterateTexts(_watchClosed));
                        break;

                    case 1: //abrir
                        _windowPane.gameObject.SetActive(false);
                        AudioSource.PlayClipAtPoint(_windowOpen, FindObjectOfType<CameraMovement>().gameObject.transform.position, 0.3f); //TODO esto esta mal
                        GetComponent<AudioSource>().Play();
                        _currentState++;
                        break;

                    case 2:
                        break;
                }
                break;


            case 1:

                yield return StartCoroutine(AskQuestion(_askWatchCloseJump, _optionsWatchCloseJump));

                switch (CurrentOption)
                {
                    case 0: //mirar
                        yield return StartCoroutine(IterateTexts(_watchOpened));
                        break;

                    case 1: //cerrar
                        _windowPane.gameObject.SetActive(true);
                        AudioSource.PlayClipAtPoint(_windowClose, FindObjectOfType<CameraMovement>().gameObject.transform.position, 0.3f); //TODO esto esta mal
                        GetComponent<AudioSource>().Stop();
                        _currentState--;
                        break;

                    case 2: //saltar
                        yield return StartCoroutine(AskQuestion(_askReallyJump, _optionsJumpDont));

                        switch (CurrentOption)
                        {
                            case 0: //no
                                yield return StartCoroutine(IterateTexts(_dontJump));
                                break;

                            case 1: //yes
                                if (_openTriedCounter < 3)
                                {
                                    yield return StartCoroutine(IterateTexts(_badIdea));
                                    _openTriedCounter++;
                                    break;
                                }
                                else
                                {
                                    yield return StartCoroutine(IterateTexts(_reallyJump));
                                    _blackScreen.TurnBlackOn();
                                    yield return new WaitForSeconds(2);
                                    _intro.gameObject.SetActive(true);
                                    break;
                                }
                        }

                        break;

                    case 3:
                        break;
                }
                
                break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
