using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Tunables
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject gun = null;
    [SerializeField] float projectileOffsetPosition = 0.5f;
    [SerializeField] float projectileInitialRotation = 60f;
    [SerializeField] AudioClip shootSFX = null;
    [SerializeField] float shootSFXVolume = 0.4f;

    // State
    AttackerSpawner myLaneSpawner = null;
    Animator animator = null;
    GameObject projectilesParent = null;
    const string PROJECTILES_PARENT_NAME = "Projectiles";

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetLaneSpawner();

        CreateProjectilesParent();
    }

    private void CreateProjectilesParent()
    {
        projectilesParent = GameObject.Find(PROJECTILES_PARENT_NAME);
        if (projectilesParent == null)
        {
            projectilesParent = new GameObject(PROJECTILES_PARENT_NAME);
        }
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        if (attackerSpawners != null)
        {
            foreach (AttackerSpawner attackerSpawner in attackerSpawners)
            {
                if (Mathf.Approximately(attackerSpawner.transform.position.y - transform.position.y, 0f))
                {
                    myLaneSpawner = attackerSpawner;
                }
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Fire()
    {
        GameObject shot = Instantiate(projectile, gun.transform.position + projectileOffsetPosition * Vector3.right, Quaternion.Euler(new Vector3(projectileInitialRotation, 0, 0)));
        shot.transform.parent = projectilesParent.transform;
        Projectile shotProjectile = shot.GetComponent<Projectile>();
        shotProjectile.Launch(Vector2.right);
        if (shootSFX != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume * PlayerPrefsController.GetMasterVolume());
        }
    }
}
