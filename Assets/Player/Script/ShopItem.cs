using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public Item item;
    public int currentStock; // -1 for infinite
    public int price; // Can be different from base price
}