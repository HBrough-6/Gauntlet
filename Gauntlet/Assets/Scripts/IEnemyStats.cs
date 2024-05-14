using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyCategory
{
    Death,
    Grunt,
    Ghost,
    Sorcerer,
    Thief
}

public enum EnemyBehavior
{
    Melee,
    Ranged
}

public interface IEnemyStats
{
    float Speed { get; }
    int DamageLevel1 { get; }
    int DamageLevel2 { get; }
    int DamageLevel3 { get; }
    float Range { get; }
    float RangedDamage { get; }
    int Points { get; }
    EnemyCategory Type { get; }
}
