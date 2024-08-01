using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] protected float heathRestore = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable == null) return;

        damageable.Heal(heathRestore);
        Destroy(gameObject);
    }
}
