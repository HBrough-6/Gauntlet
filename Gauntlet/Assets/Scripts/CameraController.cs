using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    public List<GameObject> GeneratorsOnScreen = new List<GameObject>();

    public List<Transform> players = new List<Transform>();


    private void GetCenterPoint()
    {
        Vector3 center = Vector3.zero;

        if (players.Count == 1)
        {
            center = players[0].position;
        }
        else
        {
            var bounds = new Bounds(players[0].position, Vector3.zero);
            for (int i = 0; i < players.Count; i++)
            {
                bounds.Encapsulate(players[i].position);
            }
            center = bounds.center;
        }
        transform.position = new Vector3(center.x, transform.position.y, center.z);
    }

    private void LateUpdate()
    {
        GetCenterPoint();
    }

    private void ToggleEnemyOnScreen(GameObject enemy)
    {
        if (EnemiesOnScreen.Contains(enemy))
        {
            Debug.Log("TOGGLING");
            EnemiesOnScreen.Remove(enemy);
            enemy.GetComponent<NewEnemyMovement>().ToggleOnScreen();
        }
        else
        {
            EnemiesOnScreen.Add(enemy);
            Debug.Log("TOGGLING");
            enemy.GetComponent<NewEnemyMovement>().ToggleOnScreen();
            // enemy.OnScreen();
        }
    }

    private void ToggleGeneratorOnScreen(GameObject generator)
    {
        if (GeneratorsOnScreen.Contains(generator))
        {
            GeneratorsOnScreen.Remove(generator);
            // generator.OffScreen();
        }
        else
        {
            GeneratorsOnScreen.Add(generator);
            // generator.OnScreen();
        }
    }

    public void DamageAllOnScreen(int monsterDamage, int GeneratorDamage)
    {
        for (int i = 0; i < EnemiesOnScreen.Count; i++)
        {
            //EnemiesOnScreen[i].GetComponent<EnemyMovement>().TakeDamage(monsterDamage);
            EnemiesOnScreen[i].SetActive(false);
        }

        for (int i = 0; i < GeneratorsOnScreen.Count; i++)
        {
            GeneratorsOnScreen[i].GetComponent<Generator>().TakeDamage(GeneratorDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ToggleEnemyOnScreen(other.gameObject);
        }
        else if (other.CompareTag("Generator"))
        {
            ToggleGeneratorOnScreen(other.gameObject);
        }
    }
}
