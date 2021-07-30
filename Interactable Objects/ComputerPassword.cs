using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class ComputerPassword : MonoBehaviour
{
    private Computer _computer;

    [SerializeField] private TMP_Text[] _symbols = null;
    private int _symbolsLength;

    [SerializeField] private Colors[] _currentColors = {Colors.White, Colors.White, Colors.White, Colors.White};
    [SerializeField] private int _currentOption;

    [SerializeField] private Colors[] _password = { Colors.White, Colors.White, Colors.White, Colors.White };

    [SerializeField] private Color32 _white = Color.white;
    [SerializeField] private Color32 _yellow = Color.yellow;
    [SerializeField] private Color32 _red =  Color.red;
    [SerializeField] private Color32 _purple = Color.magenta;
    [SerializeField] private Color32 _blue = Color.blue;
    [SerializeField] private Color32 _cyan = Color.cyan;
    [SerializeField] private Color32 _green = Color.green;
    [SerializeField] private Color32 _black = Color.black;
    [SerializeField] private Color32 _gray = Color.gray;

    [SerializeField] private float _blinkRate = 1f;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _scrollClip = null;
    [SerializeField] private AudioClip _successClip = null;
    [SerializeField] private AudioClip _wrongClip = null;

    private enum Colors
    {
        White = 0,
        Yellow = 1,
        Red = 2,
        Purple = 3,
        Blue = 4,
        Cyan = 5,
        Green = 6,
        Black = 7,
        Gray = 8,
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _computer = GetComponent<Computer>();

        _currentOption = 0;
        UpdateSelection(0);

        _symbolsLength = _symbols.Length;

        for (int i = 0; i < _symbols.Length; i++)
        {
            UpdateColor(i, Colors.White);
        }
    }
    
    private IEnumerator Blinking(int symbolPos)
    {
        while (true)
        {
            _symbols[symbolPos].gameObject.SetActive(true);
            yield return new WaitForSeconds(_blinkRate);
            _symbols[symbolPos].gameObject.SetActive(false);
            yield return new WaitForSeconds(_blinkRate);
        }
    }

    private void UpdateSelection(int direction)
    {
        var prevOption = _currentOption;

        _currentOption += direction;

        if (_currentOption >= _symbolsLength)
        {
            _currentOption = 0;
        }
        else if (_currentOption < 0)
        {
            _currentOption = _symbolsLength - 1;
        }

        StopCoroutine("Blinking");

        _symbols[prevOption].gameObject.SetActive(true);

        StartCoroutine("Blinking",_currentOption);
    }

    public void ScrollRight()
    {
        UpdateSelection(1);
    }

    public void ScrollLeft()
    {
        UpdateSelection(-1);
    }

    private void ScrollThroughColors(int direction)
    {
        _currentColors[_currentOption] += direction;

        if (_currentColors[_currentOption] >= (Colors) 9) // 9 == Colors.Length
        {
            _currentColors[_currentOption] = 0;
        }
        else if (_currentColors[_currentOption] < 0)
        {
            _currentColors[_currentOption] = (Colors) 8; // 8 == Colors.Length - 1
        }

        UpdateColor(_currentOption, _currentColors[_currentOption]);
    }

    private void UpdateColor(int position, Colors color)
    {
        _currentColors[position] = color;

        switch (color)
        {
            case Colors.White:
                _symbols[_currentOption].color = _white;
                break;

            case Colors.Yellow:
                _symbols[_currentOption].color = _yellow;
                break;

            case Colors.Red:
                _symbols[_currentOption].color = _red;
                break;

            case Colors.Purple:
                _symbols[_currentOption].color = _purple;
                break;

            case Colors.Blue:
                _symbols[_currentOption].color = _blue;
                break;

            case Colors.Cyan:
                _symbols[_currentOption].color = _cyan;
                break;

            case Colors.Green:
                _symbols[_currentOption].color = _green;
                break;

            case Colors.Black:
                _symbols[_currentOption].color = _black;
                break;

            case Colors.Gray:
                _symbols[_currentOption].color = _gray;
                break;
        }
    }

    public void ScrollUp()
    {
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
        ScrollThroughColors(1);
    }

    public void ScrollDown()
    {
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
        ScrollThroughColors(-1);
    }

    public void CheckPassword()
    {
        var matches = 0;

        for (int i = 0; i < _symbolsLength; i++)
        {
            if (_currentColors[i] == _password[i])
            {
                matches++;
            }
        }

        if (matches == 4)
        {
            _audioSource.PlayOneShot(_successClip, 0.5f);
            StopCoroutine("Blinking");
            _computer.Unlock();

            foreach (var symbol in _symbols)
            {
                Destroy(symbol.gameObject);
            }

            Exit();
        }
        else
        {
            _audioSource.PlayOneShot(_wrongClip, 0.2f);
        }
    }

    public void Exit()
    {
        _computer.OnPasswordExit();
    }
}
