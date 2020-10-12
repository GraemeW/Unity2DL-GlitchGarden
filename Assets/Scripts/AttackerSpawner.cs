using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    // Tunables
    [Header("Spawner Properties")]
    [SerializeField] bool spawn = true;
    [SerializeField] Attacker[] attackerPrefabs = null;
    [SerializeField] float minSpawnTime = 1.0f;
    [SerializeField] float maxSpawnTime = 5.0f;
    [SerializeField] bool delayedEnable = false;
    [SerializeField] float timeDelay = 30f;

    IEnumerator Start()
    {
        DifficultyModifierBySpawnerRate(); // adjust difficulty on player pref
        while (spawn)
        {
            if (delayedEnable)
            {
                yield return new WaitForSeconds(timeDelay);
                delayedEnable = false;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
                SpawnAttacker();
            }
        }
    }

    private void SpawnAttacker()
    {
        int randomAttackerIndex = Random.Range(0, attackerPrefabs.Length);
        Spawn(attackerPrefabs[randomAttackerIndex]);
    }

    private void Spawn(Attacker attacker)
    {
        Attacker newAttacker = Instantiate(attacker, transform.position, transform.rotation);
        newAttacker.transform.parent = transform; // move instance as child of current spawner parent
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void DifficultyModifierBySpawnerRate()
    {
        // Difficulty modifier -- 
        // Increase time for spawn at low difficulty, decrease time for spawn at high difficulty
        if (PlayerPrefsController.DifficultyKeyExist())
        {
            if (PlayerPrefsController.GetDifficulty() == 0)
            {
                minSpawnTime += 1f;
                maxSpawnTime += 1f;
            }
            else if (PlayerPrefsController.GetDifficulty() == 2)
            {
                minSpawnTime -= 0.5f;
                maxSpawnTime -= 0.5f;
            }
        }
    }
}
