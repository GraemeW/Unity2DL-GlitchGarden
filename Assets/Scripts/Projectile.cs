using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Tunables
    [Header("Fire Details")]
    [SerializeField] float projectileDamage = 50.0f;
    [SerializeField] float projectileForce = 1.0f;
    [SerializeField] bool isRotating = false;
    [SerializeField] float rotationForce = 20.0f;
    [SerializeField] AudioClip hitSFX = null;
    [SerializeField] float hitSFXVolume = 0.3f;

    // Cached references
    Rigidbody2D projectileRigidbody2D = null;

    // Start is called before the first frame update
    void Awake()
    {
        projectileRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        projectileRigidbody2D.AddForce(direction * projectileForce);
        if (isRotating)
        {
            projectileRigidbody2D.AddTorque(rotationForce);
        }
    }

    public void SetProjectileForce(float projectileForce)
    {
        this.projectileForce = projectileForce;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Deal damage & destroy projectile itself after hit
        Health health = otherCollider.GetComponent<Health>();
        if (health != null)
        {
            health.DealDamage(projectileDamage);
        }
        if (hitSFX != null)
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume * PlayerPrefsController.GetMasterVolume());
        }
        Destroy(gameObject);
    }
}
