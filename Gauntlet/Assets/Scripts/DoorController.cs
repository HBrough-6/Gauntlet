using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
