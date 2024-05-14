using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLife : MonoBehaviour
{
    private float life = .8f;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}