using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// Brough, Heath
// Created 4/11
// Handles the movement and actions of a player

public class PlayerController : MonoBehaviour
{
    private float speed = 2;
    private float moveDist = 0.5f;

    public bool moving = false;
    public Vector3 nextMoveDir;
    private Vector3 moveForward = Vector3.forward;

    //controls
    private PlayerMovement controller;
    private Vector3 playerVelocity;

    //melee attack variables
    public GameObject meleeWeapon;
    public Transform meleeSpawn;
    private float meleeDelayTime = 4f;
    private bool canSwing;

    //shoot attack variables
    public GameObject projectileWeapon;
    public Transform shootSpawn;
    private float shootDelayTime = 4f;
    private bool canShoot;


    //collectible variables
    public int potionAmt;
    public int foodAmt;
    public int keyAmt;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<PlayerMovement>();
        canSwing = true;
        canShoot = true;
    }

    private bool meleeOnCooldown = false;


    public bool cancelMove = false;


    /// <summary>
    /// Moves player forward
    /// </summary>
    public void OnMoveUp()
    {
        Debug.Log("up");
        StartCoroutine(Move(Vector3.forward));
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    /// <summary>
    /// Moves player backwards
    /// </summary>
    public void OnMoveDown()
    {
        Debug.Log("down");
        StartCoroutine(Move(Vector3.back));
        transform.rotation = Quaternion.Euler(0, 180, 0);

    }
    /// <summary>
    /// Moves player left
    /// </summary>
    public void OnMoveLeft()
    {
        Debug.Log("left");
        StartCoroutine(Move(Vector3.left));
        transform.rotation = Quaternion.Euler(0, 270, 0);

    }
    /// <summary>
    /// Moves player right
    /// </summary>
    public void OnMoveRight()
    {
        Debug.Log("right");
        StartCoroutine(Move(Vector3.right));
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    /// <summary>
    /// Not used in game dues to no specific diagnol input.
    /// Moves player in diagnol direction
    /// </summary>
    public void OnMoveUpRight()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.right) + new Vector3(0, 0, .8f)));
            transform.rotation = Quaternion.Euler(0, 45, 0);

        }
    }
    /// <summary>
    /// Not used in game dues to no specific diagnol input.
    /// Moves player in diagnol direction
    /// </summary>
    public void OnMoveUpLeft()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.left) + new Vector3(0, 0, .8f)));
            transform.rotation = Quaternion.Euler(0, 315, 0);

        }
    }
    /// <summary>
    /// Not used in game dues to no specific diagnol input.
    /// Moves player in diagnol direction
    /// </summary>
    public void OnMoveDownRight()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.right) + new Vector3(0, 0, -0.8f)));
            transform.rotation = Quaternion.Euler(0, 135, 0);

        }
    }
    /// <summary>
    /// Not used in game dues to no specific diagnol input.
    /// Moves player in diagnol direction
    /// </summary>
    public void OnMoveDownLeft()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.left) + new Vector3(0, 0, -0.8f)));
            transform.rotation = Quaternion.Euler(0, 225, 0);

        }
    }


    /// <summary>
    /// when x is used on controller (ps4),
    /// playe shoots projectile out
    /// </summary>
    public void OnMagic()
    {
        Debug.Log("MAGIC");
        ProjectileAttack();
    }
    /// <summary>
    /// Uses the move ienumerator
    /// </summary>
    /// <param name="direction"></param>
    public void OnMove(Vector3 direction)
    {
        StartCoroutine(Move(direction));
    }




    /// <summary>
    /// movement that registers objects, enemies and genrators in front of player
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public IEnumerator Move(Vector3 direction)
    {

        Transform objectInWay = DetectInDirection(direction);
        if (objectInWay == null || objectInWay.CompareTag("Item") || objectInWay.CompareTag("ExitDoor"))
        {
            cancelMove = false;
            if (!moving)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = transform.position + direction * moveDist;
                moving = true;
                float startTime = Time.time;
                float journeyLength = Vector3.Distance(startPos, endPos);

                float percentComplete = 0;
                while (percentComplete < 1 && !cancelMove)
                {
                    // Distance moved equals elapsed time times speed..
                    float distCovered = (Time.time - startTime) * speed;
                    // Fraction of journey completed equals current distance divided by total distance.
                    float fractionOfJourney = distCovered / journeyLength;
                    // Set our position as a fraction of the distance between the markers.
                    transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                    percentComplete = fractionOfJourney;
                    yield return null;
                }
                cancelMove = false;
                moving = false;
            }
        }
        else if (objectInWay.gameObject.tag == "Enemy" && objectInWay.CompareTag ("Generator"))
        {
            StartCoroutine(MeleeAttack(objectInWay.gameObject));
        }
        else if (objectInWay.CompareTag("Door"))
        {
            objectInWay.GetComponent<Door>().Activate(transform.GetComponent<Inventory>());
        }
    }




    /// <summary>
    /// meleeattack
    /// </summary>
    private void MeleeAttack()
    {
        if (canSwing)
        {
            Instantiate(meleeWeapon, transform.position, Quaternion.identity);
            StartCoroutine(SwingDelay());
        }
    }
    /// <summary>
    /// delays melee attack
    /// </summary>
    /// <returns></returns>
    IEnumerator SwingDelay()
    {
        canSwing = false;
        yield return new WaitForSeconds(meleeDelayTime);
    }
    /// <summary>
    /// Projectile attack
    /// </summary>
    private void ProjectileAttack()
    {
        if (canShoot)
        {
            Instantiate(projectileWeapon, transform.position, Quaternion.identity);
            StartCoroutine(ShootDelay());
        }
    }
    /// <summary>
    /// delays projectile spawn
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelayTime);
    }
    private Transform DetectInDirection(Vector3 direction)
    {
        float dist = 1.1f;
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction, Color.blue, 5);
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            return hit.transform;
        }
        return null;
    }


    /// <summary>
    /// sets the attack and delay
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    private IEnumerator MeleeAttack(GameObject enemy)
    {
        if (!meleeOnCooldown)
        {
            meleeOnCooldown = !meleeOnCooldown;
            GetComponent<PlayerData>().Attack(enemy);
            yield return new WaitForSeconds(1);
            meleeOnCooldown = !meleeOnCooldown;

        }
    }
    /// <summary>
    /// prevents movement from proceedding past currentpoistion
    /// </summary>
    public void CancelMoveEarly()
    {
        cancelMove = true;
    }
}
