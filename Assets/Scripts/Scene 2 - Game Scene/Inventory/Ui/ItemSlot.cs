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

        var cost = ShopUi.instance.GetShop().GetSellCostOfItem(item);

        var text = $"[{cost}] {item.name} ({item.currentAmount})";

        if (item is Equipable && (item as Equipable).IsEquiped())
        {
            textElement.color = Color.green;
        }
        else if (item is Consumable && (item as Consumable).IsReadyToUse() == false)
        {
            textElement.color = Color.red;
        }
        else
        {
            textElement.color = Color.black;
        }

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
