using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Transform _transform;

    private float _damage;
    private float _speed;

    private float _lifeTime;

    private bool _isReady = false;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (!_isReady) return;

        _transform.position += _transform.forward * _speed * Time.deltaTime;
    }

    public void SetParaments(float baseDamage, float critChance, float critDamageCoef, int weaponLevel, float speed, float lifeTime)
    {
        _damage = baseDamage * weaponLevel;

        if (Random.Range(0, 100) <= critChance)
            _damage *= critDamageCoef;

        _speed = speed;
        _lifeTime = lifeTime;

        _isReady = true;

        StartCoroutine(Destroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //для соприкосновения с кем-либо

    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
