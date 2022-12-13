using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{


    private void OnMouseDown() {
        TakeDamage(25, DamageType.Physical);
    }
}
