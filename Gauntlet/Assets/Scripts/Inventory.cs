using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

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

    private ItemStats[] upgradePotions = new ItemStats[6];

    [SerializeField] private int health = 2000;

    public bool AddKey(int numKeys)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory++;
            numKeys++;
            return true;
        }
        return false;
    }
    
    public bool AddPotion(int numPots)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory++;
            numPotions++;
            return true;
        }
        return false;
    }

    public bool AddUpgradePotion(ItemStats potion)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
                upgradePotions[upgradePotions.Length] = potion;
                currentNumItemsInInventory++;
        }
        return false;
    }

    public void IncreaseHealth(int HealthAmount)
    {
        health += HealthAmount;
    }

    public void TakeDamage(int DamageAmount)
    {
        health -= DamageAmount;
    }



    public bool UseItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Key:
                if (numPotions > 0)
                {
                    numPotions--;
                    return true;
                }
                return false;
            case ItemType.Potion:
                if (numKeys > 0)
                {
                    numKeys--;
                    return true;
                }
                return false;
            default:
                return false;
        }
    }
}
