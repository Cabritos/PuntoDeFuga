using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRedButton : InteractionManager
{
    [SerializeField] [TextArea] private string _question = null;
    [SerializeField] [TextArea] private string[] _answers = null;

    [SerializeField] [TextArea] private string[] _answerYes = null;
    [SerializeField] [TextArea] private string[] _answerNo = null;

    [SerializeField] private AudioClip _pressButtonClip = null;
    [SerializeField] private AudioClip _blackSunRising = null;

    [SerializeField] private GameObject _blackHole = null;

    private CameraMovement _cameraMovement;
    private AudioSource _audioSource;

    [SerializeField] private GameObject _platform = null;
    [SerializeField] private GameObject _emptyRoom = null;
    [SerializeField] private MultiuseRoomDoor _door = null;

    private bool _unused = true;

    void Start()
    {
        _blackHole.SetActive(false);
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override IEnumerator StartInspection()
    {
        if (_unused)
        {
            yield return StartCoroutine(AskQuestion(_question, _answers));

            switch (CurrentOption)
            {
                case 0:
                    yield return StartCoroutine(IterateTexts(_answerYes));
                    break;

                case 1:
                    yield return StartCoroutine(IterateTexts(_answerNo));
                    break;
            }

            DialogManager.ExitTextBox();
            yield return new WaitForSeconds(1f);

            AudioSource.PlayClipAtPoint(_pressButtonClip, transform.position);
            _cameraMovement.CameraLock = false;

            yield return new WaitForSeconds(3f);

            AudioSource.PlayClipAtPoint(_blackSunRising, transform.position);
            yield return new WaitForSeconds(4f);

            var child = transform.GetChild(0).gameObject;
                child.SetActive(false);

            _emptyRoom.SetActive(false);
            _platform.SetActive(false);

            _door = FindObjectOfType<MultiuseRoomDoor>();
            var par = _door.transform.parent.gameObject;
            par.SetActive(false);

            _blackHole.SetActive(true);
            _blackHole.GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(5f);

            _audioSource.Play();

            yield return new WaitForSeconds(1.5f);

            _blackHole.GetComponent<AudioSource>().Stop();

            yield return new WaitForSeconds(0.5f);

            FindObjectOfType<BlackScreen>().TurnBlackOn();
            FindObjectOfType<BananaRoomHouseToggler>().HouseRestore();
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
