using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Outro : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private GameObject _postProcessVolumeObject = null;
    private Volume _postProcessVolume;
    [SerializeField] private RectTransform _dialogBox = null;
    [SerializeField] private Thunders _thunders = null;

    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _playerDummy = null;

    private DialogManager _dialogManager = null;
    [SerializeField] float ScrollingCooldown = 0.2f;

    [SerializeField] private BlackScreen _blackScreen = null;

    [SerializeField] private string _blackScreenText1 = null;
    [SerializeField] private string _blackScreenText2 = null;
    [SerializeField] private string _blackScreenText3 = null;
    
    [SerializeField] private VolumeProfile _closeVp = null;
    [SerializeField] private Camera _closeCam = null;
    [SerializeField] private string _closeText1 = null;
    [SerializeField] private string _closeText2 = null;

    [SerializeField] private VolumeProfile _backVp = null;
    [SerializeField] private Camera _backCam = null;
    [SerializeField] private string _backText1 = null;
    [SerializeField] private string _backText2 = null;

    [SerializeField] private VolumeProfile _sideVp = null;
    [SerializeField] private Camera _sideCam = null;
    [SerializeField] private string _sideText1 = null;
    [SerializeField] private string _sideText2 = null;

    [SerializeField] private VolumeProfile _midVp = null;
    [SerializeField] private Camera _midCam = null;
    [SerializeField] private string _midText = null;

    [SerializeField] private string _blackTextFinal = null;

    void Awake()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        _postProcessVolume = _postProcessVolumeObject.GetComponent<Volume>();
    }

    public void PlayOutro()
    {
        Destroy(_mainCamera.gameObject);
        Destroy(_player.gameObject);
        Destroy(_thunders.gameObject);
        _playerDummy.SetActive(true);
        _blackScreen.TurnBlackOn();
        StartCoroutine(PlayOutroCoroutine());
    }

    private IEnumerator PlayOutroCoroutine()
    {
        _closeCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(0, 100, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_blackScreenText1, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        yield return StartCoroutine(_dialogManager.DisplayText(_blackScreenText2, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(_dialogManager.DisplayText(_blackScreenText3, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();


        yield return new WaitForSeconds(1f);

        GetComponent<AudioSource>().Play();
        _blackScreen.TurnBlackOff();
        _postProcessVolume.profile = _closeVp;
        yield return new WaitForSeconds(3f);
        _dialogBox.localPosition = new Vector3(0, -350, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_closeText1, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        yield return StartCoroutine(_dialogManager.DisplayText(_closeText2, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _backVp;
        Destroy(_closeCam.gameObject);
        _backCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(_dialogManager.DisplayText(_backText1, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        yield return StartCoroutine(_dialogManager.DisplayText(_backText2, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _sideVp;
        Destroy(_backCam.gameObject);
        _sideCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(-500, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_sideText1, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        yield return StartCoroutine(_dialogManager.DisplayText(_sideText2, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _midVp;
        Destroy(_sideCam.gameObject);
        _midCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _dialogBox.localPosition = new Vector3(-500, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_midText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _blackScreen.TurnBlackOn();
        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(0, 100, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_blackTextFinal, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();


        yield return new WaitForSeconds(5f);
        MySceneManager.LoadMainMenu();
    }

    protected IEnumerator WaitForAction()
    {
        bool done = false;
        while (!done)
        {
            if (InputManager.Action())
            {
                done = true;
            }
            yield return null;
        }
    }
}
