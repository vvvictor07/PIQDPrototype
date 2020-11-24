using UnityEngine;
using UnityEngine.UI;

public class ConsumablesSlot : MonoBehaviour
{
    public Image icon;
    public Text stackCounter;

    Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        if (item.Stackable())
        {
            stackCounter.text = item.currentAmount.ToString();
            stackCounter.gameObject.SetActive(true);
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        stackCounter.gameObject.SetActive(false);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
