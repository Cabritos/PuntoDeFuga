using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer = null;

    [SerializeField] private int _skinMaterialPosition = 0;
    [SerializeField] private int _eyesMaterialPosition = 1;

    private Color _skinMaterial;
    private Color _eyesMaterial;

    [SerializeField] private float _openTime = 0;
    [SerializeField] private float _closedTime = 0;
    
    void Awake()
    {
        if (!_meshRenderer)
        {
            GetComponent<MeshRenderer>();
        }

        _eyesMaterial = _meshRenderer.materials[_eyesMaterialPosition].color;
        _skinMaterial = _meshRenderer.materials[_skinMaterialPosition].color;
    }

    void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            _meshRenderer.materials[_eyesMaterialPosition].color = _eyesMaterial;
            yield return new WaitForSeconds(_openTime);
            _meshRenderer.materials[_eyesMaterialPosition].color = _skinMaterial;
            yield return new WaitForSeconds(_closedTime);
        }

    }
}
