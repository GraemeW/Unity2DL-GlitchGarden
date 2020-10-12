using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Tunables
    [Header("Scene Listing")]
    const string SPLASH_SCENE_NAME = "SplashScreen";
    const string LOADING_SCENE_NAME = "StartScreen";
    const string OPTIONS_SCENE_NAME = "OptionsScreen";
    const string GAME_OVER_SCENE_NAME = "GameOverScreen";
    const string WIN_SCENE_NAME = "WinScreen";

    [Header("Scene Parameters")]
    [SerializeField] float splashDelayTime = 4.0f;

    // States
    int currentSceneIndex = 0;

    private void Start()
    {
        // Overall tracking scene indices
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Flip splash to main
        if (SceneManager.GetActiveScene().name.Equals(SPLASH_SCENE_NAME))
        {
            StartCoroutine(SplashDelayToLoad());
        }

        MusicOptions musicOptions = FindObjectOfType<MusicOptions>();
        if (musicOptions != null)
        {
            musicOptions.InitializeMusic();
        }
    }

    public void LoadSplashScreen()
    {
        SceneManager.LoadScene(SPLASH_SCENE_NAME);
    }

    public void LoadLoadingScreen()
    {
        SceneManager.LoadScene(LOADING_SCENE_NAME);
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene(OPTIONS_SCENE_NAME);
    }

    public void LoadGameOverScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GAME_OVER_SCENE_NAME);
    }

    public void LoadNextScene()
    {
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 3) // indexed as:  next level (-1), lose screen (-1), win screen (-1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(WIN_SCENE_NAME);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator SplashDelayToLoad()
    {
        yield return new WaitForSeconds(splashDelayTime);
        LoadLoadingScreen();
    }

    public bool IsLoadingScreen()
    {
        if (SceneManager.GetActiveScene().name.Equals(LOADING_SCENE_NAME) || SceneManager.GetActiveScene().name.Equals(OPTIONS_SCENE_NAME))
        {
            return true;
        }
        else { return false; }
    }

    public bool IsWinScreen()
    {
        if (SceneManager.GetActiveScene().name.Equals(WIN_SCENE_NAME))
        {
            return true;
        }
        else { return false; }
    }

    public bool IsLoseScreen()
    {
        if (SceneManager.GetActiveScene().name.Equals(GAME_OVER_SCENE_NAME))
        {
            return true;
        }
        else { return false; }
    }
}
