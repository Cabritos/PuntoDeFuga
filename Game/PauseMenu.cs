using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private StateManager _stateManager;

    [SerializeField] private GameObject _menuBox = null;

    [SerializeField] private Text _soundText = null;
    [SerializeField] private AudioClip _scrollClip = null;
    [SerializeField] private AudioClip _selectionClip = null;

    [SerializeField] private GameObject[] _optionSelector = { null, null, null, null };

    [SerializeField] private int _currentOption = 0;
    private int _currentOptionsLength = 0;

    void Awake()
    {
        _stateManager = FindObjectOfType<StateManager>();
    }

    void Start()
    {
        ToggleSoundDisplay();
        _menuBox.SetActive(false);
    }

    public void OpenMenu()
    {
        _menuBox.SetActive(true);
        _currentOptionsLength = _optionSelector.Length;
        
        _currentOption = 0;
        UpdateSelection(0);
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
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
        UpdateSelection(1);
    }

    public void ScrollUp()
    {
        AudioSource.PlayClipAtPoint(_scrollClip, transform.position, 0.3f);
        UpdateSelection(-1);
    }

    public void ExitPauseMenu()
    {
        _menuBox.SetActive(false);
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


    public void ActivateSelected()
    {
        AudioSource.PlayClipAtPoint(_selectionClip, transform.position, 0.5f);

        switch (_currentOption)
        {
            case 0: //back to the game
                _stateManager.SetState(StateManager.ExplorationState);
                //closure of this menu is called by PausedState.Exit()
                break;

            case 1: //sound
                GameManager.ToggleSound();
                ToggleSoundDisplay();
                break;

            case 2: //main menu
                ExitPauseMenu();
                MySceneManager.LoadMainMenu();
                break;

            case 3: //back to the game
                MySceneManager.ExitGame();
                break;
        }
    }
}
