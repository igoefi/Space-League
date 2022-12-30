using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;


public class EventDataInventory : EventArgs{
    public Dictionary<ResourcesType, int> Resources = new Dictionary<ResourcesType, int>();
}

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public EventHandler<EventDataInventory> UpdateUIDataEvent;
    public EventDataInventory Data = new EventDataInventory();
    

    [SerializeField]private Resources _resources;
    [SerializeField]private List<ItemList> items = new List<ItemList>();
    public List<ItemList> Items{
        get => items;
    }


    [SerializeField]private Player _playerRef;
    private void Awake() {
        Instance = this;
        _resources.StorageInit();
    }
    private void Start() {
        StartCoroutine(CallItemUpdate());
        Enemy.OnDieEvent += _resources.AddResource;  

        UpdateDataArg();
    }
    private void Update() {
        UpdateUIDataEvent.Invoke(this, Data);

        if(Input.GetKeyDown(KeyCode.R)){
            _resources.DResources(ResourcesType.Iron, 23);
            _resources.DResources(ResourcesType.Wood, 3);
            _resources.DResources(ResourcesType.Ammo, 5);
            UpdateDataArg();
        }
    }   

    //обновление аргумента для ивента
    private void UpdateDataArg(){
        foreach(var res in _resources.Storage){
            Data.Resources[res.Key] = res.Value;
        }
    }

    public void DropItem(int itemIndex){
        if(items[itemIndex].Stacks > 1){
            items[itemIndex].Stacks--;
            items[itemIndex].Item.OnDrop(_playerRef);
        }
        else{
            items[itemIndex].Item.OnDrop(_playerRef);
            items.Remove(items[itemIndex]);
        }
    }
    #region CallItems func
        
    private IEnumerator CallItemUpdate(){
        foreach(ItemList item in items){
            item.Item.Update(_playerRef, item.Stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    //for bullet hit
    public void CallItemOnHit(Enemy enemy){
        foreach(ItemList item in items){
            item.Item.OnHit(enemy, item.Stacks);
        }
    }

    public void CallItemOnPickup(){
        foreach(ItemList item in items){
            if(!item.IsUsed){
                if(item.ForPlayer){
                    item.Item.OnPickup(_playerRef, 1);
                    item.IsUsed = true;
                }
                else{
                    // For weapon
                    // item.Item.OnPickup(_weaponRef, 1);
                    // item.IsUsed = true;
                }
            }
        }
    }
    #endregion
}
