using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Computer : InteractionManager
{
    private bool _isOn = false;

    [SerializeField] private MeshRenderer _screenMesh = null;
    [SerializeField] private int _screenMeshPosition = 0;
    [SerializeField] private Material _litMaterial = null;
    private Light _screenLight;
    private Canvas _screenCanvas;

    [SerializeField] private bool _isUnlocked = false;

    private bool _onPassword = false;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _turnOnClip = null;

    [SerializeField] private TMP_Text _text = null;
    [SerializeField] private Material _whiteMaterial = null;
    [SerializeField] [TextArea] private string[] _onNote = null;
    [SerializeField] [TextArea] private string[] _note = null;
    [SerializeField] [TextArea] private string[] _notePs = null;

    public override void Awake()
    {
        base.Awake();

        _screenLight = GetComponentInChildren<Light>();
        _screenCanvas = GetComponentInChildren<Canvas>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _text.gameObject.SetActive(false);
        _screenLight.gameObject.SetActive(false);
        _screenCanvas.gameObject.SetActive(false);
    }
    
    protected override IEnumerator StartInspection()
    {
        if (!_isOn) yield return TurnOn();

        if (!_isUnlocked)
        {
            StateManager.SetState(StateManager.OnComputerPasswordState);
            _onPassword = true;

            yield return new WaitWhile(() => _onPassword);
        }

        if (_isUnlocked)
        {
            if (GameManager != null) GameManager.OnReadNote();
            
            _text.gameObject.SetActive(true);
            _screenMesh.materials[_screenMeshPosition].SetColor("_EmissionColor", _whiteMaterial.GetColor("_EmissionColor"));
            yield return StartCoroutine(IterateTexts(_onNote));
            DialogManager.ExitTextBox();
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(IterateTexts(_note));
            DialogManager.ExitTextBox();
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(IterateTexts(_notePs));

        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    private object TurnOn()
    {
        _audioSource.PlayOneShot(_turnOnClip);

        _screenMesh.materials[_screenMeshPosition].SetColor("_EmissionColor", _litMaterial.GetColor("_EmissionColor"));
        _screenMesh.materials[_screenMeshPosition].EnableKeyword("_EMISSION");
        _screenLight.gameObject.SetActive(true);
        
        _screenCanvas.gameObject.SetActive(true);

        _isOn = true;

        return new WaitForSeconds(0.3f);
    }

    public void Unlock()
    {
        _isUnlocked = true;
    }

    public void OnPasswordExit()
    {
        _onPassword = false;
        StateManager.SetState(StateManager.InspectionState);
    }
}
