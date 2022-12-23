using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTest : EnemyAttack
{

    protected override void Attack()
    {
        Debug.Log("Attack");
        StartCoroutine(AttackCD());
    }
}
