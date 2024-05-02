using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clientmanager : MonoBehaviour
{
    private Generator _pool;

    private void Start()
    {
        _pool = gameObject.AddComponent<Generator>();
    }



    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Drone"))
            _pool.SpawnEnemy();
    }

}
