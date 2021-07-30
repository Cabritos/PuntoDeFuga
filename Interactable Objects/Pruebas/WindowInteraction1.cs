using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WindowInteraction1 : GenericInteraction
{
    public int Caca = 0;
    public override void DoSomeLogic()
    {
        Debug.Log("Doing some specific logic");
    }
}
