using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDoor : Door
{
    [Header("Textos")]
    [SerializeField] [TextArea] private string[] _tryToOpen1 = null;
    [SerializeField] [TextArea] private string[] _tryToOpen2 = null;
    [SerializeField] [TextArea] private string[] _tryToOpen3 = null;

    [SerializeField] [TextArea] private string[] _unpatientOpen = null;

    [SerializeField] [TextArea] private string[] _onUnlockSound = null;
    [SerializeField] [TextArea] private string[] _open = null;

    [Header("Sólo para monitoreo")] [SerializeField]
    private int _currentState = 0;
    [SerializeField] private int _openTriedCounter;

    [Header("Otros")]
    [SerializeField] private GameObject _house = null;
    [SerializeField] private Door[] _bedroomDoors = null;
    [SerializeField] private Drawer _bedroomDrawer = null;

    [SerializeField] private Thunders _thunders;
    [SerializeField] private AudioClip _tryToOpenSound = null;
    [SerializeField] private AudioClip _unlockSound = null;

    private bool _readyToUnlock;

    public override void Awake()
    {
        base.Awake();
        _readyToUnlock = false;

        foreach (var door in _bedroomDoors)
        {
            door.Lock();
        }

        _bedroomDrawer.Lock();
        _thunders = FindObjectOfType<Thunders>();
        _house.SetActive(false);
    }
    
    protected override IEnumerator StartInspection()
    {
        if (IsLocked)
        {
            if (_readyToUnlock) //unlock
            {
                PlaySound(_unlockSound);

                yield return StartCoroutine(IterateTexts(_open));

                _thunders.CastThunder();
                yield return new WaitForSeconds(0.5f);
                UnlockAndOpen();
            }
            else
            {
                switch (_currentState)
                {
                    case 0: //esta cerrada
                        PlaySound(_tryToOpenSound);
                        yield return StartCoroutine(IterateTexts(_tryToOpen1));

                        foreach (var door in _bedroomDoors)
                        {
                            door.Unlock();
                        }

                        _bedroomDrawer.Unlock();

                        _currentState++;
                        break;

                    case 1: //buscar algo
                        PlaySound(_tryToOpenSound);
                        yield return StartCoroutine(IterateTexts(_tryToOpen2));
                        _currentState++;
                        break;

                    case 2: //intentar
                        PlaySound(_tryToOpenSound);
                        if (_openTriedCounter < 5) //no llevará a nada
                        {
                            yield return StartCoroutine(IterateTexts(_tryToOpen3));
                            _openTriedCounter++;
                            break;
                        }
                        else //destrabada por insistencia
                        {
                            PlaySound(_unlockSound);

                            yield return StartCoroutine(IterateTexts(_unpatientOpen));

                            _thunders.CastThunder();
                            yield return new WaitForSeconds(0.5f);
                            UnlockAndOpen();

                            break;
                        }
                }
            }

        }
        else //destrabada, opera como puerta
        {
            TogglePosition();
            yield return new WaitForEndOfFrame();
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null) //TODO wwise
        {
            AudioSource.PlayOneShot(clip);
        }
    }

    [Header("Armarios")]

    [SerializeField] [TextArea] private string[] _hadThings= null;
    [SerializeField] private int _doorCount;

    public IEnumerator CountBedroomDoors()
    {
        _doorCount++;

        if (_doorCount == 3)
        {
            yield return StartCoroutine(IterateTexts(_hadThings));
        }
        else if (_doorCount >= 7)
        {
            _readyToUnlock = true;
            yield return StartCoroutine(AllDoorsOpened());
        }
    }

    private IEnumerator AllDoorsOpened()
    {
        yield return new WaitForSeconds(0.1f);
        PlaySound(_unlockSound);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(IterateTexts(_onUnlockSound));
    }

    public void UnlockAndOpen()
    {
        Unlock();
        Open();
        _house.SetActive(true);
    }
}
