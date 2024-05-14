using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TempEnemy : MonoBehaviour
{
    public int _currentHealth;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float timeToSelfDestruct = 3f;
    public int enemyLevel;


    private IObjectPool<TempEnemy> _enemyPool { get; set; }
    public void SetPool(IObjectPool<TempEnemy> pool)
    {
        _enemyPool = pool;
    }

    private void Start()
    {
        enemyLevel = maxHealth;
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());

    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToSelfDestruct);
        TakeDamage(maxHealth);
    }


    private void ReturnToPool()
    {
        _enemyPool.Release(this);

    }
    /*    
    public void AttackPlayer()
    {
        Debug.Log("Attack player behaviors would go here");
    }
*/

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            ReturnToPool();

    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(_currentHealth);
    }
}
