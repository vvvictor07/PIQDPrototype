using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Text textElement;

    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;

        var text = $"{item.name} ({item.currentAmount})";

        textElement.text = text;
    }

    public Item GetItem()
    {
        return item;
    }

    public void ClearSlot()
    {
        item = null;
        textElement.text = string.Empty;
    }
}
