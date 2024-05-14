using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private bool armorStrengthUpgraded = false;
    [SerializeField] private bool shotPowerUpgraded = true;
    [SerializeField] private bool shotSpeedUpgraded = false;
    [SerializeField] private bool magicVMonsterUpgraded = false;
    [SerializeField] private bool magicVGeneratorUpgraded = false;
    [SerializeField] private bool potionShotVMonsterUpgraded = false;
    [SerializeField] private bool potionShotVGeneratorUpgraded = false;
    [SerializeField] private bool meleeVMonstersUpgraded = false;
    [SerializeField] private bool meleeVGeneratorsUpgraded = false;
    [SerializeField] private bool runningSpeedUpgraded = false;

    [SerializeField] private GameObject[] projectileRefs = new GameObject[3];

    [SerializeField] private StatConfig stats;
    private Inventory inventory;
    private PlayerController controller;

    [SerializeField] public int health = 2000;

    private int score;
    private bool projectileOnScreen = false;

    

    private void Awake()
    {
        inventory = gameObject.AddComponent<Inventory>();
        controller = gameObject.GetComponent<PlayerController>();

        TempGameManager.Instance.AddPlayer(this);
    }

    public void IncreaseHealth(int HealthAmount)
    {
        health += HealthAmount;
    }

    public void TakeDamage(int DamageAmount)
    {
        Debug.Log("full damage" + DamageAmount);
        if (armorStrengthUpgraded)
        {
            health -=  (int)(DamageAmount * 1 - stats.ExtraArmorStrength);
            //Debug.Log("reduced damage" + (int)(DamageAmount * 1 - stats.ExtraArmorStrength));
        }
        
        else
        {
            health -= (int)(DamageAmount * 1 - stats.ArmorStrength);
            //Debug.Log("reduced damage" + (int)(DamageAmount * 1 - stats.ArmorStrength));
        }
    }

    // Player used a potion
    public void ActivatePotion()
    {
        int magicVMDamage = 0;
        int magicVGDamage = 0;
        if (inventory.UseItem(ItemType.Potion))
        {
            
            if (magicVGeneratorUpgraded)
            {
                // deal damage to all on screen Generators
                magicVGDamage = stats.ExtraMagicVGenerator; 
            }
            else
                magicVGDamage = stats.MagicVGenerator;

            if (magicVMonsterUpgraded)
            {
                // deal damage to all on screen monsters
                magicVGDamage = stats.ExtraMagicVMonster;
            }
            else
                magicVGDamage = stats.MagicVMonster;
        }

        // damages all enemies on screen
        CameraController.Instance.DamageAllOnScreen(magicVMDamage, magicVGDamage);
    }

    // Deals damage if the player shoots a potion
    public void PotionShot()
    {
        int magicVMDamage = 0;
        int magicVGDamage = 0;

            if (magicVGeneratorUpgraded)
            {
                // deal damage to all on screen Generators
                magicVGDamage = stats.ExtraPotionShotVGenerator;
            }
            else
                magicVGDamage = stats.PotionShotVGenerator;

            if (magicVMonsterUpgraded)
            {
                // deal damage to all on screen monsters
                magicVGDamage = stats.ExtraPotionShotVMonster;

            }
            else
                magicVGDamage = stats.PotionShotVMonster;

        CameraController.Instance.DamageAllOnScreen(magicVMDamage, magicVGDamage);
    }

    public void Attack(GameObject target)
    {
        int damage = 0;

        if (meleeVMonstersUpgraded)
        {
            damage = (int)stats.ExtraMeleeVMonsters;
        }
        else
        {
            damage = (int)stats.MeleeVMonsters;
        }

        if (target.CompareTag("Generator"))
        {
            GeneratorMissPercent hitPercent = GeneratorMissPercent.Always;
            float hitChance = 0;
            if (meleeVGeneratorsUpgraded)
                hitPercent = stats.ExtraMeleeVGenerators;
            else
                hitPercent = stats.MeleeVGenerators;

            switch (hitPercent)
            {
                case GeneratorMissPercent.Always:
                    break;
                case GeneratorMissPercent.High:
                    hitChance = 0.4f;
                    break;
                case GeneratorMissPercent.Medium:
                    hitChance = 0.7f;
                    break;
                case GeneratorMissPercent.Low:
                    hitChance = 0.9f;
                    break;
                default:
                    break;
            }
            if (Random.Range(0, 1) < hitChance)
            {
                target.GetComponent<Generator>().TakeDamage(damage);
            }
            
        }
        else if (target.CompareTag("Enemy"))
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void Shoot(Vector3 dir)
    {
        if (!projectileOnScreen)
        {
            int damage;
            float speed;
            if (shotPowerUpgraded)
                damage = (int)stats.ExtraShotStrength;
            else
                damage = (int)stats.ShotStrength;

            if (shotSpeedUpgraded)
                speed = stats.ExtraShotTravelSpeed;
            else
                speed = stats.ShotTravelSpeed;

            // Debug.Log(stats.ShotCollisionBox.ToString() + "Projectile");
            //GameObject shot = (GameObject) Resources.Load("SmallProjectile");
            GameObject shot = Instantiate(projectileRefs[0], transform.position, Quaternion.Euler(dir));
            Projectile projectile = shot.GetComponent<Projectile>();
            projectile.Setup(damage, speed, this);
            ToggleProjectileOnScreen();
        }
    }

    public void ToggleProjectileOnScreen()
    {
        projectileOnScreen = !projectileOnScreen;
    }

    public void AssignUpgrades(bool[] stats)
    {
        armorStrengthUpgraded = stats[0];
        shotPowerUpgraded = stats[1];
        shotSpeedUpgraded = stats[2];
        magicVMonsterUpgraded = stats[3];
        magicVGeneratorUpgraded = stats[4];
        potionShotVMonsterUpgraded = stats[5];
        potionShotVGeneratorUpgraded = stats[6];
        meleeVMonstersUpgraded = stats[7];
        meleeVGeneratorsUpgraded = stats[8];
        runningSpeedUpgraded = stats[9];
    }
}
