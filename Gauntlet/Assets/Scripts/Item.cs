using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Tooltip("Item name")]
    [SerializeField] private string ItemName;

    [Tooltip("What the Item will do")]
    [SerializeField] public ItemStats itemStats;

    private bool activated = false;

    private void Awake()
    {
        ItemName = itemStats.ItemName;
    }

    public void Activate(Inventory inventory)
    {
        switch (itemStats.Function)
        {
            case ItemFunction.OtherItem:

                int keyAmount = itemStats.AddsKey;
                int potionAmount = itemStats.AddsPotion;

                inventory.AddKey(keyAmount);
                inventory.AddPotion(potionAmount);
                inventory.transform.GetComponent<PlayerData>().IncreaseHealth(itemStats.HealthIncrease);
                break;
            case ItemFunction.UpgradePotion:
                inventory.AddUpgradePotion(itemStats);
                break;
            default:
                break;
        }
    }

    public void PlayerShot(PlayerData player)
    {
        if (itemStats.AddsPotion > 0)
            player.PotionShot();
    }

    public void EnemyShot()
    {
        if (itemStats.AddsPotion > 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            Activate(other.GetComponent<Inventory>());
            Destroy(gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
