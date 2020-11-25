using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potions/Health Potion", order = 51)]
public class HealthPotion : Consumable
{
    public int healAmount = 25;

    public override string GetAttributes()
    {
        return description;
    }

    public override bool IsReadyToUse()
    {
        return !Player.instance.disableConsumablesUsage;
    }

    public override void Use()
    {
        if (IsReadyToUse() == false)
        {
            return;
        }

        Player.instance.Heal(healAmount);
        Player.instance.SetConsumablesOnCooldown();
        base.Use();
    }
}
