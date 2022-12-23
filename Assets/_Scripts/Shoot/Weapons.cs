using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [Header("Prefabs and Scripts")]
    public GameObject projectileSpauner;
    public GameObject projectilePrefab;
    [Header("Damage")]
    [SerializeField] protected float damage;
    [SerializeField] protected float fireInterval;
    [SerializeField] protected float critChance;
    [SerializeField] protected float critDamageCoef;
    [Header("Other")]
    [SerializeField] protected int ammo;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLife;
    [SerializeField] protected int currentUpgradeLevel;
    [SerializeField] protected int maxUpgradeLevel;
    protected bool _reloading = false;
    protected bool _readyToFire = true;
    protected int _currentAmmo;
    

    private void Start()
    {
        _currentAmmo = ammo;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&&_readyToFire&&_currentAmmo!=0&&!_reloading) 
            Fire();

        if (_currentAmmo == 0 || Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(Reload()); 
        }
        
    }
     protected virtual  void Fire()
    {
        
    }

    protected IEnumerator Reload() 
    { 
        _reloading = true;
        _currentAmmo = ammo;
        yield return new WaitForSeconds(reloadTime);
        _reloading = false;
    }

    public string Ammo()
    {
        if (_reloading) return "Reloading";
        else return _currentAmmo + "/" + ammo;
    }

    protected IEnumerator WaitForReady()
    {
        _readyToFire = false;
        yield return new WaitForSeconds(fireInterval);
        _readyToFire = true;
    }
}