using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public IObjectPool<Generator> Pool { get; set; }




    public int enemyLevel;
    public int enemySpeed;
    public Vector3 enemyDamage;
    public int score;


    private void TakeDamage()
    {
        //
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
