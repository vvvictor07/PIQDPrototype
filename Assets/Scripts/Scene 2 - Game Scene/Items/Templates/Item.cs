﻿using UnityEngine;

public enum ItemType
{
    Food = 1,
    Weapon = 2,
    Apparel = 3,
    Crafting = 4,
    Ingredients = 5,
    Potions = 6,
    Scrolls = 7,
    Quest = 8,
    Money = 9,
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
    public GameObject onGroundPrefab;

    public virtual void Use() 
    {
        Player.instance.inventory.RemoveItem(this);
    }

    public void Drop()
    {
        var container = GameObject.FindGameObjectWithTag("ObjectsInMapContainer");

        var itemOnGroundObject = Instantiate(onGroundPrefab, Player.instance.transform.position, Quaternion.identity, container.transform);

        var itemOnGround = itemOnGroundObject.GetComponent<ItemOnGround>();

        if (itemOnGround != null)
        {
            itemOnGround.item = this;
        }

        Player.instance.inventory.RemoveItemStack(this);
    }

    public abstract string GetAttributes();

    public virtual int GetTotalCost()
    {
        return cost * currentAmount;
    }

    public bool Stackable()
    {
        return maxStack > 1;
    }
}
