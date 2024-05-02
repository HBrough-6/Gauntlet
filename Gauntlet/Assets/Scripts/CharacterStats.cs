using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float armorStrength;
    public int playerMagic;
    private int playerHealth;
    public static int playerSpeed;





    private void OnTriggerEnter(Collider other)
    {
        //Resources
        if (other.gameObject.CompareTag("Food"))
        {
            playerHealth += 100;
        }
        if (other.gameObject.CompareTag("Potion"))
        {
            //s
        }

        //Enemies
        if (other.gameObject.CompareTag("Ghost"))
        {
            //s
        }
        if (other.gameObject.CompareTag("Melee"))
        {
            //s
        }
        if (other.gameObject.CompareTag("Fireball"))
        {
            //s
        }
        if (other.gameObject.CompareTag("Rock"))
        {
            playerHealth -= 3;
        }
        if (other.gameObject.CompareTag("Death"))
        {
            //s
        }
    }





}
