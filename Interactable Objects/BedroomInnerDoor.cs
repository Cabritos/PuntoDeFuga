using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BedroomInnerDoor : Door
{
    [SerializeField] [TextArea] private string[] _tryToOpen1 = null;
    [SerializeField] private AudioClip _tryToOpenSound = null;

    private static bool _hasTried = false;

    private static BedroomDoor _bedroomDoor;
    private bool _wasCounted;

    public override void Awake()
    {
        base.Awake();

        _wasCounted = false;

        if (!_bedroomDoor)
        {
            _bedroomDoor = FindObjectOfType<BedroomDoor>();
        }
    }

    protected override IEnumerator StartInspection()
    {
        if (IsLocked)
        {
            AudioSource.PlayOneShot(_tryToOpenSound);

            if (!_hasTried)
            {
                yield return StartCoroutine(IterateTexts(_tryToOpen1));
                _hasTried = true;
            }
        }
        else
        {
            TogglePosition();

            yield return new WaitForEndOfFrame();
            
            if (!_wasCounted)
            {
                yield return StartCoroutine(_bedroomDoor.CountBedroomDoors());
                _wasCounted = true;
            }
        }

        StateManager.SetState(StateManager.ExplorationState);
    }
}
