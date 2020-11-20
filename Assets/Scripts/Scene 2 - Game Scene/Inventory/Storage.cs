using System.Collections.Generic;
using System.Linq;

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

        var itemToAddStack = sameItems
            .Where(x => x.currentAmount < x.maxStack)
            .OrderByDescending(x => x.currentAmount)
            .FirstOrDefault();

        if (itemToAddStack != null)
        {
            itemToAddStack.currentAmount++;
            OnStorageUpdate();
            return true;
        }
        
        return TryAddItemToNewSlot(item);
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

        OnStorageUpdate();
    }

    public virtual bool TryTransferItem(Item item, Storage anotherStorage)
    {
        var itemTransferred = anotherStorage.TryAddItem(item);

        if (itemTransferred)
        {
            items.Remove(item);
            OnStorageUpdate();
        }

        return itemTransferred;
    }
}

