using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventDataPlayer : EventArgs{
    public float HP;
    public float Stamina;
    public float Armor;
}

[RequireComponent(typeof(PlayerMovement))]
public class Player : Character
{
    private int _maxLvl = 4;
    private Dictionary<Stats, int> _treeUpdateStats = new Dictionary<Stats, int>();

    private EventDataPlayer Data = new EventDataPlayer();
    public EventHandler<EventDataPlayer> UpdateUIDataEvent;

    [SerializeField]protected float _stamina;
    [SerializeField]protected float _staminaRegen = 5;
    protected float _maxStamina = 100;
    protected float _bonusMaxStamina;
    public float Stamina{
        get => _stamina;
    }

    protected PlayerMovement _movement;
    public PlayerMovement MovementRef {
        get => _movement;
    }

    private void OnMouseDown() {
        TakeDamage(10, DamageType.Physical);
    }

    private new  void Start() {
        base.Start();
        InitUpdateTree();
        
        
        _stamina = _maxStamina;

        _movement = GetComponent<PlayerMovement>();
    }

    private void InitUpdateTree(){
        _treeUpdateStats[Stats.MaxHealth] = 0;
        _treeUpdateStats[Stats.MaxStamina] = 0;
        _treeUpdateStats[Stats.MaxAmmo] = 0;
    }

    private void Update() {
        UpdateDataEvent();
        UpdateUIDataEvent.Invoke(this, Data);

        if(_hp <= 0){
            Die();
        }
        StaminaRecovery();
        if(_armor != null)
        _armor.ArmorRecovery();
    }


    private void UpdateDataEvent(){
        Data.Armor = _armor == null ? 0 : _armor.Percent;
        Data.HP = _hp / (_maxHp + _bonusMaxHp);
        Data.Stamina = _stamina / (_maxStamina + _bonusMaxStamina);
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


