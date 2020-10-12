using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
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
            if (hitDefender != null)
            {
                if (hitDefender.IsJumpable())
                {
                    attacker.Jump();
                }
                else
                {
                    attacker.Attack(hitDefender);
                }
            }
        }
    }
}
