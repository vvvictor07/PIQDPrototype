using System.Linq;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private Weapon weapon;

    public static PlayerWeapons instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ToDo: for debug
        var initialWeapon = (Weapon)Player.instance.inventory.items.First(x => x.type == ItemType.Weapon);
        initialWeapon.Equip();
    }

    public void EquipWeapon(Weapon weaponToEquip)
    {
        weapon = weaponToEquip;
        meshFilter.mesh = weapon.equipedMesh;
        meshRenderer.materials = new[] { weapon.equipedMaterial };
    }

    public void UnequipWeapon(Weapon weaponToUnequip)
    {
        if (weaponToUnequip?.id != weapon?.id)
        {
            return;
        }

        weapon = null;
        meshFilter.mesh = null;
        meshRenderer.materials = new Material[0];
    }
}