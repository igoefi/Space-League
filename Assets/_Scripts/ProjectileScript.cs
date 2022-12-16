using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Transform projectile;
    public Weapons weapons;
    private float _damage;
    void Start()
    {
        if (Random.Range(0, 100) <= weapons.critChance)
        {
            _damage = weapons.damage * weapons.currentUpgradeLevel * (weapons.critDamage / 100);
        }
        else
        {
            _damage = weapons.damage * weapons.currentUpgradeLevel;
        }
    }

    
    void Update()
    {
        if(projectile.position.x>=200|| projectile.position.x <= -200|| projectile.position.y >= 200|| projectile.position.y <= -200|| projectile.position.z >= 200|| projectile.position.z <= -200)
        {
            Destroy(this.gameObject);
        }
    }
}
