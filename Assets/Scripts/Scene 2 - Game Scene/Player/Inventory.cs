using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public delegate void UpdateInventory();
    public event UpdateInventory OnInventoryUpdate;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    public void Add(Item item)
    {
        if (item.Stackable() == false)
        {
            AddItem(item);
            return;
        }

        var sameItems = items.Where(x => x.id == item.id).ToArray();

        if (sameItems.Length == 0)
        {
            AddItem(item);
            return;
        }

        var itemToAddStack = sameItems
            .Where(x => x.currentAmount < x.maxStack)
            .OrderByDescending(x => x.currentAmount)
            .FirstOrDefault();

        if (itemToAddStack != null)
        {
            itemToAddStack.currentAmount++;
            OnInventoryUpdate();
        }
        else
        {
            AddItem(item);
            return;
        }
    }

    private void AddItem(Item item)
    {
        item.currentAmount++;
        items.Add(item);
        OnInventoryUpdate();
    }

    public void Remove(Item item)
    {
        item.currentAmount--;
        if (item.currentAmount <= 0)
        {
            items.Remove(item);
        }

        OnInventoryUpdate();
    }
}
