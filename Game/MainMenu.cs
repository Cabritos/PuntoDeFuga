using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menuText = null;
    [SerializeField] private GameObject _controllers = null;
    private bool _controlDisplay= false;
    
    [SerializeField] private int _currentOption;
    private int _currentOptionsLength;
    [SerializeField] private GameObject[] _optionSelector = {null, null, null, null};

    [SerializeField] private Text _soundText = null;
    [SerializeField] private AudioClip _scrollClip = null;
    [SerializeField] private AudioClip _selectionClip = null;

    [SerializeField] private float _blinkRate = 1f;

    private float _sceneStartTime;

    private void Start()
    {
        _sceneStartTime = Time.time;
        _menuText.SetActive(true);
        _controllers.SetActive(false);
        _currentOptionsLength = _optionSelector.Length;
        _currentOption = 0;
        UpdateSelection(0);
        ToggleSoundDisplay();
        StartCoroutine("Blinking");
    }

    void Update()
    {
        if (Time.time - _sceneStartTime < 0.5f) return;

        if (!_controlDisplay)
        {
            if (InputManager.Ups())
            {
                ScrollUp();
            }

            if (InputManager.Downs())
            {
                ScrollDown();
            }

            if ((InputManager.Left() || InputManager.Right()) && _currentOption == 1)
            {
                GameManager.ToggleSound();
                ToggleSoundDisplay();
            }

            if (InputManager.Action())
            {
                ActivateSelected();
            }

            if (InputManager.Enter())
            {
                ActivateSelected();
            }
        }
        else
        {
            if (!InputManager.Action() && !InputManager.Enter() && !InputManager.Cancel()) return;

            _menuText.SetActive(true);
            _controllers.SetActive(false);
            _controlDisplay = false;
            AudioSource.PlayClipAtPoint(_selectionClip, transform.position);
        }
    }

    private void UpdateSelection(int direction)
    {
        StopCoroutine("Blinking");
        _currentOption += direction;

        if (_currentOption >= _currentOptionsLength)
            _currentOption = 0;
        else if (_currentOption < 0) _currentOption = _currentOptionsLength - 1;

        foreach (var option in _optionSelector) option.SetActive(false);

        _optionSelector[_currentOption].SetActive(true);
        StartCoroutine("Blinking");
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

    private void ToggleSoundDisplay()
    {
        if (GameManager.Sound)
        {
            _soundText.text = "Sonido: on";
            _optionSelector[1].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 265f);
        }
        else
        {
            _soundText.text = "Sonido: off";
            _optionSelector[1].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 275f);
        }
    }

    private IEnumerator Blinking()
    {
        while (true)
        {
            _optionSelector[_currentOption].gameObject.SetActive(true);
            yield return new WaitForSeconds(_blinkRate);
            _optionSelector[_currentOption].gameObject.SetActive(false);
            yield return new WaitForSeconds(_blinkRate);
        }
    }

    public void ActivateSelected()
    {
        AudioSource.PlayClipAtPoint(_selectionClip, transform.position, 0.5f);

        switch (_currentOption)
        {
            case 0:
                GameManager.NewGame();   
                //closure of this menu is called by PausedState.Exit()
                break;  

            case 1:
                GameManager.ToggleSound();
                ToggleSoundDisplay();
                break;

            case 2: //Controllers
                _menuText.SetActive(false);
                _controllers.SetActive(true);
                _controlDisplay = true;
                break;

            case 3:
                MySceneManager.ExitGame();
                break;
        }
    }
}