using UnityEngine;

public abstract class Weapon : Equipable
{
    public float damage;
    public Mesh equipedMesh;
    public Material equipedMaterial;

    public override void Equip()
    {
        base.Equip();
        PlayerWeapons.instance.EquipWeapon(this);
    }

    public override void Unequip()
    {
        base.Unequip();
        PlayerWeapons.instance.UnequipWeapon(this);
    }
}
