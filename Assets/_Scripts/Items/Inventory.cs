using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private int _coins;
    



    private List<ItemList> items = new List<ItemList>();
    public List<ItemList> Items{
        get => items;
    }
    [SerializeField]private Player _playerRef;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        StartCoroutine(CallItemUpdate());
        Enemy.OnDieEvent += AddCoin;
    }
    public void AddCoin(int amount){
        _coins += amount;
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
                item.Item.OnPickup(_playerRef, 1);
                item.IsUsed = true;
            }
        }
    }
}
