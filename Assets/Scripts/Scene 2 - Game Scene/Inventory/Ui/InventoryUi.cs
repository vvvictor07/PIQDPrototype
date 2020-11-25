using System.Linq;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public InventoryItemInspectPanel itemInspectPanel;
    public GameObject inventoryWindow;
    public Transform itemsParent;

    Storage inventory;
    ItemSlot[] inventorySlots;

    private ItemType? filter = null;

    private Item selectedItem;

    public static InventoryUi instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = Player.instance.inventory;

        inventory.OnStorageUpdate += UpdateUi;

        inventorySlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        UpdateUi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryWindow.SetActive(!inventoryWindow.activeSelf);
        }
    }

    public void SelectItem(int index)
    {
        var slot = inventorySlots[index];
        SelectItem(slot.GetItem());
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
        itemInspectPanel.SetItem(selectedItem);
    }

    public void UpdateUi()
    {
        var filteredItems = inventory.items
            .Where(item => filter == null || item.type == filter)
            .ToArray();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < filteredItems.Length)
            {
                inventorySlots[i].SetItem(filteredItems[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }

        itemInspectPanel.SetGold(Player.instance.gold);
    }

    public void UpdateFilters(int itemTypeIndex)
    {
        if (itemTypeIndex == 0)
        {
            filter = null;
        }
        else
        {
            filter = (ItemType)itemTypeIndex;
        }

        UpdateUi();
    }

    public void MoveItemToActiveStorage()
    {
        if (selectedItem == null)
        {
            return;
        }
        
        if (ShopUi.instance.IsActive())
        {
            ShopUi.instance.GetShop().TrySellItem(selectedItem);
        }
        else
        {
            inventory.TryTransferItem(selectedItem, StorageUi.instance.GetStorage());
        }

        SelectItem(null);
    }
}
