using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemFunction
{
    healthPickup,
    potion,
    ungradePotion,
    key
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Item : MonoBehaviour
{
    [Tooltip("Item name")]
    private string ItemName;

    [Tooltip("How much health the item will restore")]
    private int healthIncreaseAmount;

    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
