using Unity.VisualScripting;
using UnityEngine;

public enum GeneratorMissPercent
{
    Always,
    High,
    Medium,
    Low

}

public enum ProjectileHitboxSize
{
    Large,
    Medium,
    Small
}
public interface IStats
{
    string CharacterName { get; }
    float ArmorStrength { get; }
    float ExtraArmorStrength { get; }
    float ShotStrength { get; }
    float ExtraShotStrength { get; }
    float ShotTravelSpeed { get; }
    float ExtraShotTravelSpeed { get; }
    ProjectileHitboxSize ShotCollisionBox { get; }
    int MagicVMonster { get; }
    int ExtraMagicVMonster { get; }
    int MagicVGenerator { get; }
    int ExtraMagicVGenerator { get; }
    int PotionShotVMonster { get; }
    int ExtraPotionShotVMonster { get; }
    int PotionShotVGenerator { get; }
    int ExtraPotionShotVGenerator { get; }
    float MeleeVMonsters { get; }
    float ExtraMeleeVMonsters { get; }
    GeneratorMissPercent MeleeVGenerators { get; }
    GeneratorMissPercent ExtraMeleeVGenerators { get; }
    int RunningSpeed { get; }
    int ExtraRunningSpeed { get; }
}
