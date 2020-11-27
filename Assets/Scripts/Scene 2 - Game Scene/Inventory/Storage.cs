using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage
{
    public List<Item> items = new List<Item>();
    public int slotsCount = 10;

    public delegate void UpdateStorage();
    public event UpdateStorage OnStorageUpdate;

    public virtual bool TryAddItem(Item item)
    {
        if (item.Stackable() == false)
        {
            return TryAddItemToNewSlot(item);
        }

        var sameItems = items.Where(x => x.id == item.id).ToArray();

        if (sameItems.Length == 0)
        {
            return TryAddItemToNewSlot(item);
        }

        var res = TryAddItemToExistingSlot(sameItems, item);
        if (res)
        {
            return true;
        }

        return TryAddItemToNewSlot(item);
    }

    private bool TryAddItemToExistingSlot(IEnumerable<Item> sameItems, Item item)
    {
        var stackableItems = sameItems
                .Where(x => x.currentAmount < x.maxStack)
                .OrderByDescending(x => x.currentAmount)
                .ToArray();

        if (stackableItems.Length == 0)
        {
            return false;
        }

        foreach (var itemToAddStack in stackableItems)
        {
            var sum = itemToAddStack.currentAmount + item.currentAmount;

            itemToAddStack.currentAmount = Mathf.Clamp(sum, 0, itemToAddStack.maxStack);

            item.currentAmount = sum - itemToAddStack.maxStack;
            if (item.currentAmount <= 0)
            {
                break;
            }
        }

        if (item.currentAmount > 0)
        {
            return false;
        }

        OnStorageUpdate();
        return true;
    }

    private bool TryAddItemToNewSlot(Item item)
    {
        if (items.Count >= slotsCount)
        {
            OnStorageUpdate();
            return false;
        }

        items.Add(item);

        OnStorageUpdate();
        return true;
    }

    public virtual void RemoveItem(Item item)
    {
        item.currentAmount--;
        if (item.currentAmount <= 0)
        {
            items.Remove(item);
        }

        if (item is Equipable)
        {
            var itemAsEquipable = item as Equipable;
            itemAsEquipable.Unequip();
        }

        OnStorageUpdate();
    }

    public virtual void RemoveItem(Item item, int amountToRemove)
    {
        var sameItems = items
            .Where(x => x.id == item.id)
            .OrderBy(x => x.currentAmount)
            .ToArray();

        foreach (var sameItem in sameItems)
        {
            sameItem.currentAmount -= amountToRemove;

            if (sameItem.currentAmount > 0)
            {
                break;
            }

            if (sameItem.currentAmount == 0)
            {
                RemoveItemStack(sameItem);
                break;
            }

            amountToRemove = Mathf.Abs(sameItem.currentAmount);
            RemoveItemStack(sameItem);
        }

        OnStorageUpdate();
    }

    public virtual void RemoveItemStack(Item item)
    {
        items.Remove(item);

        if (item is Equipable)
        {
            var itemAsEquipable = item as Equipable;
            itemAsEquipable.Unequip();
        }

        OnStorageUpdate();
    }

    public virtual bool TryTransferItem(Item item, Storage anotherStorage)
    {
        var itemToTransfer = Object.Instantiate(item);
        itemToTransfer.currentAmount = 1;

        var itemTransferred = anotherStorage.TryAddItem(itemToTransfer);

        if (itemTransferred)
        {
            if (itemToTransfer is Equipable)
            {
                var itemAsEquipable = itemToTransfer as Equipable;
                itemAsEquipable.Unequip();
            }

            RemoveItem(item);

            OnStorageUpdate();
            anotherStorage.OnStorageUpdate();
        }

        return itemTransferred;
    }

    public void InvokeOnStorageUpdate()
    {
        OnStorageUpdate?.Invoke();
    }
}

