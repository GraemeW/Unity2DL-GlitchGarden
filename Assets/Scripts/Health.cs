using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Tunables
    [SerializeField] float health = 100.0f;
    [SerializeField] Vector3 deathOffset;
    [SerializeField] GameObject deathVFX = null;
    [SerializeField] AudioClip deathSFX = null;
    [SerializeField] float deathSFXVolume = 0.6f;

    private void Start()
    {
        if (deathOffset == null)
        {
            deathOffset = new Vector3(0f, 0f, 0f);
        }
    }

    public void DealDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0f, health);
        if (health <= 0f)
        {
            TriggerDeathVFX();
            TriggerDeathSFX();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if (deathVFX != null)
        {
            GameObject currentDeathVFX = Instantiate(deathVFX, transform.position + deathOffset, transform.rotation);
            Destroy(currentDeathVFX, 2f);
        }
    }

    private void TriggerDeathSFX()
    {
        if (deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume * PlayerPrefsController.GetMasterVolume());
        }
    }
}
