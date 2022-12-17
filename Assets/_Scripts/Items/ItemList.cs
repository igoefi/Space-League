using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList
{
    public Item Item;
    public string Name;
    public bool IsUsed = false;
    public int Stacks;

    public ItemList(Item newItem, string newName, int newStacks){
        Item = newItem;
        Name = newName;
        Stacks = newStacks;
    }
}
