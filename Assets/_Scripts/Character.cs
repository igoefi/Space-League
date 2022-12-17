using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField]protected float _maxHp;
    [SerializeField]private float _hp;
    public float HP{
        get => _hp;
    }
    [SerializeField]protected float _maxStamina;
    [SerializeField]private float _stamina;
    public float Stamina{
        get => _stamina;
    }

    [SerializeField]protected Armor Armor;
    [SerializeField]private float _armor;

    // private float _timerStamina;
    private float _timerArmor;

    protected void Start() {
        _hp = _maxHp;
        _stamina = _maxStamina;

        if(Armor != null){
            _armor = Armor.Durability;
            Armor.InitDictionary();
        }
    }

    protected void Update(){
        if(_hp <= 0){
            Die();
        }

        StaminaRecovery();
        if(Armor != null && _armor != Armor.Durability){
            ArmorRecovery();

        }
    }
    public void SetMaxHp(float amount){
        _maxHp += amount;
    }
    public void SetMaxStamina(float amount){
        _maxStamina += amount;
    }

    public virtual void DecreaseStamina(float amount){
        _stamina -= amount;
    }

    protected virtual void ArmorRecovery(){
        if(_armor < Armor.Durability && _timerArmor <= 0){
            _armor += Armor.SpeedRegen * Time.deltaTime;
            return;
        }
        _timerArmor -= Time.deltaTime;
    } 
    protected virtual void StaminaRecovery(){
        if(_stamina < _maxStamina){
            _stamina += 5 * Time.deltaTime;
        }
    }

    public virtual void HpRecovery(float amount){
        _hp += amount;
        _hp = _hp > _maxHp ? _maxHp : _hp;
    }
    public virtual void TakeDamage(float damage, DamageType type){
        if(_armor > 0){
            _armor -= damage / Armor.Resistance[type];
            _timerArmor = Armor.CooldownRegen;
            if(_armor < 0){
                _hp -= -_armor;
                _armor = 0;
            }
        }
        else{
            _hp -= damage;
        }

    }
    protected virtual void Die(){
        Debug.Log("Lol you die");
    }
}
