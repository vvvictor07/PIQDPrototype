using UnityEngine;

public class StorageUi : MonoBehaviour
{
    public StorageItemInspectPanel itemInspectPanel;
    public GameObject storageWindow;
    public Transform itemsParent;

    ItemSlot[] inventorySlots;

    Item selectedItem;

    private Storage storage;

    public static StorageUi instance;

    private void Awake()
    {
        instance = this;
        inventorySlots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    private void Start()
    {
        SetStorage(new Storage());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            storageWindow.SetActive(!storageWindow.activeSelf);
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

    public void SetStorage(Storage newStorage)
    {
        try
        {
            storage.OnStorageUpdate -= UpdateUi;
        }
        catch {}

        newStorage.OnStorageUpdate += UpdateUi;
        storage = newStorage;

        UpdateUi();
    }

    public Storage GetStorage()
    {
        return storage;
    }

    public void TakeItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        storage.TryTransferItem(selectedItem, Player.instance.inventory);
        SelectItem(null);
    }

    private void UpdateUi()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < storage.items.Count)
            {
                inventorySlots[i].SetItem(storage.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
