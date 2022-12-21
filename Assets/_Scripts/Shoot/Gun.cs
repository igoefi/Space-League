using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapons
{

    protected override  void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpauner.transform.position, Quaternion.identity);
        if (projectile.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript))
        {
            projectileScript.SetParaments(1, 1, 1, 1, 1, 10);
        }
        _currentAmmo--;
        StartCoroutine(WaitForReady());
    }
    
}
