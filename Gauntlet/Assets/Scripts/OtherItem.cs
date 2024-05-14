using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatConfig", menuName = "Stat/OtherItem", order = 0)]
public class OtherItem : ScriptableObject
{
    [Tooltip("The name of the Item")]
    [SerializeField] private string itemName;

    [Range(0, 500)]
    [Tooltip("Amount Health Increase")]
    [SerializeField] private int healthIncrease;

    [Tooltip("Determines if the object adds a key")]
    [SerializeField] private bool addsKey;

    [Tooltip("Determines if the object adds a potion")]
    [SerializeField] private bool addsPotion;

    public string ItemName
    {
        get { return itemName; }
    }

    public int HealthIncrease
    {
        get { return healthIncrease; }
    }
    
    public bool AddsKey
    {
        get { return AddsKey; }
    }

    public bool AddsPotion
    {
        get { return addsPotion; }
    }
}
