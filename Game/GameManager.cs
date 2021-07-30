using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject Player { get; private set; } = null;
    public static bool Sound { get; private set; } = true;
        
    public static bool ToggleSound()
    {
        Sound = !Sound;

        if (Sound)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }

        return Sound;
    }

    public static InteractionManager CurrentInteractionManager { get; set; }
    public static bool HasTvControl { get; set; } = false;
    public static bool BedroomReset { get; set; } = false;
    public static int BatteryCount { get; set; }

    /*
    public static ShowWhaleRoom ShowWhaleRoom { get; set; } = null;

    public static ShowEmptyRoom ShowEmptyRoom { get; set; } = null;

    public static ShowBananaRoom ShowBananaRoom { get; set; } = null;
    */


    void Start()
    {
        MySceneManager.LoadMainMenu();
    }

    public static void NewGame()
    {
        HasTvControl = false;
        BatteryCount = 0;
        BedroomReset = false;

        MySceneManager.LoadNewGame();
    }

    public void OnReadNote()
    {
        FindObjectOfType<MainDoor>().Unlock();
    }

    public static void SetPlayer(GameObject player)
    {
        Player = player;
    }
}
