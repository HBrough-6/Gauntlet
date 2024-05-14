using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
/*
 * [Rico,Alex]
 * 5/7/24
 * This code is responsible for making an enemy pool, 
 * spawning enemies from the pool, and returning them. 
 */
public class Generator : MonoBehaviour
{
    //spawner type and level variables
    [SerializeField] int generatorLevel;
    [SerializeField] bool BlockGen;

    //pool variables
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] private float spawnTime = .5f;
    [SerializeField] private TempEnemy enemyPrefab;
    public IObjectPool<TempEnemy> _enemyPool;
    private float timeSinceSpawn;
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

//[SerializeField] private LevelManager levelManager;

    // Start is called before the first frame update
    void Awake()
    {
//levelManager.addGenerator(this);
        _enemyPool = new ObjectPool<TempEnemy>(CreateEnemy,
                    OnTakeFromPool,
                    OnReturntoPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize);
    }


    private TempEnemy CreateEnemy()
    {
        TempEnemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(_enemyPool);
        return enemy;
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void OnScreen()
    {
        //
    }
    private void OffScreen()
    {
        //
    }




    /// <summary>
    /// Spawns enemy in one of the positions around generator
    /// and matches enemy's level with the generator level
    /// </summary>
    /// <param name="enemy"></param>
    private void OnTakeFromPool(TempEnemy enemy)
    {
        int randomValue = Random.Range(0, spawnPositions.Length);
        enemy.gameObject.SetActive(true);
        enemy.enemyLevel = generatorLevel;
        enemy._currentHealth = generatorLevel;
        Transform spawnAt = spawnPositions[randomValue];
        enemy.transform.position = spawnAt.position;
    }

    //returns enemy to pool
    private void OnReturntoPool(TempEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(TempEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    /// <summary>
    /// Sets time interval for enemies to spawn
    /// </summary>
    public void SpawnEnemy()
    {
        if (Time.time > timeSinceSpawn)
        {
            _enemyPool.Get();
            timeSinceSpawn = Time.time + spawnTime;
        }
    }


    private void TakeDamage()
    {
        if (generatorLevel == 1)
        {
            //give player points
            Destroy(gameObject);
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
            TakeDamage();
        }
    }
}
=======
using UnityEngine;

public class Generator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> Heath
