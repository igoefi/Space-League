using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    public string ItemName;
    public bool ForPlayer;
    public Item Item;
    //enum 
    public Items ItemDrop;

    private void Start() {
        Item = AssignItem(ItemDrop);
        Item.Name = ItemName;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            AddItem();
            Inventory.Instance.CallItemOnPickup();
            Destroy(gameObject);
        }
    }

    private void AddItem(){
        foreach(ItemList item in Inventory.Instance.Items){
            if(Item.Name == item.Name){
                item.Stacks++;
                item.IsUsed = false;
                return;
            }
        }
        Inventory.Instance.Items.Add(new ItemList(Item, Item.Name, 1, ForPlayer));
    }
    

    private Item AssignItem(Items item){
        switch(item){
            case Items.HealighItem: return new HealighItem();
            case Items.FireDamageItem: return new FireDamageItem();
            case Items.IncreaseMaxHp: return new IncreaseMaxHp();
            case Items.Stamina: return new Stamina();
            case Items.SpeedItem: return new SpeedItem();

            default: return new HealighItem();
        }
    }
}


