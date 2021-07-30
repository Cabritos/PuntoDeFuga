using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : Door
{
    [Header("Textos")]
    [SerializeField] [TextArea] private string[] _tryToOpen1 = null;
    [SerializeField] [TextArea] private string[] _tryToOpen2 = null;

    [SerializeField] [TextArea] private string[] _thisIsIt = null;

    [Header("Otros")]
    [SerializeField] private AudioClip _tryToOpenSound = null;
    [SerializeField] private AudioClip _keys = null;

    [Header("Sólo para monitoreo")] [SerializeField]
    private int _currentState = 0;

    [SerializeField] private Outro _outro = null;

    public override void Awake()
    {
        base.Awake();
    }

    protected override IEnumerator StartInspection()
    {
        if (_isLocked)
        {
            switch (_currentState)
            {
                case 0:
                    AudioSource.PlayOneShot(_tryToOpenSound);
                    yield return StartCoroutine(IterateTexts(_tryToOpen1));
                    _currentState++;
                    break;

                case 1:
                    AudioSource.PlayOneShot(_tryToOpenSound);
                    yield return StartCoroutine(IterateTexts(_tryToOpen2));
                    break;
            }
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_thisIsIt));
            DialogManager.ExitTextBox();

            AudioSource.PlayOneShot(_keys);
            yield return new WaitForSeconds(2f);

            Open(true);
            _outro.gameObject.SetActive(true);
            _outro.PlayOutro();
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
