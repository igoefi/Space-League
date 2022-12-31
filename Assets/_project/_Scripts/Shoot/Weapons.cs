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
    [Header("Sounds")]
    [SerializeField] protected AudioClip shootSound;
    [SerializeField] protected AudioClip reloadingSound;
    protected AudioSource _audio;
    protected bool _reloading = false;
    protected bool _readyToFire = true;
    public int CurrentAmmo { get; protected set; }

    

    private void Start()
    {
        CurrentAmmo = ammo;
        _audio = GetComponentInChildren<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0)&&_readyToFire&&CurrentAmmo!=0&&!_reloading) 
            Fire();

        if (CurrentAmmo == 0 || Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(Reload()); 
        }
        
    }
    protected abstract void Fire();


    protected IEnumerator Reload() 
    { 
        _reloading = true;
        CurrentAmmo = ammo;
        _audio.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadTime);
        _reloading = false;

    }

    public string Ammo()
    {
        if (_reloading) return "Reloading";
        else return CurrentAmmo + "/" + ammo;
    }

    protected IEnumerator WaitForReady()
    {
        _readyToFire = false;
        yield return new WaitForSeconds(fireInterval);
        _readyToFire = true;
    }
}