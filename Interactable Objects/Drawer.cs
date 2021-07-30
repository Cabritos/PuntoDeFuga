using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drawer : InteractionManager
{
    [SerializeField] private AudioClip _tryToOpenSound = null;

    [SerializeField] private bool _isLocked;
    [SerializeField] private bool _startsOpen = false;

    protected bool IsLocked => _isLocked;

    private bool _isOpen = false;
    private Vector3 _openPos;

    private BoxCollider _collider;

    [SerializeField] private AudioClip _openSound = null;
    [SerializeField] private AudioClip _closeSound = null;

    private AudioSource AudioSource;

    private static BedroomDoor _bedroomDoor;
    private bool _wasCounted;

    [SerializeField] private Battery _battery = null;

    public override void Awake()
    {
        base.Awake();

        if (!_bedroomDoor)
        {
            _bedroomDoor = FindObjectOfType<BedroomDoor>();
        }

        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _openPos = transform.position;
        _collider = GetComponent<BoxCollider>();
        if (_startsOpen) { return; }
        Close(false);
    }

    protected override IEnumerator StartInspection()
    {
        if (IsLocked)
        {
            AudioSource.PlayOneShot(_tryToOpenSound);
        }
        else
        {
            TogglePosition();

            yield return new WaitForEndOfFrame();

            if (!_wasCounted)
            {
                yield return StartCoroutine(_bedroomDoor.CountBedroomDoors());
                _wasCounted = true;
            }
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    protected void TogglePosition()
    {
        if (!_isLocked)
        {
            if (_isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    protected void Open(bool sound = true)
    {
        _collider.enabled = false;
        transform.position = _openPos;
        _collider.enabled = true;
        _isOpen = true;

        if (GameManager.BedroomReset)
        {
            if (_battery)
            {
                try
                {
                    _battery.gameObject.SetActive(true);
                }
                catch
                {
                    throw;
                }
            }
        }

        if (sound) //TODO wwise
        {
            if (_closeSound) AudioSource.PlayOneShot(_openSound);
        }
    }

    public void Close(bool sound = true)
    {
        _collider.enabled = false;
        transform.localPosition = new Vector3(0, 0, 0);
        _collider.enabled = true;
        _isOpen = false;

        if (sound) //TODO wwise
        {
            if (_closeSound) AudioSource.PlayOneShot(_closeSound);
        }

        try
        {
            _battery.gameObject.SetActive(false);
        }
        catch
        {
            return;
        }
    }

    public void Unlock()
    {
        _isLocked = false;
    }

    public void Lock()
    {
        _isLocked = true;
    }
}
