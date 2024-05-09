using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum ItemType
{
    Key,
    Potion
}

public class Inventory : MonoBehaviour
{
    private int maxItemsInInventory = 5;
    private int currentNumItemsInInventory = 0;
    private int numKeys = 0;
    private int numPotions = 0;

    private UpgradePotion[] upgradePotions = new UpgradePotion[6];

    public bool AddKey()
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory++;
            numKeys++;
            return true;
        }
        return false;
    }
    
    public bool AddPotion()
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory++;
            numPotions++;
            return true;
        }
        return false;
    }

    public bool AddUpgradePotion(UpgradePotion potion)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            if (potion)
            {
                currentNumItemsInInventory++;
            }
        }
        return false;
    }

    public bool UseItem(ItemType type)
    {
        if (true)
        {

        }
    }

    public bool activatePotion()
    {
        if (numPotions > 0)
        {
            numPotions--;
            return true;
        }
        return false;
    }

    public bool UseKey()
    {
        if (numKeys > 0)
        {
            numKeys--;
            return true;
        }
        return false;
    }
}
