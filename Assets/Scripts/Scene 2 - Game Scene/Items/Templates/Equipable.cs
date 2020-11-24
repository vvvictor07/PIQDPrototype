using UnityEngine;

public abstract class Equipable : Item
{
    private bool equiped;

    public abstract override string GetAttributes();

    public override void Use()
    {
        if (IsEquiped())
        {
            Unequip();
        }
        else
        {
            Equip();
        }
    }

    public bool IsEquiped()
    {
        return equiped;
    }

    public virtual void Equip() 
    {
        equiped = true;
        Player.instance.inventory.InvokeOnStorageUpdate();
    }

    public virtual void Unequip()
    {
        equiped = false;
        Player.instance.inventory.InvokeOnStorageUpdate();
    }
}
