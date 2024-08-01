using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject spawnPoint;
    
    
    protected void Fire()
    {
        GameObject projecttile =  Instantiate(projectilePrefab, spawnPoint.transform.position, projectilePrefab.transform.rotation);
        
        // For fliping projecttile
        Vector3 newScale = projecttile.transform.localScale;
        if (transform.localScale.x < 0)
        {
            newScale.x *= -1;
        }

        projecttile.transform.localScale = newScale;
    }
    
}
