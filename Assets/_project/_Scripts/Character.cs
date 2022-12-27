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
    
    [SerializeField]protected Armor _armor;

    private bool _onNegativeEffect = false;
    public delegate void OnNegativeEffect();
    public OnNegativeEffect OnNegativeEffectEvent;

    // private float _timerStamina;
    

    protected void Start() {
        _hp = _maxHp + _bonusMaxHp;

        if(_armor != null){
            _armor.Init();
        }
    }

    //метод для нанисения урона принимает количество урона и тип урона 
    public virtual void TakeDamage(float damage, DamageType type){
        if(_armor != null && !_armor.IsDestroyed){
            _armor.DamageArmor(damage, type);
        }
        else{
            _hp -= damage;
        }
    }

    //метод для наличия переодического урона
    public IEnumerator TakeDamageOverTime(float damage, DamageType type){
        if(!_onNegativeEffect){
            _onNegativeEffect = true;
            float timer = 0;
            while(timer < 3){
                TakeDamage(damage * Time.deltaTime, type);
                OnNegativeEffectEvent();
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _onNegativeEffect = false;
        }
    }

    //метод смерти который можно переписать под каждого кто наследует класс
    protected virtual void Die(){
        Debug.Log("Lol you die");
    }
}
