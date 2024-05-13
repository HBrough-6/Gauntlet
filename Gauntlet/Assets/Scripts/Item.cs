using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("Item name")]
    [SerializeField] private string ItemName;

    [Tooltip("What the Item will do")]
    [SerializeField] private ItemStats itemStats;

    private void Awake()
    {
        ItemName = itemStats.ItemName;
    }

    public void Activate(Inventory inventory)
    {
        switch (itemStats.Function)
        {
            case ItemFunction.OtherItem:
                inventory.AddKey(itemStats.AddsKey);
                inventory.AddPotion(itemStats.AddsPotion);
                inventory.IncreaseHealth(itemStats.HealthIncrease);
                break;
            case ItemFunction.UpgradePotion:
                inventory.AddUpgradePotion(itemStats);
                break;
            default:
                break;
        }
    }
}
