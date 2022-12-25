using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Armor")]
public class Armor : ScriptableObject
{
    public string Name;
    public ArmorType Type;
    public float Durability;
    [SerializeField]private float _armor;
    public float CooldownRegen;
    public float SpeedRegen;

    public bool IsDestroyed{
        get => _armor <= 0;
    }
    public Resistance[] StartResistance = new Resistance[3];
    public Dictionary<DamageType, float> Resistance = new Dictionary<DamageType, float>();

    [SerializeField]private float _timerArmor;

    public void Init(){
        _armor = Durability;
        for(int i = 0; i < StartResistance.Length; i++){
            Resistance[StartResistance[i].type] = StartResistance[i].values;
        }

    }
    public void ArmorRecovery(){
        if(_armor < Durability && _timerArmor <= 0){
            _armor += SpeedRegen * Time.deltaTime;
        }
        else{
            _timerArmor -= Time.deltaTime;
        }
    }
    public void DamageArmor(float damage, DamageType type){
        _timerArmor = CooldownRegen;
        _armor -= damage / Resistance[type];
    }
    

}
