using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private string _targetName = "Player";

    [SerializeField] private float _heightOffset = 1.5f;

    [SerializeField] private float _distance;
    [SerializeField] private float _minDistance = 5f;
    [SerializeField] private float _maxDistance = 30f;

    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 1f;

    private Vector3 _vectorHeightOffset;
    private float _x;
    private float _y;
    private float _previousDistance;

    private float _zoomInput;
    private float _rotationInput;
    
    public bool CameraLock { get; set; }


    void Awake()
    {
        if (_target == null)
        {
            _target = GameObject.Find(_targetName).transform;
        }

        CameraLock = false; //ver según guión
    }

    void Start()
    {
        var angles = transform.eulerAngles;
        _x = angles.y;
        _y = angles.x;
        _vectorHeightOffset = new Vector3(0, _heightOffset,0);
    }

    void Update()
    {
        _zoomInput = InputManager.VerticalCamera;
        _rotationInput = InputManager.HorizontalCamera;
        _vectorHeightOffset = new Vector3(0, _heightOffset, 0);
    }

    void LateUpdate()
    {
        if (CameraLock) return;

        if (_distance < _minDistance) _distance  = _minDistance;
        if (_distance > _maxDistance) _distance = _maxDistance;

        _distance -= _zoomInput * 0.02f * _zoomSpeed;
        _x += _rotationInput * 0.02f * _rotationSpeed;
        
        var rotation = Quaternion.Euler(_y, _x, 0);
        var position = rotation * new Vector3(0f, 0f, -_distance) + _target.position + _vectorHeightOffset;
        
        transform.rotation = rotation;
        transform.position = position;

        if (Math.Abs(_previousDistance - _distance) > 0.001f)
        {
            _previousDistance = _distance;
            var rot = Quaternion.Euler(_y, _x, 0);
            var po = rot * new Vector3(0f, 0f, -_distance) + _target.position + _vectorHeightOffset;
            transform.rotation = rot;
            transform.position = po;
        }
    }
}
