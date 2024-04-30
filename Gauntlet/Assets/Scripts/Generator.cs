using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Generator : MonoBehaviour
{
    //spawner types
    private int generatorLevel;
    [SerializeField] bool BlockGen;

    //Spawn variables
    [SerializeField] float delayTime;
    private bool canSpawn;

    //list of enemies to spawn
    public GameObject Ghost;
    public List<GameObject> enemyTypes;
    public GameObject Grunt;
    public GameObject Demon;
    public GameObject Lobber;
    public GameObject Sorcerer;
    public GameObject Thief;
    public GameObject EnemyObject;


    //surrounding spaces
    public GameObject TopLeft;
    public GameObject TopCenter;
    public GameObject TopRight;
    public GameObject LeftSpace;
    public GameObject RightSpace;
    public GameObject LowLeft;
    public GameObject LowCenter;
    public GameObject LowRight;


    //pool
    public int maxPoolSize = 5;
    public int stackDefaultCapacity = 10;
    private IObjectPool<Enemy> _enemyPool;





    private void Update()
    {
        //SpawnEnemy();
    }

    /*
    public IObjectPool<Enemy> Pool
    {
        get
        {
            if (_enemyPool == null)
                _enemyPool = 
                    new ObjectPool<Enemy>(
                    ChooseEnemy,
                    OnTakeFromPool,
                    OnReturntoPool,
                    OnDestroyPoolObject,
                    true, 
                    stackDefaultCapacity,
                    maxPoolSize);

                return _enemyPool;
        }
    }

    private Enemy ChooseEnemy()
    {
        var thief = Resources.Load("Thief");
        Enemy thief = go.AddComponent<Enemy>();
        thief.Pool = Pool;
        return thief; 
    }
    

    private void OnTakeFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReturntoPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }









    
    private void SpawnEnemy()
    {
        var amount = 5;


        for (int i = 0; i < amount; i++)
        {
            var enemy = Pool.Get();
            //cvhange this to spawn it in one of four spots
            //enemy.transform.position = Random.insideUnitSphere * 10;
            Debug.Log("Enemy spawned");


        }

        if (BlockGen)
        {
            //get from pool
        }
        if (!BlockGen)
        {
            //ghosts
        }




        if (canSpawn)
        {
            //spawn enemy


        }
        StartCoroutine(SpawnDelay());
    }
    */
    private void TakeDamage()
    {
        if (generatorLevel == 1)
        {
            //give player points
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


    /// <summary>
    /// Coollisions
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Magic")
        {
            if (!BlockGen)
            {
                //give points
                //take less damage
                TakeDamage();
            }
            if (BlockGen)
            {
                TakeDamage();
            }
        }
        if (collision.rigidbody.tag == "Projectile")
        {
            if (!BlockGen)
            {
                //give points
                //Destroy
            }
            if (BlockGen)
            {
                TakeDamage();
            }
        }
        if (collision.rigidbody.tag == "Magic")
        {
            if (!BlockGen)
            {
                //needs less damage from magic (from valkyrie)
                TakeDamage();

            }
            if (BlockGen)
            {
                TakeDamage();
            }
        }
        if (collision.rigidbody.tag == "Melee")
        {
            if (!BlockGen)
            {
                TakeDamage();

            }
            if (BlockGen)
            {
                TakeDamage();
            }
        }
    }
}