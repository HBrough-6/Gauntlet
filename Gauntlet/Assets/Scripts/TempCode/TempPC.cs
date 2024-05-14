using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerMovement))]
public class TempPC : MonoBehaviour
{
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float moveDist = 0.5f;
    [SerializeField]
    private bool moving = false;
    private Vector3 moveForward = Vector3.forward;

    //controls
    private PlayerMovement controller;
    private Vector3 playerVelocity;


    //melee attack variables
    public GameObject meleeWeapon;
    public Transform meleeSpawn;
    private bool canSwing;

    //projectile attack variables
    public GameObject projectileWeapon;
    public Transform shootSpawn;
    private bool canShoot;



    //collectible variables
    public int potionAmt;
    public int foodAmt;
    public int keyAmt;

    public GameObject projectileObject;
    public Transform projectileSpawn;
    [SerializeField] float projectileSpeed;



    // Start is called before the first frame update
    void Start()
    {
        canSwing = true;
        canShoot = true;

        controller = gameObject.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        //transform.Translate(new Vector3(joystickAngle.x, 0, joystickAngle.y) * speed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        /*
        Debug.Log(joystickAngle);
        OnMoveUpRight();
        OnMoveUpLeft();
        OnMoveDownRight();
        OnMoveDownLeft();
        */
    }
    /*
    public void OnGeneralMovement(InputAction.CallbackContext _context) => joystickAngle = _context.ReadValue<Vector2>();
    

    
    public void OnDiagnolMovement(InputAction.CallbackContext angle)
    {
        joystickAngle = angle.ReadValue<Vector2>();

    }
    */

    public void OnMoveUp()
    {
        Debug.Log("up");
        StartCoroutine(Move(Vector3.forward));
        transform.rotation = Quaternion.Euler(0,0,0);

    }
    public void OnMoveDown()
    {
        Debug.Log("down");
        StartCoroutine(Move(Vector3.back));
        transform.rotation = Quaternion.Euler(0,180, 0);

    }
    public void OnMoveLeft()
    {
        Debug.Log("left");
        StartCoroutine(Move(Vector3.left));
        transform.rotation = Quaternion.Euler(0, 270, 0);

    }
    public void OnMoveRight()
    {
        Debug.Log("right");
        StartCoroutine(Move(Vector3.right));
        transform.rotation = Quaternion.Euler(0, 90,0);


    }

    public void OnMoveUpRight()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.right)+ new Vector3(0,0,.8f)));
            transform.rotation = Quaternion.Euler(0, 45, 0);

        }
    }
    public void OnMoveUpLeft()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.left) + new Vector3(0, 0, .8f)));
            transform.rotation = Quaternion.Euler(0, 315, 0);

        }
    }
    public void OnMoveDownRight()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.right) + new Vector3(0, 0, -0.8f)));
            transform.rotation = Quaternion.Euler(0, 135, 0);

        }
    }
    public void OnMoveDownLeft()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("moveUpRight");
            StartCoroutine(Move((Vector3.left) + new Vector3(0, 0, -0.8f)));
            transform.rotation = Quaternion.Euler(0, 225, 0);

        }
    }
    

    public void OnMelee()
    {
        Debug.Log("ATTACk");
        MeleeAttack();
    }
    public void OnMagic()
    {
        Debug.Log("MAGIC");
        ProjectileAttack();
    }





    private IEnumerator Move(Vector3 direction)
    {
        Transform objectInWay = DetectInDirection(direction);
        if (objectInWay == null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + direction * moveDist;
            moving = true;
            float startTime = Time.time;
            float journeyLength = Vector3.Distance(startPos, endPos);

            float percentComplete = 0;
            while (percentComplete < 1)
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
            moving = false;
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
    IEnumerator SwingDelay()
    {
        StartCoroutine(WalkAttack());
        canSwing = false;
        yield return new WaitForSeconds(.7f);
    }
    /// <summary>
    /// Projectile attack
    /// </summary>
    private void ProjectileAttack()
    {
        if (canShoot)
        {
            StartCoroutine(WalkAttack());
            if (canShoot == true)
            {
                StartCoroutine(ShootProjectiles());
            }
            StartCoroutine(ShootDelay());
        }
    }
    IEnumerator ShootProjectiles()
    {
        if (canShoot == true)
        {
            canShoot = false;
            var projectile = Instantiate(projectileObject, projectileSpawn.position, projectileSpawn.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawn.forward * projectileSpeed;
        }
        StartCoroutine(ShootDelay());
        yield return null;
    }
    /// <summary>
    /// lessens player from spamming the shoot
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(.2f);
    }
    IEnumerator WalkAttack()
    {
        float currentSpeed = speed;
        speed = 0;

        yield return new WaitForSeconds(1f);
        speed = currentSpeed;
    }
    /// <summary>
    /// item container
    /// </summary>
    private void ItemContainer()
    {

    }









    private Transform DetectInDirection(Vector3 direction)
    {
        float dist = 1.1f;
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction, Color.blue, 5);
        if (Physics.Raycast(transform.position, direction, out hit, dist))
        {
            Debug.Log(hit.transform.name);
            return hit.transform;
        }
        return null;
    }
}