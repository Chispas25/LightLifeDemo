using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public int itemID;

    // Constructor
    public Item(string name, Sprite icon, int id)
    {
        itemName = name;
        itemIcon = icon;
        itemID = id;
    }
}

}
