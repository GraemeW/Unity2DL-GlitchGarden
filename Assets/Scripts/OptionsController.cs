using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    // Tunables
    [SerializeField] Slider volumeSlider = null;
    [SerializeField] Slider difficultySlider = null;
    [SerializeField] float defaultVolume = 0.3f;
    [SerializeField] int defaultDifficulty = 1;

    // Cached References
    LevelLoader levelLoader = null;

    private void Start()
    {
        if (PlayerPrefsController.VolumeKeyExist())
        {
            volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        }
        else
        {
            volumeSlider.value = defaultVolume;
        }

        if (PlayerPrefsController.DifficultyKeyExist())
        {
            difficultySlider.value = PlayerPrefsController.GetDifficulty();
        }
        else
        {
            difficultySlider.value = defaultDifficulty;
        }

        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Update()
    {
        MusicOptions musicOptions = FindObjectOfType<MusicOptions>();
        if (musicOptions != null)
        {
            musicOptions.SetVolume(volumeSlider.value);
        }
    }

    public void SaveOptionsAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetDifficulty((int)Mathf.RoundToInt(difficultySlider.value));
        levelLoader.LoadLoadingScreen();
    }

    public void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
    }
}
