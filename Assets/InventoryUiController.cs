using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiController : MonoBehaviour
{
    public GameObject inventoryWindow;
    public Transform itemsParent;

    Inventory inventory;
    InventorySlot[] inventorySlots;

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
        for (int i = 0; i< inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].SetItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
