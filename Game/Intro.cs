using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Intro : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private GameObject _postProcessVolumeObject = null;
    private Volume _postProcessVolume;
    [SerializeField] private RectTransform _dialogBox = null;

    [SerializeField] private GameObject _player = null;
    [SerializeField] private GameObject _playerDummy = null;

    private Transform _playerStartTransform;

    private DialogManager _dialogManager = null;
    [SerializeField] float ScrollingCooldown = 0.2f;

    [SerializeField] private BlackScreen _blackScreen = null;

    [SerializeField] private string _blackScreenText = null;

    [SerializeField] private VolumeProfile _fanVp = null;
    [SerializeField] private Camera _fanCam = null;
    [SerializeField] private string _fanText = null;

    [SerializeField] private VolumeProfile _fanLeftVp = null;
    [SerializeField] private Camera _fanLeftCam = null;
    [SerializeField] private string _fanLeftText = null;

    [SerializeField] private VolumeProfile _mapVp = null;
    [SerializeField] private Camera _mapCam = null;
    [SerializeField] private string _mapText = null;

    [SerializeField] private VolumeProfile _boatVp = null;
    [SerializeField] private Camera _boatCam = null;
    [SerializeField] private string _boatText = null;

    [SerializeField] private VolumeProfile _overMidVp = null;
    [SerializeField] private Camera _overMidCam = null;
    [SerializeField] private string _overMidText = null;

    [SerializeField] private VolumeProfile _fanRightVp = null;
    [SerializeField] private Camera _fanRightCam = null;
    [SerializeField] private string _fanRightText = null;

    [SerializeField] private VolumeProfile _midPlaneVp = null;
    [SerializeField] private Camera _midPlaneCam = null;
    [SerializeField] private string _midPlaneText = null;

    [SerializeField] private VolumeProfile _closeupVp = null;
    [SerializeField] private Camera _closeupCam = null;
    [SerializeField] private string _closeupText = null;

    [SerializeField] private VolumeProfile _generalVp = null;
    [SerializeField] private Camera _generalCam = null;
    [SerializeField] private string _generalText = null;

    [SerializeField] private GameObject _introCameras = null;

    [SerializeField] private VolumeProfile _mainVp = null;
    private Vector3 _dialogueBoxRegularPosition;

    void Awake()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        _postProcessVolume = _postProcessVolumeObject.GetComponent<Volume>();
        _playerStartTransform = _player.transform;
    }

    void Start()
    {
        _mainCamera.gameObject.SetActive(false);
        _player.SetActive(false);
        _playerDummy.SetActive(true);
        _blackScreen.TurnBlackOn();
        _dialogueBoxRegularPosition = _dialogBox.localPosition;
        StartCoroutine(nameof(PlayIntro));
    }

    private IEnumerator PlayIntro()
    {
        _fanCam.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(0, 100,0);
        yield return StartCoroutine(_dialogManager.DisplayText(_blackScreenText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _blackScreen.TurnBlackOff();
        _postProcessVolume.profile = _fanVp;
        yield return new WaitForSeconds(3f);
        _dialogBox.localPosition = new Vector3(400, -250, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_fanText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _fanRightVp;
        Destroy(_fanCam.gameObject);
        _fanLeftCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(_dialogManager.DisplayText(_fanLeftText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _mapVp;
        Destroy(_fanLeftCam.gameObject);
        _mapCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(_dialogManager.DisplayText(_mapText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _boatVp;
        Destroy(_mapCam.gameObject);
        _boatCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); 
        _dialogBox.localPosition = new Vector3(-480, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_boatText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _overMidVp;
        Destroy(_boatCam.gameObject);
        _overMidCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _dialogBox.localPosition = new Vector3(500, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_overMidText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _fanLeftVp;
        Destroy(_overMidCam.gameObject);
        _fanRightCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(400, -250, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_fanRightText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _midPlaneVp;
        Destroy(_fanRightCam.gameObject);
        _midPlaneCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _dialogBox.localPosition = new Vector3(500, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_midPlaneText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(1f);

        _postProcessVolume.profile = _closeupVp;
        Destroy(_midPlaneCam.gameObject);
        _closeupCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _dialogBox.localPosition = new Vector3(-500, 500, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_closeupText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(3f);

        _postProcessVolume.profile = _generalVp;
        Destroy(_closeupCam.gameObject);
        _generalCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _dialogBox.localPosition = new Vector3(0, 530, 0);
        yield return StartCoroutine(_dialogManager.DisplayText(_generalText, false));
        yield return new WaitForSeconds(ScrollingCooldown);
        yield return StartCoroutine(WaitForAction());
        _dialogManager.ExitTextBox();
        yield return new WaitForSeconds(2f);

        _blackScreen.TurnBlackOn();
        yield return new WaitForSeconds(2f);     
        Destroy(_generalCam.gameObject);
        _mainCamera.gameObject.SetActive(true);
        _postProcessVolume.profile = _mainVp;
        _playerDummy.SetActive(false);
        _player.SetActive(true);
        _blackScreen.TurnBlackOff();
        FindObjectOfType<Thunders>().CastThunder(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(nameof(PlayIntro));
            Destroy(_introCameras.gameObject);
            StartGame();
        }
    }

    public void StartGame()
    {
        _dialogManager.ExitTextBox();
        _dialogBox.localPosition = _dialogueBoxRegularPosition;
        _blackScreen.TurnBlackOn();
        _mainCamera.gameObject.SetActive(true);
        _postProcessVolume.profile = _mainVp;
        _playerDummy.SetActive(false);
        _player.transform.position = _playerStartTransform.position;
        _player.transform.rotation = _playerStartTransform.rotation;
        _player.SetActive(true);
        _blackScreen.TurnBlackOff();
        FindObjectOfType<Thunders>().CastThunder(1);
        gameObject.SetActive(false);
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
