using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractionManager
{
    [SerializeField] [TextArea] private string[] _textOneBattery = null;
    [SerializeField] [TextArea] private string[] _textSecondBattery = null;
    [SerializeField] [TextArea] private string[] _textMissingBattery = null;

    [SerializeField] private bool _startsActive = true;

    [SerializeField] private AudioClip _pickUpClip = null;

    void Start()
    {
        if (!_startsActive)
        {
            gameObject.SetActive(false);
        }
    }

    protected override IEnumerator StartInspection()
    {
        AudioSource.PlayClipAtPoint(_pickUpClip, transform.position);

        if (GameManager.BatteryCount == 0)
        {
            yield return StartCoroutine(IterateTexts(_textOneBattery));
        }
        else if (!GameManager.HasTvControl)
        {
            yield return StartCoroutine(IterateTexts(_textSecondBattery));
        }
        else
        {
            yield return StartCoroutine(IterateTexts(_textMissingBattery));
        }
        
        GameManager.BatteryCount++;
        StateManager.SetState(StateManager.ExplorationState);

        Destroy(gameObject);
    }
}
