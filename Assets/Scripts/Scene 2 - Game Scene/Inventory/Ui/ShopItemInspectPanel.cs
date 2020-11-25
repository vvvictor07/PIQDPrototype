using UnityEngine;
using UnityEngine.UI;

public class ShopItemInspectPanel : MonoBehaviour
{
    public Image icon;
    public Text itemNameElement;
    public Text itemDescriptionElement;

    private Item selectedItem;
    private StorageUi storageUi;

    void Start()
    {
        storageUi = StorageUi.instance;
    }

    public void SetItem(Item newItem)
    {
        selectedItem = newItem;

        icon.sprite = selectedItem?.icon;
        itemNameElement.text = selectedItem?.name;
        itemDescriptionElement.text = selectedItem?.description;
    }

    public void BuyItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        storageUi.GetStorage().TryTransferItem(selectedItem, Player.instance.inventory);
    }
}
