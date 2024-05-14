using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public IObjectPool<Generator> Pool { get; set; }



    [SerializeField] public EnemyStatConfig stats;

    [SerializeField] protected int level;

    protected NewEnemyMovement movement;

    public void TakeDamage(int damage)
    {
        if ((level -= damage) <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetLevel(int newLevel)
    {
        level = newLevel;
    }

    public int GetDamageAmount()
    {
        switch (level)
        {
            case 1:
                return stats.DamageLevel1;
            case 2:
                return stats.DamageLevel2;
            case 3:
                return stats.DamageLevel3;
            default:
                return stats.DamageLevel1;
        }
    }
}
