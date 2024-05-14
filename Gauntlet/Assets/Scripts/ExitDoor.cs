using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] int nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().CancelMoveEarly();
            TempGameManager.Instance.GotoLevel(nextLevel);
        }
    }
}
