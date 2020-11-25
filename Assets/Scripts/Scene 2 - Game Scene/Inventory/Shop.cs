﻿public class Shop : Storage
{
    public float sellCostCoefficent = 0.5f;
    public float buyCostCoefficent = 1f;

    public int GetSellCostOfItem(Item item)
    {
        return (int)(item.GetTotalCost() * sellCostCoefficent);
    }

    public int GetBuyCostOfItem(Item item)
    {
        return (int)(item.GetTotalCost() * buyCostCoefficent);
    }

    public bool TryBuyItem(Item item)
    {
        if (Player.instance.gold < GetBuyCostOfItem(item))
        {
            return false;
        }

        var cost = (int)(item.GetTotalCost() * buyCostCoefficent);
        var itemTransferred = TryTransferItem(item, Player.instance.inventory);

        if (itemTransferred)
        {
            Player.instance.gold -= cost;
            InventoryUi.instance.UpdateUi();
            return true;
        }

        return false;
    }

    public bool TrySellItem(Item item)
    {
        var cost = (int)(item.GetTotalCost() * sellCostCoefficent);

        var itemTransferred = Player.instance.inventory.TryTransferItem(item, this);

        if (itemTransferred)
        {
            Player.instance.gold += cost;
            InventoryUi.instance.UpdateUi();
            return true;
        }

        return false;
    }
}
