using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class shopItems {
    public string itemName;
    public int itemCost;
    public bool purchased;
    public bool unlocked;
    
   
    public shopItems(string n, int c, bool p, bool u)
    {
        itemName = n;
        itemCost = c;
        purchased = p;
        unlocked = u;
    }
}
