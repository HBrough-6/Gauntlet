using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool onScreen = false;
    public float speed = 2;
    private float moveDist = 0.5f;
    [SerializeField] private float detectDist = 0.5f;
    [SerializeField] private bool attackOnCooldown = false;
    [SerializeField] private GameObject playerTarget;

    private Vector3 nextMove = new Vector3(0,-1, 0);

    private bool moving = false;
    public bool canAttack = false;
    private Vector3 moveForward = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            FixedMove();
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
            if (!moving)
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
        // DETERMINE THE OFFSET THAT THE ENEMY NEEDS TO BE RESET TO
        // ABOVE 0.125 WHAT IS IT?
        // BELOW 0.125 WHAT IS IT?
        if (transform.position.x % 0.25 != 0 || transform.position.z % 0.25 != 0)
        {
            float newX, newZ;
            if (transform.position.x % 2 <= 0.125f)
            {
                newX = transform.position.x - transform.position.x % 2;
            }
            else
            {
                newX = transform.position.x - transform.position.x % 2;
            }
            newX = transform.position.x - transform.position.x % 2;
            newZ = transform.position.z - transform.position.z % 2;
        }
        
    }

    // player is to the right of the enemy
    private void FixedMove()
    {
        // compare difference between distance in x's and z's
        // move in the direction that is further away
        // find distance between the x and z values
        float xDif = Mathf.Abs(transform.position.x - playerTarget.transform.position.x);
        float zDif = Mathf.Abs(transform.position.z - playerTarget.transform.position.z);

        Vector3 direction = Vector3.zero;

            // the enemy is further away in the x axis
            if (xDif > zDif)
            {
                if (transform.position.x < playerTarget.transform.position.x)
                {
                    // move to the right
                    Debug.Log("move right");
                    //StartCoroutine(Move(Vector3.right));
                    direction = Vector3.right;
                }
                else
                {
                    // move to the left
                    Debug.Log("move left");
                    //StartCoroutine(Move(Vector3.left));
                    direction = Vector3.left;
                }
            }
            // the enemy is further away in the z axis
            else if (xDif < zDif)
            {
                // player is higher than the enemy
                if (transform.position.z < playerTarget.transform.position.z)
                {
                    // move up
                    Debug.Log("move up");
                    //StartCoroutine(Move(Vector3.forward));
                    direction = Vector3.forward;
                }
                else
                {
                    // move down
                    Debug.Log("move down");
                    //StartCoroutine(Move(Vector3.back));
                    direction = Vector3.back;
                }
        }
        else
        {
            // player is higher than the enemy
            if (transform.position.z <
                playerTarget.transform.position.z)
            {
                // move up
                Debug.Log("move up");
                //StartCoroutine(Move(Vector3.forward));
                direction = Vector3.forward;
            }
            else
            {
                // move down
                Debug.Log("move down");
                //StartCoroutine(Move(Vector3.back));
                direction = Vector3.back;
            }
        }
        Debug.Log(direction);
        bool hasNextMove = DoubleDetect(direction);
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
        else
        {
            if (hasNextMove)
            {
                StartCoroutine(Move(nextMove));
            }
            else
            {
                StartCoroutine(Move(direction));
            }
        }
        
        
    }

    private bool DoubleDetect(Vector3 direction)
    {
        RaycastHit hit;
        RaycastHit hit2;
        Vector3 leftTileOffset = transform.position + new Vector3(-0.25f, 0, 0.25f);
        Vector3 rightTileOffset = transform.position + new Vector3(0.25f, 0, 0.25f);

        Vector3 relativeLeft = Vector3.left;
        Vector3 relativeRight = Vector3.right;

        if (direction == Vector3.forward)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, 0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, 0.25f);
        }
        else if (direction == Vector3.left)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(-0.25f, 0, 0.25f);
        }
        else if (direction == Vector3.back)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);
        }
        else if (direction == Vector3.right)
        {
            leftTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, 0.25f);
        }
        bool leftHit = false;
        bool rightHit = false;
        // send out left and right rays
        Debug.DrawRay(leftTileOffset, direction, Color.red, 10);
        Debug.DrawRay(rightTileOffset, direction, Color.red, 10);
        if (Physics.Raycast(leftTileOffset, direction, out hit, detectDist)) { leftHit = true; }
        if (Physics.Raycast(rightTileOffset, direction, out hit2, detectDist)) { rightHit = true; }


        // determine left and right relative to the last move
        if (direction == Vector3.forward)
        {
            relativeLeft = Vector3.left;
            relativeRight = Vector3.right;
        }
        else if (direction == Vector3.left)
        {
            relativeLeft = Vector3.back;
            relativeRight = Vector3.forward;
        }
        else if (direction == Vector3.back)
        {
            relativeLeft = Vector3.right;
            relativeRight = Vector3.left;
        }
        else if (direction == Vector3.right)
        {
            relativeLeft = Vector3.forward;
            relativeRight = Vector3.back;
        }


        if (leftHit && rightHit)
        {
            Debug.Log("can Attack");
            // reset next move since the raycast did not hit anything
            nextMove = new Vector3(0, -1, 0);
            canAttack = true;
            // attack
        }
        else if (leftHit)
        {
            nextMove = relativeLeft;
        }
        else if (rightHit)
        {
            nextMove = relativeRight;
        }
        else
        {
            // reset next move since the raycast did not hit anything
            nextMove = new Vector3(0, -1, 0);
        }

        if (nextMove == new Vector3(0, -1, 0))
            return false;

        else
            return true;
    }


    private IEnumerator Attack()
    {
        if (canAttack)
        {
            Debug.Log("ATTACK");
            canAttack = false;
            yield return new WaitForSeconds(3);
            canAttack = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            moving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exited");
            moving = false;
        }
    }
}
