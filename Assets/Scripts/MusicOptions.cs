using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOptions : MonoBehaviour
{
    // Tunables
    [SerializeField] AudioClip loadingAndOptionsMusic = null;
    [SerializeField] AudioClip gamePlayMusic = null;
    [SerializeField] AudioClip endGameWin = null;
    [SerializeField] AudioClip endGameLose = null;
    [Range(0f, 1f)] [SerializeField] float musicVolume = 0.3f;

    // Cached References
    AudioSource audioSource = null;
    LevelLoader levelLoader = null;

    private void Awake()
    {
        int StartOptionsMusicCount = FindObjectsOfType<MusicOptions>().Length;
        if (StartOptionsMusicCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        levelLoader = FindObjectOfType<LevelLoader>();
        audioSource.loop = true;

        SetInitialVolume();
        InitializeMusic();
    }


    public void InitializeMusic()
    {
        audioSource.volume = musicVolume;
        if (levelLoader.IsLoadingScreen())
        {
            if (audioSource.clip != loadingAndOptionsMusic)
            {
                audioSource.clip = loadingAndOptionsMusic;
                audioSource.Play();
            }
            // otherwise, if it's already playing, keep playing
        }
        else if (levelLoader.IsWinScreen())
        {
            audioSource.clip = endGameWin;
            audioSource.Play();
        }
        else if (levelLoader.IsLoseScreen())
        {
            audioSource.clip = endGameLose;
            audioSource.Play();
        }
        else // main game music
        {
            if (audioSource.clip != gamePlayMusic)
            {
                audioSource.clip = gamePlayMusic;
                audioSource.Play();
            }
        }
    }

    private void SetInitialVolume()
    {
        if (PlayerPrefsController.VolumeKeyExist())
        {
            SetVolume(PlayerPrefsController.GetMasterVolume());
        }
        else
        {
            SetVolume(musicVolume);
        }
    }

    public void SetVolume(float volumeSetting)
    {
        musicVolume = volumeSetting;
        audioSource.volume = musicVolume;
    }
}
