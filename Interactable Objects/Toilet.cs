using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : InteractionManager
{
    [SerializeField] [TextArea] private string[] _text1 = null;
    [SerializeField] [TextArea] private string[] _text2 = null;
    [SerializeField] [TextArea] private string[] _text3 = null;

    [SerializeField] private AudioClip _flushInitial = null;

    private AudioSource _audioSource;
    private CameraMovement _cameraMovement;
    private MultiuseRoomDoor _multiuseRoomDoor;

    private bool _hasPlayed;

    public override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _multiuseRoomDoor = FindObjectOfType<MultiuseRoomDoor>();
    }

    protected override IEnumerator StartInspection()
    {
        if (_hasPlayed)
        {
            StateManager.SetState(StateManager.ExplorationState);
            
        }
        else
        {
            _hasPlayed = true;

            yield return StartCoroutine(IterateTexts(_text1));
            DialogManager.ExitTextBox();
            
            _audioSource.PlayOneShot(_flushInitial);
            _cameraMovement.CameraLock = false;

            yield return new WaitForSeconds(2f);

            _audioSource.Play();

            yield return new WaitForSeconds(5f);

            yield return StartCoroutine(IterateTexts(_text2));
            DialogManager.ExitTextBox();

            yield return new WaitForSeconds(5f);

            yield return StartCoroutine(IterateTexts(_text3));
            DialogManager.ExitTextBox();

            StartCoroutine(_multiuseRoomDoor.Water());

            StateManager.SetState(StateManager.ExplorationState);
        }
    }

    public void StopWater()
    {
        _audioSource.Stop();
    }
}
