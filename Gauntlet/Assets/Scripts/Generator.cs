using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //lvl
    private int generatorLevel;

    //Spawn variables
    private float delayTime;
    private bool canSpawn;

    //list of enemies to spawn
    public GameObject Ghost;
    public GameObject Grunt;
    public GameObject Demon;
    public GameObject Lobber;
    public GameObject Sorcerer;
    public GameObject Thief;



    

    private void SpawnEnemy()
    {
        if(canSpawn)
        {
            //spawn enemy

        }
        StartCoroutine(SpawnDelay());
    }

    private void TakeDamage()
    {
        if(generatorLevel == 1)
        {
            //destroy
        }
        if (generatorLevel == 2)
        {
            generatorLevel--;
        }
        if (generatorLevel == 3)
        {
            generatorLevel--;
        }
    }
    private void DetectOpenSquare()
    {

    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(delayTime);
    }
}