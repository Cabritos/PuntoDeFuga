using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

static class MySceneManager
{
    public static string MainMenuSceneName = "Main Menu";
    public static string GameSceneName = "House Main";
    
    public static void LoadNewGame()
    {
        SceneManager.UnloadSceneAsync(MainMenuSceneName);
        SceneManager.LoadScene(GameSceneName, LoadSceneMode.Additive);
    }   


    public static void LoadMainMenu()
    {
        Scene gameScene = SceneManager.GetSceneByName(GameSceneName);
        if (gameScene.isLoaded) SceneManager.UnloadSceneAsync(gameScene);

        SceneManager.LoadScene(MainMenuSceneName, LoadSceneMode.Additive);
    }

    private static void LoadAsync(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
