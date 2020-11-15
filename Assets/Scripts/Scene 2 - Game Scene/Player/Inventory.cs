using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public bool showInventory = false;
    private Vector2 scr;
    private string sortType = "";

    private void Display()
    {
        if(sortType == "")
        {
            if(inventory.Count <= 34)
            {
                for(int i = 0; i < inventory.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inventory[i].Name))
                    {
                        Debug.Log(inventory[i].Name);
                    }
                }
            }

        }
    }
    private void OnGUI()
    {
        if (showInventory)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            string[] itemTypes = Enum.GetNames(typeof(ItemType));
            int CountofItemTypes = itemTypes.Length;

            for(int i = 0; 1 < CountofItemTypes; i++)
            {
                if (GUI.Button(new Rect(4 * scr.x + i * scr.x, 0, scr.x, 0.25f * scr.y), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
            Display();
        }
    }

}
