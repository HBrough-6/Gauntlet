using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int enemyLevel;
    public int enemySpeed;
    public Vector3 enemyDamage;
    public int score;


    public void TakeDamage(int damage)
    {
        if ((enemyLevel -= damage) <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLevel()
    {
        switch (enemyLevel)
        {
            case 0:
                //enemyDamage = 5;
                break;
            case 1:
                //enemyDamage = 8;
                break;
            case 2:
                //enemyDamage = 10;
                break;
            default:
                break;
        }

    }








}
