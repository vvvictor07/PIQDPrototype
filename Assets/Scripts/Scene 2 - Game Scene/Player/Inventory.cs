using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InventorySortingType{
    None = 0,
}

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public bool showInventory = true;
    private Vector2 scr;
    private InventorySortingType sortType = InventorySortingType.None;

    public delegate void UpdateInventory();
    public event UpdateInventory OnInventoryUpdate;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log(items.Count);
    }
}
