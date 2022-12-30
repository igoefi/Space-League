using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resources
{
    public int ResourcesStorageSize;

    private int _maxCountResources = 250;
    public int MaxCountResources { get => _maxCountResources; }
    private int _maxGrenade = 5;
    public int MaxGrenade { get => _maxGrenade; }
    private int _grenade;
    public int Grenade { get => _grenade; set { _grenade = value > _maxGrenade ? _maxGrenade : value; } }

    private Dictionary<ResourcesType, int>  _storage = new Dictionary<ResourcesType, int>();
    public Dictionary<ResourcesType, int> Storage { get => _storage; }
    public void StorageInit(){
        for(int i = 0; i < ResourcesStorageSize; i++){
            _storage[(ResourcesType)i] = 45;
        }
        Grenade = 5;
    }

    public void AddResource(ResourcesType type, int amount){
        _storage[type] += amount;
    }
    public int GetCountResource(ResourcesType type){
        return _storage[type];
    }
    public void DResources(ResourcesType type, int amount){
        _storage[type] -= amount;
    }
    public void SaveResourcesStorage(){
        for(int i = 0; i < _storage.Count; i++){
            PlayerPrefs.SetInt(((ResourcesType)i).ToString(), _storage[(ResourcesType)i]);
        }
    }
}
