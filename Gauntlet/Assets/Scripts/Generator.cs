using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Enemy enemyPrefab;
    public IObjectPool<Enemy> _enemyPool;
    private float timeSinceSpawn;
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    [SerializeField] private LevelManager levelManager;

    private bool onScreen = false;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager.AddGenerator(this);
        _enemyPool = new ObjectPool<Enemy>(CreateEnemy,
                    OnTakeFromPool,
                    OnReturntoPool,
                    OnDestroyPoolObject,
                    true,
                    stackDefaultCapacity,
                    maxPoolSize);
    }


    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetPool(_enemyPool);
        return enemy;
    }

    private void Update()
    {
        SpawnEnemy();
    }

   public void ToggleOnScreen()
    {
        onScreen = !onScreen;
    }




    /// <summary>
    /// Spawns enemy in one of the positions around generator
    /// and matches enemy's level with the generator level
    /// </summary>
    /// <param name="enemy"></param>
    private void OnTakeFromPool(Enemy enemy)
    {
        int randomValue = Random.Range(0, spawnPositions.Length);
        enemy.gameObject.SetActive(true);
        enemy.SetLevel(generatorLevel);
        Transform spawnAt = spawnPositions[randomValue];
        enemy.transform.position = spawnAt.position;
    }

    //returns enemy to pool
    private void OnReturntoPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    /// <summary>
    /// Sets time interval for enemies to spawn
    /// </summary>
    public void SpawnEnemy()
    {
        if (/*onScreen &&*/ Time.time > timeSinceSpawn)
        {
            _enemyPool.Get();
            timeSinceSpawn = Time.time + spawnTime;
        }
    }


    public void TakeDamage(int damage)
    {
        generatorLevel -= damage;
        if (generatorLevel <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /*/// <summary>
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
    }*/
}
