using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    [SerializeField] private float _maxRotationX = 1f;
    [SerializeField] private float _maxRotationY = 1f;
    [SerializeField] private float _speed = 1f;

    private float _initialRotationX;
    private float _initialRotationY;

    [SerializeField] private float _rateMultiplier = 1f;
    [SerializeField] private float _rangeReduction = 40;
    [SerializeField] private float _yOffset = 0.1f;

    void Awake()
    {
        _initialRotationX = transform.rotation.eulerAngles.x;
        _initialRotationY = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(_maxRotationX * Mathf.Sin(Time.time * _speed) + _initialRotationX,
                                                _maxRotationY * Mathf.Sin(Time.time * _speed) + _initialRotationY, 0);

        var pos = transform.position;
        pos.y = Mathf.Sin(Time.time * _rateMultiplier) / _rangeReduction + _yOffset;

        transform.position = pos;
    }
}
