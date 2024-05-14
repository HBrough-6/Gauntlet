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

    [SerializeField]
    private Vector2 joystickAngle;
    public void DashAim(InputAction.CallbackContext ctx)
    {
        Vector2 vec = ctx.ReadValue<Vector2>();
    }




    // Start is called before the first frame update
    void Start()
    {
        canSwing = true;
        canShoot = true;

        controller = gameObject.GetComponent<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        Debug.Log(joystickAngle);
        OnMoveUpRight();
        OnMoveUpLeft();
        OnMoveDownRight();
        OnMoveDownLeft();

    }

    public void OnMove(InputAction.CallbackContext angle)
    {
        joystickAngle = angle.ReadValue<Vector2>();

    }


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
    public Vector2 SnapAngle(Vector2 vector, int increments)
    {
        float angle = Mathf.Atan2(vector.y, vector.x);
        float direction = ((angle / Mathf.PI) + 1) * 0.5f; // Convert to [0..1] range from [-pi..pi]
        float snappedDirection = Mathf.Round(direction * increments) / increments; // Snap to increment
        snappedDirection = ((snappedDirection * 2) - 1) * Mathf.PI; // Convert back to [-pi..pi] range
        Vector2 snappedVector = new Vector2(Mathf.Cos(snappedDirection), Mathf.Sin(snappedDirection));
        return vector.magnitude * snappedVector;
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


    // Update is called once per frame
    void Update()
    {

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
        yield return new WaitForSeconds(meleeDelayTime);
    }
    /// <summary>
    /// Projectile attack
    /// </summary>
    private void ProjectileAttack()
    {
        if (canShoot)
        {
            StartCoroutine(WalkAttack());
            Instantiate(projectileWeapon, transform.position, Quaternion.identity);
            StartCoroutine(ShootDelay());
        }
    }
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelayTime);
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