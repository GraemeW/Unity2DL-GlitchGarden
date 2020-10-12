using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    // Keys
    const string MASTER_VOLUME_KEY = "masterVolume";
    const string DIFFICULTY_KEY = "difficulty";

    // Parameters
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    const int MIN_DIFFICULTY = 0;
    const int MAX_DIFFICULTY = 2;

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME));
    }

    public static void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(DIFFICULTY_KEY, (int)Mathf.RoundToInt(Mathf.Clamp((float)difficulty, (float)MIN_DIFFICULTY, (float)MAX_DIFFICULTY)));
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static int GetDifficulty()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }

    public static bool VolumeKeyExist()
    {
        return PlayerPrefs.HasKey(MASTER_VOLUME_KEY);
    }

    public static bool DifficultyKeyExist()
    {
        return PlayerPrefs.HasKey(DIFFICULTY_KEY);
    }
}
