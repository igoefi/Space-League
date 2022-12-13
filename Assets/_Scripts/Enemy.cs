using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private void OnMouseDown() {
        TakeDamage(25, DamageType.Physical);
    }
}
