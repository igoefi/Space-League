using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList
{
    public Item Item;
    public string Name;
    public bool IsUsed;
    public bool ForPlayer;
    public int Stacks;
    public Sprite ItemSprite;

    public ItemList(Item newItem, string newName, int newStacks, bool forPlayer){
        Item = newItem;
        Name = newName;
        Stacks = newStacks;
        ForPlayer = forPlayer;
        IsUsed = false;

    }
}
