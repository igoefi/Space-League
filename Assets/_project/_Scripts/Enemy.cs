using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventDataBoss : EventArgs{
    public float HP;
    public string Name;
    public bool IsAlive;
}

public class Enemy : Character
{
    [SerializeField]private string _name;
    public delegate void OnDie(ResourcesType type,int dropCoin);
    public static OnDie OnDieEvent;

    public static EventHandler<EventDataBoss> BossUpdateUIEvent;
    public EventDataBoss Data = new EventDataBoss();
    
    [SerializeField]private bool _isBoss;
    private void Update() {
        if(_isBoss){
            UpdateDataArg();
            BossUpdateUIEvent.Invoke(this, Data);
        }

        if(_hp <= 0){
            Die();
        }
        if(_armor != null){
            _armor.ArmorRecovery();
        }
    }

    private void OnDestroy() {
        Data.IsAlive = false;   
        BossUpdateUIEvent.Invoke(this, Data);
    }
    private void UpdateDataArg(){
        Data.HP = _hp / (_maxHp + _bonusMaxHp);
        Data.Name = _name;
        Data.IsAlive = true;
    }
    
    protected override void Die(){
        OnDieEvent(ResourcesType.Coin, 5);
        Destroy(gameObject);
    }
}
