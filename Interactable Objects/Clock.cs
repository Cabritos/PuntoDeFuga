using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject _1 = null;
    [SerializeField] private GameObject _2 = null;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(Coso());
    }

    private IEnumerator Coso()
    {
        while (true)
        {
            _audioSource.Play();
            _1.SetActive(true);
            _2.SetActive(false);
            yield return new WaitForSeconds(0.8f);
            _1.SetActive(false);
            _2.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
