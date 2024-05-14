using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TempCode : MonoBehaviour
{
    //pool
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] private float spawnTime = .5f;
    [SerializeField] private TempEnemy enemyPrefab;
    public IObjectPool<TempEnemy> _enemyPool;
    private float timeSinceSpawn;
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;



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



    //get
    private void OnTakeFromPool(TempEnemy enemy)
    {
        
        int randomValue = Random.Range(0, spawnPositions.Length);
        Debug.Log(randomValue);
        enemy.gameObject.SetActive(true);

        Transform spawnAt = spawnPositions[randomValue];
        enemy.transform.position = spawnAt.position;
        
    }

    //release
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
