using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : InteractionManager
{
    [SerializeField] private Light[] _lights = null;
    [SerializeField] private MeshRenderer[] _meshRenderers =null;
    [SerializeField] private int _position = 0;

    [SerializeField] private Light[] _lightsOff = null;

    [SerializeField] private bool _isLit = false;

    [SerializeField] AudioClip _audioClip = null;

    void Start()
    {
        if (_isLit) {TurnOn();}
        else {TurnOff();}
    }

    protected override IEnumerator StartInspection()
    {
        Switch();

        yield return new WaitForEndOfFrame();

        StateManager.SetState(StateManager.ExplorationState);
    }

    public void Switch()
    {
        AudioSource.PlayClipAtPoint(_audioClip, transform.position); //TODO wwise

        if (_isLit)
        { 
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    protected void TurnOn()
    {
        foreach (Light light in _lights)
        {
            if (light == null)
            {
                Debug.LogError("Light missing");
                return;
            }

            light.transform.gameObject.SetActive(true);

            _isLit = true;
        }

        foreach (var meshRenderer in _meshRenderers)
        {
            if (meshRenderer == null)
            {
                Debug.LogError($"Mesh light missing {gameObject.name}");
                return;
            }

            meshRenderer.materials[_position].EnableKeyword("_EMISSION");
        }

        foreach (Light light in _lightsOff)
        {
            if (light == null)
            {
                return;
            }

            light.transform.gameObject.SetActive(false);
        }
    }


    protected void TurnOff()
    {
        foreach (Light light in _lights)
        {
            if (light == null)
            {
                Debug.LogError($"Light missing at {gameObject.name}");
                return;
            }

            light.transform.gameObject.SetActive(false);

            _isLit = false;
        }

        foreach (var meshRenderer in _meshRenderers)
        {
            if (meshRenderer == null)
            {
                Debug.LogError($"Mesh light missing {gameObject.name}");
                return;
            }
            meshRenderer.materials[_position].DisableKeyword("_EMISSION");
        }

        foreach (Light light in _lightsOff)
        {
            if (light == null)
            {
                return;
            }

            light.transform.gameObject.SetActive(true);
        }
    }
}
