using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int debugItemIndex;
    private new void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.G)){
            Inventory.Instance.DropItem(debugItemIndex);

        }
    }
}


