using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemFunction
{
    OtherItem,
    UpgradePotion
}

[CreateAssetMenu(fileName = "NewStatConfig", menuName = "Stat/ItemStats", order = 1)]
public class ItemStats : ScriptableObject
{
    [Tooltip("The name of the Item")]
    [SerializeField] private string itemName;

    [Tooltip("the function of the item")]
    [SerializeField] private ItemFunction function;

    [Tooltip("Determines if the object adds is breakable")]
    [SerializeField] private bool breakable;

    [Tooltip("Increases percent of damage reduced")]
    [SerializeField] private bool armorStrength;


    [Tooltip("Increases The strength of the ranged attack.")]
    [SerializeField] private bool shotStrength;


    [Tooltip("Increases How fast the shot travels")]
    [SerializeField] private bool shotTravelSpeed;


    [Tooltip("Increases How much damage the potions do vs monsters")]
    [SerializeField] private bool magicVMonster;


    [Tooltip("Increases How much damage the potions do vs generators")]
    [SerializeField] private bool magicVGenerator;


    [Tooltip("Increases How much damage the potions do vs monsters when a potion is shot")]
    [SerializeField] private bool potionShotVMonster;


    [Tooltip("Increases How much damage the potions do vs generators when a potion is shot")]
    [SerializeField] private bool potionShotVGenerator;


    [Tooltip("Increases How much damage the melee does to enemies")]
    [SerializeField] private bool meleeVMonsters;


    [Tooltip("vthe rate at which the melee attack hits generators")]
    [SerializeField] private bool meleeVGenerators;

    [Tooltip("Increases how fast the character moves")]
    [SerializeField] private bool runningSpeed;

    // other item stats
    [Range(0, 500)]
    [Tooltip("Amount Health Increase")]
    [SerializeField] private int healthIncrease;

    [Tooltip("Determines if the object adds a key")]
    [SerializeField] private int addsKey;

    [Tooltip("Determines if the object adds a potion")]
    [SerializeField] private int addsPotion;

    

    public string ItemName
    {
        get { return itemName; } 
    }
    
    public ItemFunction Function
    {
        get { return function; }
    }

    public bool Breakable
    {
        get { return breakable; }
    }

    public bool ArmorStrength
    {
        get { return armorStrength; }
    }

    public bool ShotStrength
    {
        get { return armorStrength; }
    }

    public bool ShotTravelSpeed
    {
        get { return shotTravelSpeed; }
    }

    public bool MagicVMonster
    {
        get { return magicVMonster; }
    }

    public bool MagicVGenerator
    {
        get { return magicVGenerator; }
    }

    public bool PotionShotVMonster
    {
        get { return potionShotVMonster; }
    }

    public bool PotionShotVGenerator
    {
        get { return potionShotVGenerator; }
    }

    public bool MeleeVMonsters
    {
        get { return meleeVMonsters; }
    }

    public bool MeleeVGenerators
    {
        get { return meleeVGenerators; }
    }

    public bool RunningSpeed
    {
        get { return runningSpeed; }
    }

    // other item stats

    public int HealthIncrease
    {
        get { return healthIncrease; }
    }

    public int AddsKey
    {
        get { return addsKey; }
    }

    public int AddsPotion
    {
        get { return addsPotion; }
    }
}
