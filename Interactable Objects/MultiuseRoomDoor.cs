using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiuseRoomDoor : Door
{
    private enum stage
    {
        Start,
        ClosedWithWaterWaiting,
        OpenedWithWater,
        OnWhaleWatched,
        ClosedWithEmptyWaiting,
        OpenedWithEmpty,
        ClosedWithBananaWaiting ,
        OpenedWithBanana,
        AfterKingBanana,
        InBlackHall,
        NeverAgain
    }
    
    [Header("Textos")]
    [SerializeField] [TextArea] private string[] _tryToOpen1 = null;
    [SerializeField] [TextArea] private string[] _whaleCall = null;
    [SerializeField] [TextArea] private string[] _closeWhaleRoom = null;
    [SerializeField] [TextArea] private string[] _onWhaleWatch = null;
    [SerializeField] [TextArea] private string[] _neverAgain = null;

    [Header("Sólo para monitoreo")] [SerializeField]
    private stage _currentStage = stage.Start;

    [Header("Otros")]
    [SerializeField] private Thunders _thunders;

    [Header("Whaleroom")]
    [SerializeField] private float _whaleStare = 10f;
    [SerializeField] private AudioClip _whaleSound = null;
    [SerializeField] private AudioClip _whaleCloseClip = null;
    private CameraMovement _cameraMovement;
    private Toilet _toilet;

    [SerializeField] private GameObject _onBannanaRoomEnter = null;

    [SerializeField] private GameObject _whaleRoom = null;
    [SerializeField] private GameObject _emptyRoom = null;
    [SerializeField] private GameObject _bananaRoom = null;

    private BlackHall _blackHall;
    private GameObject _player = null;

    //[SerializeField] private Transform _playerTransform = null;

    public override void Awake()
    {
        base.Awake();

        _thunders = FindObjectOfType<Thunders>();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _toilet = FindObjectOfType<Toilet>();
        _player = GameManager.Player;
    }

    void Update()
    {
        switch (_currentStage)
        {
            case stage.OpenedWithWater:
                if (PlayerIsInsideSomewhatMore())
                {
                    OnWhaleWatch();
                    _currentStage = stage.OnWhaleWatched;
                }
                break;

            case stage.OnWhaleWatched:
                if (PlayerIsOutside() && _whaleRoom != null)
                {
                    StartCoroutine(CloseWhaleRoom());
                }
                break;
        }
    }

    protected override IEnumerator StartInspection()
    {

        switch (_currentStage)
        {
            case stage.Start:
                yield return StartCoroutine(IterateTexts(_tryToOpen1));
                break;

            case stage.ClosedWithWaterWaiting:
                _thunders.CastThunder();
                _whaleRoom.SetActive(true);
                Open();
                _currentStage = stage.OpenedWithWater;
                break;

            case stage.OpenedWithWater:
                break;


            case stage.ClosedWithEmptyWaiting:
                _emptyRoom.SetActive(true);
                Open();
                _currentStage = stage.OpenedWithEmpty;
                break;

            case stage.OpenedWithEmpty:
                if (PlayerIsInside())
                {
                    if (IsOpen)
                    {
                        Close();
                        break;
                    }

                    Open();
                    break;
                }

                Close();
                Destroy(_emptyRoom);
                _currentStage = stage.ClosedWithBananaWaiting;
                break;

            case stage.ClosedWithBananaWaiting:
                Open();
                _bananaRoom.SetActive(true); //GameManager.ShowBananaRoom.ShowRoom();
                _onBannanaRoomEnter.SetActive(true);
                _currentStage = stage.OpenedWithBanana;
                break;

            case stage.OpenedWithBanana:
                break;
            
            case stage.AfterKingBanana:
                _blackHall.SetBlackHall();
                Open();
                _currentStage = stage.InBlackHall;
                break;

            case stage.InBlackHall:
                break;

            case stage.NeverAgain:
                yield return StartCoroutine(IterateTexts(_neverAgain));
                break;
        }

        StateManager.SetState(StateManager.ExplorationState);
    }

    public IEnumerator Water()
    {
        yield return new WaitForSeconds(5f);

        var wait = true;
        while (wait)
        {
            if (StateManager.CurrentState != StateManager.ExplorationState)
            {
                yield return new WaitForSeconds(2f);
            }
            else wait = false;
        }

        AudioSource.PlayOneShot(_whaleSound);

        yield return new WaitForSeconds(3f);

        var waitAgain = true;
        while (waitAgain)
        {
            if (StateManager.CurrentState != StateManager.ExplorationState)
            {
                yield return new WaitForSeconds(1f);
            }
            else waitAgain = false;
        }

        StateManager.SetState(StateManager.InspectionState);

        yield return StartCoroutine(IterateTexts(_whaleCall));
        _currentStage = stage.ClosedWithWaterWaiting;

        StateManager.SetState(StateManager.ExplorationState);
    }

    public IEnumerator CloseWhaleRoom()
    {
        StateManager.SetState(StateManager.InspectionState);
        _cameraMovement.CameraLock = false;

        Close();
        Destroy(_whaleRoom); //MySceneManager.UnloadWhaleRoom();
        _toilet.StopWater();

        yield return StartCoroutine(IterateTexts(_closeWhaleRoom));

        _currentStage = stage.ClosedWithEmptyWaiting;
        StateManager.SetState(StateManager.ExplorationState);
    }

    public void OnWhaleWatch()
    {
        StartCoroutine(StareWhale());
    }

    public IEnumerator StareWhale()
    {
        StateManager.SetState(StateManager.InspectionState);
        _cameraMovement.CameraLock = false;

        AudioSource.PlayClipAtPoint(_whaleCloseClip, transform.position);
        
        yield return new WaitForSeconds(_whaleStare);

        yield return StartCoroutine(IterateTexts(_onWhaleWatch));

        StateManager.SetState(StateManager.ExplorationState);
    }

    public void KingBananaIsGone(BlackHall blackHall)
    {
        _currentStage = stage.AfterKingBanana;
        _blackHall = blackHall;
    }

    public void Finished()
    {
        _currentStage = stage.NeverAgain;
        Close(false);
    }

    public bool PlayerIsInside()
    {
        return _player.transform.position.z > 9;
    }

    public bool PlayerIsInsideSomewhatMore()
    {
        return _player.transform.position.z > 9.5 && _player.transform.position.x > 9;
    }

    public bool PlayerIsOutside()
    { 
        return _player.transform.position.z < 9;
    }

    public void ReadyToWater()
    {
        _currentStage = stage.ClosedWithWaterWaiting;
    }
}