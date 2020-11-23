using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public Item item;
    private bool playerInTouch;
    public GameObject worldObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTouch && Input.GetKeyDown(KeyCode.E))
        {
            Player.instance.inventory.TryAddItem(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.instance.CompareTag(other.tag))
        {
            playerInTouch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player.instance.CompareTag(other.tag))
        {
            playerInTouch = false;
        }
    }

    public void OnGUI()
    {

    }
}
