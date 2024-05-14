using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Transform levelObjectsRef;
    private List<Generator> Generators = new List<Generator>();

    [SerializeField] private GameObject[] levelSpawnPoints;
    [SerializeField] private int levelNumber;
    public int LevelNumber
        { 
            get { return levelNumber;  }
        }

    private void Awake()
    {
        // find the Teleport points for the level
        levelSpawnPoints = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            levelSpawnPoints[i] = GameObject.Find("Level" + levelNumber.ToString() + "SpawnPoint" + (i+1));
        }


        // assign yourself to the GameManager
        TempGameManager.Instance.addLevel(this);

        // get the level objects
        levelObjectsRef = transform.parent.Find("Objects");
    }

    // adds a generator to the list
    public void AddGenerator(Generator generator)
    {
        Generators.Add(generator);
    }

    // clears Enemies from the level
    private void ClearGenerators()
    {
        for (int i = 0; i < Generators.Count; i++)
        {
            // clear enemies from the level
        }
    }

    private void CycleGenerators()
    {
        for (int i = 0; i < Generators.Count; i++)
        {
            // tell generator to spawn a few enemies
        }
    }

    // enables or disables all objects in the level
    private void SetLevelActive(bool status)
    {
        levelObjectsRef.gameObject.SetActive(status);
    }

    // teleports players to the spawn points in the level
    private void TeleportPlayers()
    {
        List<Transform> players = CameraController.Instance.players;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = levelSpawnPoints[i].transform.position;
        }
    }

    // activates the level, teleports players to the spawn points in the level,
    // and tells generators to spawn enemies a times
    public void EnterLevel()
    {

        //SetLevelActive(true);
        TeleportPlayers();
        CycleGenerators();
    }

    // disables the level and clears all the enemies in the level
    public void ExitLevel()
    {
        ClearGenerators();
        //SetLevelActive(false);
    }
}
