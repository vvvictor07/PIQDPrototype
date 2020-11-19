﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Weapon,
    Apparel,
    Crafting,
    Ingredients,
    Potions,
    Scrolls,
    Quest,
    Money
}

[System.Serializable]
public abstract class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public ItemType type;

    public int maxStack = 1;
    public int currentAmount = 1;

    public int cost = 0;

    public Sprite icon;
    public GameObject mesh;

    public virtual void Use() 
    {
        Inventory.instance.Remove(this);
    }

    public abstract string GetDescription();

    public bool Stackable()
    {
        return maxStack > 1;
    }
}
