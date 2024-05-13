using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public List<GameObject> EnemiesOnScreen = new List<GameObject>();

    public List<GameObject> GeneratorsOnScreen = new List<GameObject>();

    public GameObject[] players = new GameObject[4];
    private int numPlayers = 2;

    public float averageX = 0;
    public float averageZ = 0;

    private void AverageCameraPos()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            averageX += players[i].transform.position.x;
            averageZ += players[i].transform.position.z;
        }

        averageX /= numPlayers;
        averageZ /= numPlayers;
        transform.position = new Vector3(averageX, transform.position.y, averageZ);
    }

    private void LateUpdate()
    {
        AverageCameraPos();
    }

    private void ToggleEnemyOnScreen(GameObject enemy)
    {
        if (EnemiesOnScreen.Contains(enemy))
        {
            EnemiesOnScreen.Remove(enemy);
            // enemy.OffScreen();
        }
        else
        {
            EnemiesOnScreen.Add(enemy);
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
            //GeneratorsOnScreen[i].GetComponent<Generator>().TakeDamage(GeneratorDamage);
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
