using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatConfig", menuName = "Stat/UpgradePotion", order = 2)]
public class UpgradePotion : ScriptableObject
{
    [SerializeField] private int potionNumber;

    [Tooltip("Increases Armor")]
    [SerializeField] private bool extraArmor;
    // affects armor strength

    [Tooltip("Increases Magic Power")]
    [SerializeField] private bool extraMagic;
    // affects MagicVMonster, MagicVGenerator, PotionShotVMonster, PotionShotVGenerator

    [Tooltip("Increases Ranged Attack")]
    [SerializeField] private bool extraShotPower;
    // affects ShotStrength

    [Tooltip("Increases Shot Speed")]
    [SerializeField] private bool extraShotSpeed;
    // affects ShotTravelSpeed

    [Tooltip("Increases player speed")]
    [SerializeField] private bool extraSpeed;
    // Increases RunningSpeed

    [Tooltip("increases Melee attack Power")]
    [SerializeField] private bool extraFightPower;
    // Affects MeleeVMonsters, MeleeVGenerators

    public int PotionNumber
    {
        get { return potionNumber; }
    }

    public bool ExtraArmor
    {
        get { return extraArmor; }
    }

    public bool ExtraMagic
    {
        get { return extraMagic; }
    }

    public bool ExtraShotPower
    {
        get { return extraShotPower; }
    }

    public bool ExtraShotSpeed
    {
        get { return extraShotSpeed; }
    }

    public bool ExtraSpeed
    {
        get { return extraSpeed; }
    }

    public bool ExtraFightPower
    {
        get { return extraFightPower; }
    }
}
