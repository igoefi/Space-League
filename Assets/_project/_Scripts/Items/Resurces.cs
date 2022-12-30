using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resources
{
    public int ResourcesStorageSize = 4;
    private Dictionary<ResourcesType, int>  _storage = new Dictionary<ResourcesType, int>();
    public Dictionary<ResourcesType, int> Storage { get => _storage; }
    public void StorageInit(){
        for(int i = 0; i < ResourcesStorageSize; i++){
            _storage[(ResourcesType)i] = 45;
        }
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
