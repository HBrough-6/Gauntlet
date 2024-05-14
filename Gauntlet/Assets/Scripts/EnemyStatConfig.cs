using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatConfig", menuName = "Stat/Enemy Stats", order = 3)]
public class EnemyStatConfig : ScriptableObject, IEnemyStats
{
    [Range(0, 5)]
    [Tooltip("The speed that the enemy moves at")]
    [SerializeField] private float speed;

    [Range(0, 30)]
    [Tooltip("damage at level 1")]
    [SerializeField] private int damageLevel1;

    [Range(0, 30)]
    [Tooltip("damage at level 2")]
    [SerializeField] private int damageLevel2;

    [Range(0, 30)]
    [Tooltip("damage at level 3")]
    [SerializeField] private int damageLevel3;
    
    [Range(0, 20)]
    [Tooltip("How far ranged enemies can shoot")]
    [SerializeField] private float range;

    [Range(0, 20)]
    [Tooltip("how much damage ranged attacks can do")]
    [SerializeField] private float rangedDamage;

    [Range(0, 1000)]
    [Tooltip("how many points the enemy is worth")]
    [SerializeField] private int points;

    [Tooltip("The type of Enemy")]
    [SerializeField] private EnemyCategory type;

    public float Speed
    {
        get { return speed; }
    }

    public int DamageLevel1
    {
        get { return damageLevel1; }
    }

    public int DamageLevel2
    {
        get { return damageLevel2; }
    }

    public int DamageLevel3
    {
        get { return damageLevel3; }
    }

    public float Range
    {
        get { return range; }
    }
    
    public float RangedDamage
    {
        get { return rangedDamage; }
    }

    public int Points
    {
        get { return points; }
    }

    public EnemyCategory Type
    { 
        get { return type; }
    }
}
