using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInspectPanel : MonoBehaviour
{
    public Image icon;
    public Text itemNameElement;
    public Text itemDescriptionElement;
    public Text goldElement;

    private Item selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(Item newItem)
    {
        selectedItem = newItem;

        icon.sprite = selectedItem?.icon;
        itemNameElement.text = selectedItem?.name;
        itemDescriptionElement.text = selectedItem?.description;
    }

    public void UseItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Use();

            if (selectedItem.currentAmount <= 0)
            {
                SetItem(null);
            }
        }
    }

    public void DropItem()
    {
        if (selectedItem != null)
        {
            selectedItem.Drop();
            SetItem(null);
        }
    }
}
