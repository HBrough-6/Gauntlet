using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private PlayerData player;

    private void Update()
    {
        Move();
    }

    public void Setup(int dmg, float travelSpeed, PlayerData data)
    {
        damage = dmg;
        speed = travelSpeed;
        player = data;
    }

    private void Move()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime * 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Damage: " + damage);
        Debug.Log("hit");
        bool keep = false;
        if (other.CompareTag("Enemy"))
        {
            if (!other.GetComponent<NewEnemyMovement>().invisible)
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
            else
            {
                keep = true;
            }
        }
        else if (other.CompareTag("Generator"))
        {
            // other.GetComponent<Generator>().TakeDamage(damage);
            
        }
        else if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            item.PlayerShot(player);
            // projectiles pass through objects
            if (item.itemStats.AddsKey > 0)
            {
                keep = true;
            }
        }
        // dont hit yourself 
        else if (other.GetComponent<PlayerData>() == player)
        {
            keep = true;
        }

        if (!keep)
        {
            player.ToggleProjectileOnScreen();
            Destroy(gameObject);
        }
        
    }
}
