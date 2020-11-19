using System.Linq;
using UnityEngine;

public class InventoryUiController : MonoBehaviour
{
    public GameObject inventoryWindow;
    public Transform itemsParent;

    Inventory inventory;
    InventorySlot[] inventorySlots;

    private ItemType? filter = null;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        inventory.OnInventoryUpdate += UpdateUi;

        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();

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

    void UpdateUi()
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
}
