using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
/*
 * [Rico,Alex]
 * 5/7/24
 * 
 */
public class Generator : MonoBehaviour
{
    //spawner types
    private int generatorLevel;
    [SerializeField] bool BlockGen;

    //Spawn variables
    [SerializeField] float delayTime;
    private bool canSpawn;


    //public List<GameObject> enemyTypes;
    public GameObject enemySpawn;


    //surrounding spaces
    private Vector3 TopLeft;
    private Vector3 TopCenter;
    private Vector3 TopRight;
    private Vector3 LeftSpace;
    private Vector3 RightSpace;
    private Vector3 LowLeft;
    private Vector3 LowCenter;
    private Vector3 LowRight;


    //pool
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;
    private IObjectPool<Enemy> _enemyPool;



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
        GameObject enemy = Resources.Load("thief") as GameObject;
        return enemy.GetComponent<Enemy>();

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










    public void SpawnEnemy()
    {
        var amount = 3;
        //BlockGen = true;
        //if (BlockGen)
        //{
        for (int i = 0; i < amount; i++)
            {
            Debug.Log("spawnEnemy");

            var spawnEnemy = Pool.Get();
                //cvhange this to spawn it in one of 8 spots
                spawnEnemy.transform.position = new Vector3 (0,0,0);
                //Debug.Log("Enemy spawned");


            }
        //}
        /*
        if (!BlockGen)
        {
            for (int i = 0; i < amount; i++)
            {
                var enemy = Pool.Get();
                //cvhange this to spawn it in one of 8 spots
                enemy.transform.position = LowLeft;
                Debug.Log("Enemy spawned");


            }
        }
        if (canSpawn)
        {
            //spawn enemy


        }
                */
        //StartCoroutine(SpawnDelay());

    }
    
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





    /*
private Enemy ChooseEnemy()
{
    var _enemy = Resources.Load("thief");
    _enemy.Pool = Pool;
    return _enemy;

}
*/

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