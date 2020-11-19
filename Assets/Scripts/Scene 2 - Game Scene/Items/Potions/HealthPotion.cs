using System;
using UnityEngine;

namespace Assets.Scripts.Scene_2___Game_Scene.Items.Templates
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potions/Health Potion", order = 51)]
    public class HealthPotion : Item
    {
        public int healAmount = 25;

        public override string GetDescription()
        {
            return description;
        }

        public override void Use()
        {
            base.Use();
            Player.instance.Heal(healAmount);
        }
    }
}
