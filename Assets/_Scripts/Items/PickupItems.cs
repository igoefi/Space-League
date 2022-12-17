using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    public string ItemName;
    public Item Item;
    public Items ItemDrop;

    private void Start() {
        Item = AssignItem(ItemDrop);
        Item.Name = ItemName;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            AddItem();
            Destroy(gameObject);
        }
    }

    private void AddItem(){
        foreach(ItemList item in Inventory.Instance.Items){
            if(Item.Name == item.Name){
                item.Stacks++;
                return;
            }
        }
        Inventory.Instance.Items.Add(new ItemList(Item, Item.Name, 1));
    }

    private Item AssignItem(Items item){
        switch(item){
            case Items.HealighItem: return new HealighItem();
            case Items.FireDamageItem: return new FireDamageItem();

            default: return new HealighItem();
        }
    }
}


