using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // Tunables
    [Tooltip("Time for level in seconds")]
    [SerializeField] float levelTime = 60f;

    // Cached references
    Slider slider = null;
    LevelController levelController = null;
    Animator animator = null;

    private void Start()
    {
        slider = GetComponent<Slider>();
        levelController = FindObjectOfType<LevelController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Time.timeSinceLevelLoad / levelTime;
        HandleTimerUpdate();
    }

    private void HandleTimerUpdate()
    {
        if (levelController.CheckGameTimeActive())
        {
            bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
            if (timerFinished)
            {
                levelController.ReachedEndOfGameTime();
                animator.SetTrigger("setFoxToJump");
            }
        }
    }
}
