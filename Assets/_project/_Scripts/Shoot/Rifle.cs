using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapons
{
    protected override void Fire()
    {
        GameObject projectile =  Instantiate(projectilePrefab, projectileSpauner.transform.position, projectileSpauner.transform.rotation);
        if (projectile.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript))
        {
            projectileScript.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        }
        CurrentAmmo--;
        _audio.PlayOneShot(shootSound);
        StartCoroutine(WaitForReady());
    }
}
