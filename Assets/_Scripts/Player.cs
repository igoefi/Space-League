using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int _maxLvl = 4;
    private Dictionary<Stats, int> _treeUpdateStats = new Dictionary<Stats, int>();

    [SerializeField]protected float _stamina;
    protected float _staminaRegen = 5;
    protected float _maxStamina = 100;
    protected float _bonusMaxStamina;
    public float Stamina{
        get => _stamina;
    }

    private new  void Start() {
        base.Start();
        _stamina = _maxStamina;
        _treeUpdateStats[Stats.MaxHealth] = 0;
        _treeUpdateStats[Stats.MaxStamina] = 0;
        _treeUpdateStats[Stats.MaxAmmo] = 0;
    }
    private void Update() {
        if(_hp <= 0){
            Die();
        }
        StaminaRecovery();
        ArmorRecovery();
    }
    private void OnMouseDown() {
        StartCoroutine(TakePoisonDamage());
    }
    public virtual void DecreaseStamina(float amount){
        _stamina -= amount;
    }
    public virtual void StaminaRecovery(){
        if(_stamina < _maxStamina){
            _stamina += _staminaRegen * Time.deltaTime;
        }
    }
    public virtual void HpRecovery(float amount){
        _hp += amount;
        _hp = _hp > _maxHp + _bonusMaxHp ? _maxHp + _bonusMaxHp : _hp;
    }
    public void SetBonusMaxHp(float amount){
        _bonusMaxHp += amount;
        _hp = _maxHp + _bonusMaxHp;
    }
    public void SetBonusMaxStamina(float amount){
        _bonusMaxStamina += amount;
        _stamina = _maxStamina + _bonusMaxStamina;
    }
    public void SetBonusStaminaRegen(float amount){
        _staminaRegen += amount;
    }
    public void UpdateStats(int stats){
        if(_treeUpdateStats[(Stats)stats] + 1 > _maxLvl){
            return;
        }
        _treeUpdateStats[(Stats)stats]++;
        switch((Stats)stats){
            case Stats.MaxHealth : SetBonusMaxHp(15); break;
            case Stats.MaxStamina : SetBonusMaxStamina(15); break;
            case Stats.MaxAmmo : SetBonusMaxHp(30); break;
        }
    }
    
    
    
}


