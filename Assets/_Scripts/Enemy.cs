using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public delegate void OnDie(ResourcesType type,int dropCoin);
    public static OnDie OnDieEvent;
    
    private void Update() {
        if(_hp <= 0){
            Die();
        }
        ArmorRecovery();
    }
    
    protected override void Die(){
        OnDieEvent(ResourcesType.Coin, 5);
        Destroy(gameObject);
    }
}
