using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] protected float attackDamage = 10;

    [SerializeField] protected Vector2 knockback = Vector2.zero;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        if (damageable == null) return;
        
        // Face-to-face other when knockbacking
        Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        
        bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
        if(gotHit) 
            Debug.Log("attack " + attackDamage.ToString());
    }

    
}
