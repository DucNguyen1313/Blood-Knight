using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage = 15;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected Vector2 knockback = Vector2.zero;

    [SerializeField] protected Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(moveSpeed * transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        
        if(damageable == null) return;
        
        // Face-to-face other when knockbacking
        Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

        bool gotHit = damageable.Hit(damage, deliveredKnockback);
        if (gotHit)
        {
            Debug.Log("attack " + damage.ToString());
            Destroy(gameObject);
        }

    }
}
