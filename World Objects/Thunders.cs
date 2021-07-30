using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunders : MonoBehaviour
{
    [SerializeField] private GameObject[] _thunderLights = null;

    [SerializeField] private float _minIntervalTime = 0;
    [SerializeField] private float _maxIntervalTime = 0;

    [SerializeField] private float _minDurationTime = 0;
    [SerializeField] private float _maxDurationTime = 0;

    [SerializeField] private MeshRenderer[] _windows = null;
    [SerializeField] private int _glassPosition = 0;
    [SerializeField] private Material _thunderMaterial = null;

    private Color _thunderEmissionColor;

    //private Color _regularColor;
    private Color _regularEmmisionColor;

    private AudioSource _audioSource; //TODO wise
    [SerializeField] private AudioClip _thunder = null;

    void Awake()
    {
        //_regularColor = _windows[0].materials[_glassPosition].color;
        _regularEmmisionColor = _windows[0].materials[_glassPosition].GetColor("_EmissionColor");

        if (_thunderMaterial) {_thunderEmissionColor = _thunderMaterial.GetColor("_EmissionColor");}

        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(Storm());
    }

    private IEnumerator Storm()
    {
        if (_thunderLights == null) {StopCoroutine(Storm());}

        while (true)
        {
            ThundersOff();
            yield return new WaitForSeconds(Random.Range(_minIntervalTime, _maxIntervalTime));

            ThundersOn(false);
            yield return new WaitForSeconds(Random.Range(_minDurationTime / 5, _maxDurationTime / 5));

            ThundersOff();
            yield return new WaitForSeconds(Random.Range(_minDurationTime / 5, _maxDurationTime / 5));

            ThundersOn();
            yield return new WaitForSeconds(Random.Range(_minDurationTime, _maxDurationTime));
        }
    }

    private void ThundersOff()
    {
        foreach (var light in _thunderLights)
        {
            light.SetActive(false);
        }

        foreach (var window in _windows)
        {
            //window.materials[_glassPosition].color = _regularColor;
            window.materials[_glassPosition].SetColor("_EmissionColor", _regularEmmisionColor);
        }
    }

    private void ThundersOn(bool sound = true)
    {
        foreach (var light in _thunderLights)
        {
            light.SetActive(true);
        }

        foreach (var window in _windows)
        {
            window.materials[_glassPosition].SetColor("_EmissionColor", _thunderEmissionColor);
        }

        if (sound) _audioSource.PlayOneShot(_thunder); //TODO wwise
    }

    public void CastThunder(float duration = 1)
    {
        StopCoroutine(Storm());
        StartCoroutine(Thunder(duration));
        StartCoroutine(Storm());
    }
    
    private IEnumerator Thunder(float duration)
    {
        ThundersOff();
        yield return new WaitForSeconds(0.5f);
        ThundersOn();
        yield return new WaitForSeconds(duration);
        ThundersOff();
    }

    public void BananaThunder(float duration = 1)
    {
        StartCoroutine(BananaThunderCo(duration));
    }

    private IEnumerator BananaThunderCo(float duration)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSources[1].Stop();

        StopCoroutine(Storm());
        yield return StartCoroutine(Thunder(duration));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Storm());
        audioSources[1].Play();
    }
}
