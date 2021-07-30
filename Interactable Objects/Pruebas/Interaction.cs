using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Interaction
{
    [SerializeField] private string _text = null;
    [SerializeField] public GenericInteraction _logic;

    public void DoSomeLogic()
    {
        Debug.Log(_text);
        if (_logic == null)
        {
            Debug.LogError($"No logic");
            return;
        }
    
        _logic.DoSomeLogic();
    }
}
