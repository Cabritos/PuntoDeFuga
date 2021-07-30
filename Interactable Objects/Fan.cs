using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, _speed * 0.1f, 0);
    }
}
