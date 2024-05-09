using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clientmanager : MonoBehaviour
{
    private Generator _generator;

    private void Start()
    {
        //_generator = gameObject.AddComponent<Generator>();
    }



    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Drone"))
        {
            _generator.SpawnEnemy();
                Debug.Log("Yeehaw");
        }


    }

}
