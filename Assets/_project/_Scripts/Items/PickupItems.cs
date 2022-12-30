using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupInfo : EventArgs{
    public ItemList Item;
}

public class PickupItems : MonoBehaviour
{
    public string ItemName;
    public bool ForPlayer;
    public Sprite ItemSprite;
    public Item Item;
    //enum 
    public Items ItemDrop;


    public PickupInfo Info = new PickupInfo();
    public EventHandler<PickupInfo> PickupEvent;

    private void Start() {
        Item = AssignItem(ItemDrop);
        Item.Name = ItemName;
        Info.Item = new ItemList(Item, Item.Name, 1, ForPlayer);
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
                item.ItemSprite = ItemSprite;
                return;
            }
        }
        //PickupEvent.Invoke(this, Info);
        Inventory.Instance.Items.Add(Info.Item);
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


