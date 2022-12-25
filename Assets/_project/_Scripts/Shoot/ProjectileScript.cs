using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    
    private float _damage;
    private float _speed;
    private float _lifeTime;
    private bool _isReady = false;
    
    

    private void Start()
    {
        
       
       
    }

    void Update()
    {
        if (!_isReady) return;

        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void SetParaments(float weaponDamage, float critChance, float critDamageCoef, int weaponLevel, float speed, float lifeTime)
    {
        _damage = weaponDamage * weaponLevel;

        if (Random.Range(0, 100) <= critChance)
            _damage *= critDamageCoef;

        _speed = speed;
        _lifeTime = lifeTime;

        _isReady = true;

        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Damage: " + _damage);
        Destroy(this.gameObject);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
