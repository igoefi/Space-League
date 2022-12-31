using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    protected override void Fire()
    {
        float angle = -30;
        for(int i = 0; i < 5; i++){
            GameObject projectile = (GameObject)Instantiate(projectilePrefab, projectileSpauner.transform.position, projectileSpauner.transform.rotation);
            projectile.transform.rotation = Quaternion.Euler(0, projectile.transform.eulerAngles.y + angle, 0);
            projectile.GetComponent<ProjectileScript>().SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
            angle += 10;
        }
        



        // GameObject projectile_1 = Instantiate(projectilePrefab, projectileSpauner.transform.position+new Vector3(1,0,0), projectileSpauner.transform.rotation);
        // GameObject projectile_0 = Instantiate(projectilePrefab, projectileSpauner.transform.position, projectileSpauner.transform.rotation);
        // GameObject projectile_2 = Instantiate(projectilePrefab, projectileSpauner.transform.position - new Vector3(1, 0, 0), projectileSpauner.transform.rotation);
        // projectile_1.transform.rotation = Quaternion.Euler(projectile_0.transform.rotation.eulerAngles.x, projectile_0.transform.rotation.eulerAngles.y + 20, projectile_0.transform.rotation.eulerAngles.z);
        // projectile_2.transform.rotation = Quaternion.Euler(projectile_0.transform.rotation.eulerAngles.x, projectile_0.transform.rotation.eulerAngles.y - 20, projectile_0.transform.rotation.eulerAngles.z);

        // if (projectile_0.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript))
        // {
        //     projectileScript.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        // }
        // if (projectile_1.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript_1))
        // {
        //     projectileScript_1.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        // }
        // if (projectile_2.TryGetComponent<ProjectileScript>(out ProjectileScript projectileScript_2))
        // {
        //     projectileScript_2.SetParaments(damage, critChance, critDamageCoef, currentUpgradeLevel, bulletSpeed, bulletLife);
        // }
        CurrentAmmo--;
        _audio.PlayOneShot(shootSound);
        StartCoroutine(WaitForReady());
    }
}
