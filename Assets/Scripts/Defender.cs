using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    // Tunables
    [Header("Defender Stats")]
    [SerializeField] int starCost = 100;
    [SerializeField] bool jumpable = false;
    [SerializeField] AudioClip placementSound = null;

    // State
    int starIncrementModifierByDifficulty = 0;

    // Cached references
    Health health = null;
    StarDisplay starDisplay = null;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
        starDisplay = FindObjectOfType<StarDisplay>();

        DifficultyModifierByStarGeneration();
    }

    public Health GetHealth()
    {
        return health;
    }

    public int GetStarCost()
    {
        return starCost;
    }
    
    public void AddStars(int starIncrement)
    {
        starDisplay.AddStars(starIncrement + starIncrementModifierByDifficulty);
    }

    public bool IsJumpable()
    {
        return jumpable;
    }

    public AudioClip GetPlacementSound()
    {
        return placementSound;
    }

    private void DifficultyModifierByStarGeneration()
    {
        // Difficulty modifier -- 
        // Increase cashflow rate for low difficulty, decrease for high difficulty
        if (PlayerPrefsController.DifficultyKeyExist())
        {
            if (PlayerPrefsController.GetDifficulty() == 0)
            {
                starIncrementModifierByDifficulty = 1;
            }
            else if (PlayerPrefsController.GetDifficulty() == 2)
            {
                starIncrementModifierByDifficulty = -1;
            }
        }
    }
}
