using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "Items/Weapon/Axe", order = 51)]
public class Axe : Weapon
{
    public override string GetAttributes()
    {
        return description;
    }
}
