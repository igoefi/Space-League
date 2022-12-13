using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField]protected float _maxHp;
    [SerializeField]protected float _hp;
    public float HP{
        get => _hp;
    }
    [SerializeField]protected float _maxStamina;
    [SerializeField]protected float _stamina;
    public float Stamina{
        get => _stamina;
    }

    protected Dictionary<DamageType, float> _resistance = new Dictionary<DamageType, float>();

    private void Start() {
        //init dictionary
        _resistance[DamageType.Physical] = 1;
        _resistance[DamageType.Fire] = 1;
        _resistance[DamageType.Shock] = 1;

        _hp = _maxHp;
        _stamina = _maxStamina;
    }

    protected void Update(){
        if(_hp <= 0){
            Die();
        }

        if(_stamina < _maxStamina){
            _stamina += 5 * Time.deltaTime;
        }
    }

    public void DecreaseStamina(float amount){
        _stamina -= amount;
    }
    public void TakeDamage(float damage, DamageType type){
        _hp -= damage / _resistance[type];
    }
    protected virtual void Die(){
        Debug.Log("Lol you die");
    }
}
