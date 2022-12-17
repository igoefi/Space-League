using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public delegate void OnDie(int dropCoin);
    public static OnDie OnDieEvent;
    
    



    protected override void Die(){
        OnDieEvent(5);
        Destroy(gameObject);
    }
}
