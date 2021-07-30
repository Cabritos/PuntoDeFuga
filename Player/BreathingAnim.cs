using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingAnim : MonoBehaviour
{
    [SerializeField] private float _rateMultiplier = 1f;
    [SerializeField] private float _rangeReduction = 40;
    [SerializeField] private float _yOffset = 0.1f; 

    void FixedUpdate()
    {
        var pos = transform.position;
        pos.y = Mathf.Sin(Time.time * _rateMultiplier) / _rangeReduction + _yOffset;

        transform.position = pos;
    }
}
