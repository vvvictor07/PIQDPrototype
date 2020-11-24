using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potions/Health Potion", order = 51)]
public class HealthPotion : Item
{
    public int healAmount = 25;

    public override string GetAttributes()
    {
        return description;
    }

    public override void Use()
    {
        base.Use();
        Player.instance.Heal(healAmount);
    }
}
