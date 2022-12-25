using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    //Пока что все публичное после тестов урона и дамага ненужное заприватим или запротектим  
    [SerializeField] GameObject projectileSpauner;
    public Rigidbody projectilePrefab;
    public float damage;
    public float fireInterval;
    public float critChance;
    public float critDamage;
    public int ammo;
    public float reloadTime;
    public int currentUpgradeLevel;
    public int maxUpgradeLevel;
    protected bool _reloading = false;
    protected bool _readyToFire = true;
    protected float _timeOfFire;
    protected int _currentAmmo;
    

    private void Start()
    {
        _currentAmmo = ammo;
    }
    private void Update()
    {
        if (Time.time - _timeOfFire >= fireInterval&&!_reloading) { _readyToFire= true; }
        if(Input.GetMouseButtonDown(0)||Input.GetMouseButton(0)&&_readyToFire&&_currentAmmo!=0&&_currentAmmo!=0) { Fire(); }
        if (_currentAmmo == 0 || Input.GetKeyDown(KeyCode.R)) 
        {
            _reloading = true;
            Invoke("Reload", reloadTime); 
        }
        
    }

    /*
    protected void Fire()
    {
        Rigidbody projectile = Instantiate(projectilePrefab, projectileSpauner.transform.position, Quaternion.identity);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position,targetPosition,20f*Time.deltaTime);
            _currentAmmo--;
            _readyToFire = false;
            _timeOfFire = Time.time;
        }
    }*/

    abstract public void Fire();


    protected void Reload() 
    { 
        _currentAmmo= ammo;
        _reloading = false;
    }

    public string Ammo()
    {
        return _currentAmmo + "/" + ammo;
    }
}