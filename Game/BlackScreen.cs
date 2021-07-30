using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public void TurnBlackOn()
    {
        GetComponent<Image>().enabled = true;
    }

    public void TurnBlackOff()
    {
        GetComponent<Image>().enabled = false;
    }
}
