using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    public Text textElement;

    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;

        var cost = ShopUi.instance.GetShop().GetBuyCostOfItem(item);

        var text = $"[{cost}x1] {item.name} ({item.currentAmount})";

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
