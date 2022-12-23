using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapons
{

    protected override  void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpauner.transform.position, projectileSpauner.transform.rotation);
        if (projectile.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript))
        {
            projectileScript.SetParaments(damage,critChance,critDamageCoef,currentUpgradeLevel,bulletSpeed, bulletLife);
        }
        _currentAmmo--;
        StartCoroutine(WaitForReady());
    }
    
}
