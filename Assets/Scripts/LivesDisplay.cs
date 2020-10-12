using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    // Tunables
    [SerializeField] int lives = 100;
    [SerializeField] int maxLives = 999;

    // Cached references
    TextMeshProUGUI lifeDisplay = null;

    public void Start()
    {
        lifeDisplay = GetComponent<TextMeshProUGUI>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        lifeDisplay.text = lives.ToString();
    }

    public void DecrementLives(int lifeHit)
    {
        lives = Mathf.Clamp(lives - lifeHit, 0, maxLives);
        UpdateDisplay();
    }

    public void IncremenentLives(int lifeAdder)
    {
        lives = Mathf.Clamp(lives + lifeAdder, 0, maxLives);
        UpdateDisplay();
    }

    public int GetLives()
    {
        return lives;
    }
}
