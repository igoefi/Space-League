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
    private IEnumerator CallItemUpdate(){
        foreach(ItemList item in items){
            item.Item.Update(_playerRef, item.Stacks);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    public void CallItemOnHit(Enemy enemy){
        foreach(ItemList item in items){
            item.Item.OnHit(enemy, item.Stacks);
        }
    }
}
