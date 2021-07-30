using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Door : InteractionManager
{
    [SerializeField] protected float ClosedAngle = 0;
    [SerializeField] protected bool _isLocked;
    [SerializeField] protected bool StartsOpen = false;
    
    public bool IsLocked => _isLocked;

    protected bool IsOpen = false;
    protected Vector3 OpenPos;

    protected BoxCollider Collider;
    protected AudioSource AudioSource;

    [SerializeField] protected AudioClip OpenSound = null;
    [SerializeField] protected AudioClip CoseSound = null;

    public override void Awake()
    {
        base.Awake();
        AudioSource = GetComponent<AudioSource>();
        Collider = GetComponent<BoxCollider>();
        OpenPos = transform.rotation.eulerAngles;
    }

    public void Start()
    {
        if (StartsOpen)
        {
            IsOpen = true;
            return;
        }

        Close(false);
    }

    protected override IEnumerator StartInspection()
    {
        TogglePosition();

        yield return new WaitForEndOfFrame();

        StateManager.SetState(StateManager.ExplorationState);
    }

    protected void TogglePosition()
    {
        if (!_isLocked)
        {
            if (IsOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Open(bool sound = true)
    {
        Collider.enabled = false;
        transform.eulerAngles = OpenPos;
        Collider.enabled = true;
        IsOpen = true;
        
        if (sound) //TODO wwise
        {
            if (OpenSound) AudioSource.PlayOneShot(OpenSound);
        }
    }

    public void Close(bool sound = true)
    {
        Collider.enabled = false;
        transform.eulerAngles = new Vector3(0, ClosedAngle, 0);
        Collider.enabled = true;
        IsOpen = false;

        if (sound) //TODO wwise
        {
            if (OpenSound) AudioSource.PlayOneShot(CoseSound);
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
