using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    protected float _maxHp = 100;
    protected float _bonusMaxHp;
    [SerializeField]protected float _hp;
    public float HP{
        get => _hp;
    }
    

    [SerializeField]protected Armor Armor;
    [SerializeField]protected float _armor;

    private bool _onNegativeEffect = false;
    public delegate void OnNegativeEffect();
    public OnNegativeEffect OnNegativeEffectEvent;

    // private float _timerStamina;
    protected float _timerArmor;

    protected void Start() {
        _hp = _maxHp + _bonusMaxHp;

        if(Armor != null){
            _armor = Armor.Durability;
            Armor.Init();
        }
    }
    

    protected virtual void ArmorRecovery(){
        if(_armor < Armor.Durability && _timerArmor <= 0){
            _armor += Armor.SpeedRegen * Time.deltaTime;
            return;
        }
        _timerArmor -= Time.deltaTime;
    } 
    public virtual void TakeDamage(float damage, DamageType type){
        if(_armor > 0){
            _armor -= damage / Armor.Resistance[type];
            _timerArmor = Armor.CooldownRegen;
            if(_armor < 0){
                _hp -= -_armor;
                _armor = 0;
            }
            Debug.Log("Damage take " + damage / Armor.Resistance[type]);
        }
        else{
            _hp -= damage;
            Debug.Log("Damage take " + damage);
        }
        
    }
    protected IEnumerator TakePoisonDamage(){
        if(!_onNegativeEffect){
            _onNegativeEffect = true;
            float timer = 0;
            while(timer < 3){
                TakeDamage(5 * Time.deltaTime, DamageType.Shock);
                OnNegativeEffectEvent();
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _onNegativeEffect = false;
        }
    }
    
    
    protected virtual void Die(){
        Debug.Log("Lol you die");
    }
}
