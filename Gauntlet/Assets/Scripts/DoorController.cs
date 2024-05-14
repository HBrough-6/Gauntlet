using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Brough, Heath
 *  4/11/24
 * Script for keeping dors locked or unlcked
 */

public class DoorController : MonoBehaviour
{
    private List<Door> doors = new List<Door>();

    public void AddDoor(Door door)
    {
        doors.Add(door);
    }
    public void Unlock()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].gameObject.SetActive(false);
        }
    }

    public void Lock()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].gameObject.SetActive(true);
        }
    }
}
