using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Armor")]
public class Armor : ScriptableObject
{
    public string Name;
    public ArmorType Type;
    public float Durability;
    public float CooldownRegen;
    public float SpeedRegen;
    public Resistance[] StartResistance = new Resistance[3];
    public Dictionary<DamageType, float> Resistance = new Dictionary<DamageType, float>();

  

    public void Init(){
        for(int i = 0; i < StartResistance.Length; i++){
            Resistance[StartResistance[i].type] = StartResistance[i].values;
        }

    }
    

}
