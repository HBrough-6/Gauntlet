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
    [SerializeField] private int currentNumItemsInInventory = 0;
    [SerializeField] private int numKeys = 0;
    [SerializeField] private int numPotions = 0;

    private ItemStats[] upgradePotions = new ItemStats[6];
    private int numUpgradePotions = 0;
   

    public bool AddKey(int keys)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory += keys;
            numKeys += keys;
            return true;
        }
        return false;
    }
    
    public bool AddPotion(int numPots)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            currentNumItemsInInventory += numPots;
            numPotions += numPots;
            return true;
        }
        return false;
    }

    public bool AddUpgradePotion(ItemStats potion)
    {
        if (currentNumItemsInInventory < maxItemsInInventory)
        {
            upgradePotions[numUpgradePotions] = potion;
            currentNumItemsInInventory++;
            numUpgradePotions++;
            CheckPotionStats();
        }
        return false;
    }


    public bool UseItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.Potion:
                if (numPotions > 0)
                {
                    numPotions--;
                    return true;
                }
                return false;
            case ItemType.Key:
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

    public void CheckPotionStats()
    {
        bool[] stats = { false, false, false, false, false, false, false, false, false, false };
        for (int i = 0; i < upgradePotions.Length && upgradePotions[i] != null; i++)
        {
            if (upgradePotions[i].ArmorStrength)
            {
                stats[0] = true;
            }
            if (upgradePotions[i].ShotStrength)
            {
                stats[1] = true;
            }
            if (upgradePotions[i].ShotTravelSpeed)
            {
                stats[2] = true;
            }
            if (upgradePotions[i].MagicVMonster)
            {
                stats[3] = true;
            }
            if (upgradePotions[i].MagicVGenerator)
            {
                stats[4] = true;
            }
            if (upgradePotions[i].PotionShotVMonster)
            {
                stats[5] = true;
            }
            if (upgradePotions[i].PotionShotVGenerator)
            {
                stats[6] = true;
            }
            if (upgradePotions[i].MeleeVMonsters)
            {
                stats[7] = true;
            }
            if (upgradePotions[i].MeleeVGenerators)
            {
                stats[8] = true;
            }
            if (upgradePotions[i].RunningSpeed)
            {
                stats[9] = true;
            }
        }

        transform.GetComponent<PlayerData>().AssignUpgrades(stats);
    }
}
