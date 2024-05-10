using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TempEnemy : MonoBehaviour
{
    public float _currentHealth;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float timeToSelfDestruct = 3f;


    private IObjectPool<TempEnemy> _enemyPool { get; set; }
    public void SetPool(IObjectPool<TempEnemy> pool)
    {
        _enemyPool = pool;
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        //AttackPlayer();
        StartCoroutine(SelfDestruct());

    }
    private void OnDisable()
    {
        //ResetDrone();
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
    private void ResetEnemy()
    {
        //_currentHealth = maxHealth;
    }
    
    public void AttackPlayer()
    {
        Debug.Log("Attack player behaviors would go here");
    }
*/

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            ReturnToPool();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hgjhkuj");
        TakeDamage(_currentHealth);
    }
}
