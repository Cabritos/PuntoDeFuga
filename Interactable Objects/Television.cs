using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Television : InteractionManager
{
    private bool _isOn = false;

    [SerializeField] private MeshRenderer _screenMesh = null;
    [SerializeField] private int _screenMeshPosition = 0;
    [SerializeField] private Material _litMaterial = null;
    private Canvas _screenCanvas;

    private Material _currentMaterial;

    private AudioSource _audioSource;
    private TextMeshPro _text = null;

    [SerializeField] [TextArea] private string[] _noControllText= null;

    public override void Awake()
    {
        base.Awake();

        _text = GetComponentInChildren<TextMeshPro>();

        _currentMaterial = _screenMesh.materials[_screenMeshPosition];
        _screenCanvas = GetComponentInChildren<Canvas>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _text.enabled = false;
    }

    protected override IEnumerator StartInspection()
    {
        if (_isOn)
        {
            TurnOff();
        }
        else if (CheckIfController())
        {
            TurnOn();
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_noControllText));
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    private bool CheckIfController()
    {
        return GameManager.HasTvControl && GameManager.BatteryCount == 2;
    }

    private void TurnOn()
    {
        _audioSource.Play();

        _screenMesh.materials[_screenMeshPosition].SetColor("_EmissionColor", _litMaterial.GetColor("_EmissionColor"));
        _screenMesh.materials[_screenMeshPosition].EnableKeyword("_EMISSION");

        _text.enabled = true;

        _isOn = true;
    }

    private void TurnOff()
    {
        _audioSource.Stop();

        _screenMesh.materials[_screenMeshPosition].SetColor("_EmissionColor", _currentMaterial.GetColor("_EmissionColor"));
        _screenMesh.materials[_screenMeshPosition].DisableKeyword("_EMISSION");

        _text.enabled = false;

        _isOn = false;
    }
}
