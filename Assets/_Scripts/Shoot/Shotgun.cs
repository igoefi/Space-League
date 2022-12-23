using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    protected override void Fire()
    {
        GameObject projectile_1 = Instantiate(projectilePrefab, projectileSpauner.transform.position+new Vector3(1,0,0), projectileSpauner.transform.rotation);
        GameObject projectile = Instantiate(projectilePrefab, projectileSpauner.transform.position, projectileSpauner.transform.rotation);
        GameObject projectile_2 = Instantiate(projectilePrefab, projectileSpauner.transform.position - new Vector3(1, 0, 0), projectileSpauner.transform.rotation);
        projectile_1.transform.rotation = Quaternion.Euler(projectile_1.transform.rotation.eulerAngles.x, projectile_1.transform.rotation.eulerAngles.y + 20, projectile_1.transform.rotation.eulerAngles.z);
        projectile_2.transform.rotation = Quaternion.Euler(projectile_1.transform.rotation.eulerAngles.x, projectile_1.transform.rotation.eulerAngles.y - 20, projectile_1.transform.rotation.eulerAngles.z);

        if (projectile.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript))
        {
            projectileScript.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        }
        if (projectile_1.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript_1))
        {
            projectileScript_1.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        }
        if (projectile_2.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript_2))
        {
            projectileScript_2.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        }
        _currentAmmo--;
        StartCoroutine(WaitForReady());
    }
}
