using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHallPiece : MonoBehaviour
{
    [SerializeField] private GameObject _podium = null;
    [SerializeField] private bool _isLast = false;

    private Light _light;
    [SerializeField] private AudioClip _audioClip = null;

    void Awake()
    {
        _light = GetComponentInChildren<Light>();
    }

    void Start()
    {
        _light.gameObject.SetActive(false);
        if (_podium) { _podium.SetActive(false); }
    }

    void OnTriggerEnter(Collider c)
    {
        _light.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(_audioClip, FindObjectOfType<CameraMovement>().gameObject.transform.position);

        if (_isLast)
        {
            _podium.SetActive(true);
        }
    }
}
