using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TempCode : MonoBehaviour
{

    [SerializeField] Transform[] spawnPositions;
    [SerializeField] private float spawnTime = .5f;
     private float timeSinceSpawn;

    [SerializeField] private TempEnemy enemyPrefab;
    public IObjectPool<TempEnemy> _enemyPool;


    //pool
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    //public TempEnemy thiefPrefab;

    //[SerializeField] Vector3 apple;


    // Start is called before the first frame update
    void Awake()
    {
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



    
    private void OnTakeFromPool(TempEnemy enemy)
    {

        enemy.gameObject.SetActive(true);

        Transform  spawnAt = spawnPositions[Random.Range(0,spawnPositions.Length)];
        enemy.transform.position = spawnAt.position;
    }

    private void OnReturntoPool(TempEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(TempEnemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy()
    {
        if (Time.time > timeSinceSpawn)
        {
            _enemyPool.Get();
            timeSinceSpawn = Time.time + spawnTime;
        }


    }
    
}
