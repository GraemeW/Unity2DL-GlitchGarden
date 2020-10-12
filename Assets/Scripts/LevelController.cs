using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Tunables
    [SerializeField] GameObject winOverlay = null;
    [SerializeField] GameObject loseOverlay = null;

    // State
    int attackersAlive = 0;
    bool gameTimeActive = true;
    bool levelComplete = false;
    bool winToggle = false;

    // Cached references
    LivesDisplay livesDisplay = null;
    LevelLoader levelLoader = null;

    private void Start()
    {
        livesDisplay = FindObjectOfType<LivesDisplay>();
        levelLoader = FindObjectOfType<LevelLoader>();
        winOverlay.SetActive(false);
        loseOverlay.SetActive(false);
    }

    private void Update()
    {
        if (levelComplete && winToggle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                levelLoader.LoadNextScene();
            }
        }
    }

    public void IncrementAttackersAlive(int numberOfAttackers)
    {
        attackersAlive += numberOfAttackers;
    }

    public void DecrementAttackersAlive(int numberOfAttackers)
    {
        attackersAlive -= numberOfAttackers;
        if (attackersAlive <= 0 && !gameTimeActive)
        {
            RunWinCondition();
        }
    }

    public void ReachedEndOfGameTime()
    {
        gameTimeActive = false;
        AttackerSpawner[] attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner attackerSpawner in attackerSpawners)
        {
            attackerSpawner.StopSpawning();
        }
    }

    public bool CheckGameTimeActive()
    {
        return gameTimeActive;
    }

    public bool CheckLevelComplete()
    {
        return levelComplete;
    }

    public void CheckForLoseCondition()
    {
        if (livesDisplay.GetLives() <= 0)
        {
            RunLoseCondition();
        }
    }

    private void RunWinCondition()
    {
        if (!levelComplete)
        {
            levelComplete = true;
            winToggle = true;
            winOverlay.SetActive(true);
        }
    }

    private void RunLoseCondition()
    {
        if (!levelComplete)
        {
            levelComplete = true;
            loseOverlay.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
