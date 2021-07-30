using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;   

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionsBox;

    [SerializeField] private GameObject[] _options = { null, null, null, null };
    private Text[] _optionText = {null, null, null, null};

    [SerializeField] private GameObject[] _optionSelector = { null, null, null, null };

    [SerializeField] private int _currentOption = 0;
    private int _currentOptionsLength = 0;

    [SerializeField] private GameObject[] _backgrounds = null;

    [SerializeField] private AudioClip _scrollClip = null;
    [SerializeField] private AudioClip _selectionClip = null;

    void Awake()
    {
        if (_optionsBox == null)
        {
            _optionsBox = GameObject.Find("Options Box");
        }

        for (var i = 0; i < 4; i++)
        {
            _optionText[i] = _options[i].GetComponent<Text>();
        }
    }

    void Start()
    {
        _optionsBox.SetActive(false);

        foreach (var option in _options)
        {
            option.SetActive(false);
        }

        foreach (var textComponent in _optionText)
        {
            textComponent.text = null;
        }
    }

    public void DisplayOptions(string[] options)
    {
        foreach (var textComponent in _optionText)
        {
            textComponent.text = null;
        }

        _optionsBox.SetActive(true);
        _currentOptionsLength = options.Length;


        for (var i = 0; i < _currentOptionsLength; i++)
        {
            _options[i].SetActive(true);
            _optionText[i].text = options[i];
        }

        _currentOption = 0;
        UpdateSelection(0);

        foreach (var background in _backgrounds)
        {
            background.SetActive(false);
        }

        if (_currentOptionsLength > 0)
        {
            _backgrounds[_currentOptionsLength - 1].SetActive(true);
        }
    }

    private void UpdateSelection(int direction)
    {
        _currentOption += direction;

        if (_currentOption >= _currentOptionsLength)
        {
            _currentOption = 0;
        }
        else if (_currentOption < 0)
        {
            _currentOption = _currentOptionsLength - 1;
        }

        foreach (var option in _optionSelector)
        {
            option.SetActive(false);
        }

        _optionSelector[_currentOption].SetActive(true);
    }
    
    public void ScrollDown()
    {
        UpdateSelection(1);
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
    }

    public void ScrollUp()
    {
        UpdateSelection(-1);
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
    }

    public void ActivateSelectedSound()
    {
        AudioSource.PlayClipAtPoint(_selectionClip, transform.position, 0.5f);
    }

    public int ExitOptionsBox()
    {
        _optionsBox.SetActive(false);

        return _currentOption;
    }
}
