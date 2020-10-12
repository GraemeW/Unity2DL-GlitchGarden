using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    // Cached references
    LivesDisplay livesDisplay = null;
    LevelController levelController = null;

    private void Start()
    {
        livesDisplay = FindObjectOfType<LivesDisplay>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        DecrementLivesOnAttacker(otherCollider);
        levelController.CheckForLoseCondition();
    }

    private void DecrementLivesOnAttacker(Collider2D otherCollider)
    {
        Attacker attacker = otherCollider.GetComponent<Attacker>();
        if (attacker != null)
        {
            livesDisplay.DecrementLives(attacker.GetBaseDamage());
            Destroy(otherCollider.gameObject);
        }
    }
}
