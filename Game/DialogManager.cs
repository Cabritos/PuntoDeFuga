using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _statusText;

    [SerializeField] private string _textForExit = "Cerrar";
    [SerializeField] private string _textForKeepReading = "...";

    [SerializeField] private float _blinkRate = 1f;
    [SerializeField] private float _typeRate = 0.01f;

    private bool _lineDisplayed = false;
    private string _currentText = null;
    [SerializeField] private bool _isTyping = false;

    public bool OnDialog { get; private set; }

    void Awake()
    {
        if (!_dialogBox)
        {
            _dialogBox = GameObject.Find("Dialog Box");
        }

        if (!_mainText)
        {
            _mainText = GameObject.Find("Status Text").GetComponent<Text>();
        }

        if (!_statusText)
        {
            _statusText = GameObject.Find("Status Text").GetComponent<Text>();
        }
    }

    void Start()
    {
        _dialogBox.SetActive(false);
    }

    void Update()
    {
        if (InputManager.Action())
        {
            if (!_onCooldown)
            {
                _isTyping = false;
            }
        }
    }

    public IEnumerator DisplayText(string newText, bool isLast)
    {
        if (newText == null) yield break;

        _currentText = newText;

        if (!_dialogBox.gameObject.activeSelf) //es mi modo feo de hacer que arranque sólo al principio
        {
            StartCoroutine("Blinking");
        }
        _dialogBox.SetActive(true);
        
        _mainText.text = "";
        _lineDisplayed = false;
        _isTyping = true;

        StartCoroutine(Cooldown());
        yield return StartCoroutine(TypeText(_currentText));
        yield return new WaitForSeconds(0.2f);

        _statusText.text = isLast ? _textForExit : _textForKeepReading;

        if (!_lineDisplayed) yield return new WaitForSeconds(0.1f);
    }
    
    public void ExitTextBox()
    {
        _dialogBox.SetActive(false);
        _mainText.text = null;
        _statusText.text = null;
        StopCoroutine("Blinking");
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            _statusText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_blinkRate);
            _statusText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_blinkRate);
        }
    }
    
    private IEnumerator TypeText(string text)
    {
        foreach (char c in text)
        {
            if (!_isTyping) break;

            if (!char.IsWhiteSpace(c))
            {
                GetComponent<AudioSource>().Play();
            }

            _mainText.text += c; 
            yield return new WaitForSeconds(_typeRate);
        }
        
        _mainText.text = _currentText;
        _lineDisplayed = true;
    }

    private bool _onCooldown = false;
    private IEnumerator Cooldown()
    {
        _onCooldown = true;
        yield return new WaitForSeconds(0.15f);
        _onCooldown = false;
    }
}