using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorController doorController;

    private void Start()
    {
        doorController.AddDoor(this);
    }

    public void Activate(Inventory inventory)
    {
        if (inventory.UseItem(ItemType.Key))
        {
            doorController.Unlock();
        }
    }
}
