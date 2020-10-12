using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Attacker : MonoBehaviour
{
    // Tunables
    [Header("Attacker Stats")]
    //[SerializeField] public float damagePerHit = 20f; - embedded in attack animation, for reasons
    [SerializeField] int baseDamage = 1;

    [Header("Animator Properties")]
    float currentSpeed = 1.0f;
    [SerializeField] AudioClip biteSFX = null;
    float biteSFXVolume = 0.4f;

    // State
    Defender currentTarget = null;

    // Cached references
    Animator animator = null;
    Health health = null;
    LevelController levelController = null;

    private void Awake()
    {
        levelController = FindObjectOfType<LevelController>();
        levelController.IncrementAttackersAlive(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed);
        UpdateAnimationState();
    }

    public void OnDestroy()
    {
        if (levelController != null)
        {
            levelController.DecrementAttackersAlive(1);
        }
    }

    private void UpdateAnimationState()
    {
        if (currentTarget == null)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void Attack(Defender hitDefender)
    {
        animator.SetBool("isAttacking", true);
        currentTarget = hitDefender;
    }

    public void Jump()
    {
        SetMovementSpeed(2.0f);
        animator.SetTrigger("jumpTrigger");
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (currentTarget != null)
        {
            Health targetHealth = currentTarget.GetComponent<Health>();
            if (health != null)
            {
                targetHealth.DealDamage(damage);
            }
        }
    }

    public void BiteSFX()
    {
        if (biteSFX != null)
        {
            AudioSource.PlayClipAtPoint(biteSFX, Camera.main.transform.position, biteSFXVolume * PlayerPrefsController.GetMasterVolume());
        }
    }

    public Defender GetCurrentTarget()
    {
        return currentTarget;
    }

    public Health GetHealth()
    {
        return health;
    }

    public int GetBaseDamage()
    {
        return baseDamage;
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }
}
