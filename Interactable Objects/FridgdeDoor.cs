using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgdeDoor : InteractionManager
{
    [SerializeField] protected float _closedAngle = 0;
    [SerializeField] protected bool _isLocked;

    protected bool _isOpen = false;
    protected Vector3 _openPos;

    protected BoxCollider _collider;
    protected AudioSource AudioSource;

    [SerializeField] protected AudioClip _openSound = null;
    [SerializeField] protected AudioClip _closeSound = null;

    [SerializeField] private Light[] _lights = null;
    [SerializeField] private bool _isLit = false;

    public override void Awake()
    {
        base.Awake();
        AudioSource = GetComponent<AudioSource>();
        _collider = GetComponent<BoxCollider>();
        _openPos = transform.rotation.eulerAngles;
    }

    public void Start()
    {
        Close(false);

        if (_isLit) { TurnOn(); }
        else { TurnOff(); }
    }

    protected override IEnumerator StartInspection()
    {
        TogglePosition();
        Switch();

        yield return new WaitForEndOfFrame();

        StateManager.SetState(StateManager.ExplorationState);
    }

    public void Switch()
    {
        if (_isLit)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    protected void TurnOn()
    {
        foreach (Light light in _lights)
        {
            if (light == null)
            {
                Debug.LogError("Light missing");
                return;
            }

            light.transform.gameObject.SetActive(true);

            _isLit = true;
        }
    }


    protected void TurnOff()
    {
        foreach (Light light in _lights)
        {
            if (light == null)
            {
                Debug.LogError($"Light missing at {gameObject.name}");
                return;
            }

            light.transform.gameObject.SetActive(false);

            _isLit = false;
        }
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

    public void Open(bool sound = true)
    {
        _collider.enabled = false;
        transform.eulerAngles = _openPos;
        _collider.enabled = true;
        _isOpen = true;

        if (sound) //TODO wwise
        {
            if (_openSound) AudioSource.PlayOneShot(_openSound);
        }
    }

    public void Close(bool sound = true)
    {
        _collider.enabled = false;
        transform.eulerAngles = new Vector3(0, _closedAngle, 0);
        _collider.enabled = true;
        _isOpen = false;

        if (sound) //TODO wwise
        {
            if (_openSound) AudioSource.PlayOneShot(_closeSound);
        }
    }
}
