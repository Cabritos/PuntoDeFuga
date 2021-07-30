using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInteraction : MonoBehaviour, IInteraction
{
    public virtual void DoSomeLogic()
    {
        Debug.Log("Doing some base logic.");
    }
}
