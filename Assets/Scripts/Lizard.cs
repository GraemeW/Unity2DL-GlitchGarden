using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // Cached references
    Attacker attacker = null;

    private void Start()
    {
        attacker = GetComponent<Attacker>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Check that attacker is in the correct lane
        if (Mathf.Approximately(transform.position.y - otherCollider.transform.position.y, 0f))
        {
            Defender hitDefender = otherCollider.GetComponent<Defender>();
            //Attacker interferenceAttacker = otherCollider.GetComponent<Attacker>();
            if (hitDefender != null) // simple action if adjacent a defender
            {
                    attacker.Attack(hitDefender);
            }
            /*  Deprecated -- allow attackers to stack
            else if (interferenceAttacker != null) // attack from range if adjacent another attacker
            {
                attacker.Attack(interferenceAttacker.GetCurrentTarget());
            }*/
        }
    }
}
