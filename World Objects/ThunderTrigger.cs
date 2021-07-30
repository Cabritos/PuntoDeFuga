using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ThunderTrigger : MonoBehaviour
{
    private Thunders _thunders;
    [SerializeField] private float _duration = 1f;

    void Awake()
    {
        _thunders = FindObjectOfType<Thunders>();
    }

    void OnTriggerEnter(Collider c)
    {
        try
        {
            _thunders.CastThunder(_duration);
            Destroy(this);
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
