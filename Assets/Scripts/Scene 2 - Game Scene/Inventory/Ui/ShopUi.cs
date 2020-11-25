using UnityEngine;

public class ShopUi : MonoBehaviour
{
    public ShopItemInspectPanel itemInspectPanel;
    public GameObject storageWindow;
    public Transform itemsParent;

    ShopItemSlot[] inventorySlots;

    Item selectedItem;

    private Shop shop = new Shop();

    private bool active;

    public static ShopUi instance;

    private void Awake()
    {
        instance = this;
        inventorySlots = itemsParent.GetComponentsInChildren<ShopItemSlot>();
    }

    private void Start()
    {
        SetShop(new Shop());
        active = storageWindow.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            storageWindow.SetActive(!storageWindow.activeSelf);
            active = storageWindow.activeSelf;
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

    public void BuyItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        shop.TryBuyItem(selectedItem);
        SelectItem(null);
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetShop(Shop newStorage)
    {
        try
        {
            shop.OnStorageUpdate -= UpdateUi;
        }
        catch { }

        newStorage.OnStorageUpdate += UpdateUi;
        shop = newStorage;

        UpdateUi();
    }

    public Shop GetShop()
    {
        return shop;
    }

    private void UpdateUi()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < shop.items.Count)
            {
                inventorySlots[i].SetItem(shop.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
