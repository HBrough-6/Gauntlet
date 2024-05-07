using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatConfig", menuName = "Stat/Config", order = 2)]

public class StatConfig : ScriptableObject, IStats
{
    [Tooltip("The name of the character")]
    [SerializeField] private string characterName;

    [Range(0, 1)]
    [Tooltip("Percent of damage reduced")]
    [SerializeField] private float armorStrength;

    [Range(0, 1)]
    [Tooltip("Percent of damage reduced when upgraded")]
    [SerializeField] private float extraArmorStrength;

    [Range(0, 3)]
    [Tooltip("The strength of the ranged attack. Values with decimals will vary between 2 numbers. \n" +
        " eg: 1.5 will cause damage to be either 1 or 2")]
    [SerializeField] private float shotStrength;

    [Range(0, 3)]
    [Tooltip("The strength of the ranged attack when upgraded")]
    [SerializeField] private float extraShotStrength;

    [Range(0, 5)]
    [Tooltip("How fast the shot travels")]
    [SerializeField] private float shotTravelSpeed;
    [Range(0, 5)]
    [Tooltip("How fast the shot travels when upgraded")]
    [SerializeField] private float extraShotTravelSpeed;

    [Tooltip("Size of the projectile hitbox")]
    [SerializeField] private ProjectileHitboxSize shotCollisionBox;

    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs monsters")]
    [SerializeField] private int magicVMonster;
    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs monsters when upgraded")]
    [SerializeField] private int extraMagicVMonster;

    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs generators")]
    [SerializeField] private int magicVGenerator;
    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs generators when upgraded")]
    [SerializeField] private int extraMagicVGenerator;

    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs monsters when a potion is shot")]
    [SerializeField] private int potionShotVMonster;
    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs monsters when a potion is shot when upgraded")]
    [SerializeField] private int extraPotionShotVMonster;

    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs generators when a potion is shot")]
    [SerializeField] private int potionShotVGenerator;
    [Range(0, 3)]
    [Tooltip("How much damage the potions do vs generators when a potion is shot when upgraded")]
    [SerializeField] private int extraPotionShotVGenerator;

    [Range(0, 3)]
    [Tooltip("How much damage the melee does to enemies")]
    [SerializeField] private float meleeVMonsters;
    [Range(0, 3)]
    [Tooltip("How much damage the melee does to enemies when upgraded")]
    [SerializeField] private float extraMeleeVMonsters;

    [Tooltip("the rate at which the melee attack hits generators")]
    [SerializeField] private GeneratorMissPercent meleeVGenerators;
    [Tooltip("the rate at which the melee attack hits generators when upgraded")]
    [SerializeField] private GeneratorMissPercent extraMeleeVGenerators;

    [Range(0, 5)]
    [Tooltip("how fast the character moves")]
    [SerializeField] private int runningSpeed;
    [Range(0, 5)]
    [Tooltip("how fast the character moves when upgraded")]
    [SerializeField] private int extraRunningSpeed;

    public string CharacterName
    {
        get { return characterName; }
    }

    // base stats
    public float ArmorStrength
    {
        get { return armorStrength; }
    }

    public float ShotStrength
    {
        get { return armorStrength; }
    }

    public float ShotTravelSpeed
    {
        get { return shotTravelSpeed; }
    }

    public ProjectileHitboxSize ShotCollisionBox
    {
        get { return shotCollisionBox; }
    }

    public int MagicVMonster
    {
        get { return magicVMonster; }
    }

    public int MagicVGenerator
    {
        get { return magicVGenerator; }
    }

    public int PotionShotVMonster
    {
        get { return potionShotVMonster; }
    }

    public int PotionShotVGenerator
    {
        get { return potionShotVGenerator; }
    }

    public float MeleeVMonsters
    {
        get { return meleeVMonsters; }
    }

    public GeneratorMissPercent MeleeVGenerators
    {
        get { return meleeVGenerators; }
    }

    public int RunningSpeed
    {
        get { return runningSpeed; }
    }

    // upgraded Stats
    public float ExtraArmorStrength
    {
        get { return extraArmorStrength; }
    }

    public float ExtraShotStrength
    {
        get { return extraShotStrength; }
    }

    public float ExtraShotTravelSpeed
    {
        get { return extraShotTravelSpeed; }
    }

    public int ExtraMagicVMonster
    {
        get { return extraMagicVMonster; }
    }

    public int ExtraMagicVGenerator
    {
        get { return extraMagicVGenerator; }
    }

    public int ExtraPotionShotVMonster
    {
        get { return extraPotionShotVMonster; }
    }

    public int ExtraPotionShotVGenerator
    {
        get { return extraPotionShotVGenerator; }
    }

    public float ExtraMeleeVMonsters
    {
        get { return extraMeleeVMonsters; }
    }

    public GeneratorMissPercent ExtraMeleeVGenerators
    {
        get { return extraMeleeVGenerators; }
    }

    public int ExtraRunningSpeed
    {
        get { return extraRunningSpeed; }
    }
}
