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

    private bool moving = false;
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
            MoveTowardsPlayer();
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        Debug.Log("moving");
        Transform objectInWay = DetectInDirection(direction);
        if (objectInWay == null)
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
        }
        // trying to make the enemy move after the player moves so they dont end up on top of each other
        /*else if (objectInWay.transform.CompareTag("Player") && objectInWay.gameObject.GetComponent<PlayerController>().moving)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 nextPlayerMoveDir = objectInWay.gameObject.GetComponent<PlayerController>().nextMoveDir;


            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + nextPlayerMoveDir * moveDist;
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
        }*/
    }


    private Transform DetectInDirection(Vector3 direction)
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction, Color.blue, 5);
        if (Physics.Raycast(transform.position, direction, out hit, detectDist))
        {
            Debug.Log(hit.transform.name);
            return hit.transform;
        }
        return null;
    }

    private void MoveTowardsPlayer()
    {
        // move up z is greater
        // move left x is greater
        // move down z is smaller
        // move right x is smaller 

        // player is to the right of the enemy
        if (transform.position.x < playerTarget.transform.position.x)
        {
            // player is higher than the enemy
            if (transform.position.z < playerTarget.transform.position.z)
            {
                Debug.Log("move up right");
                StartCoroutine(Move(Vector3.right + Vector3.forward)); 
            }
            // player is below the enemy
            else if (transform.position.z > playerTarget.transform.position.z)
            {
                Debug.Log("move down right");
                StartCoroutine(Move(Vector3.right + Vector3.back));
            }
            // player is at the same height
            else
            {
                Debug.Log("move right");
                StartCoroutine(Move(Vector3.right));
            }
        }
        // player is to the left of the enemy
        else if (transform.position.x > playerTarget.transform.position.x)
        {
            // player is higher than the enemy
            if (transform.position.z < playerTarget.transform.position.z)
            {
                Debug.Log("move up left");
                StartCoroutine(Move(Vector3.left + Vector3.forward));
            }
            // player is below the enemy
            else if (transform.position.z > playerTarget.transform.position.z)
            {
                Debug.Log("move down left");
                StartCoroutine(Move(Vector3.left + Vector3.back));
            }
            // player is at the same height
            else
            {
                Debug.Log("move left");
                StartCoroutine(Move(Vector3.left));
            }
        }
        // player is at the same x
        else
        {
            // player is higher than the enemy
            if (transform.position.z < playerTarget.transform.position.z)
            {
                Debug.Log("move up");
                StartCoroutine(Move(Vector3.forward));
            }
            // player is below the enemy
            else if (transform.position.z > playerTarget.transform.position.z)
            {
                Debug.Log("move down");
                StartCoroutine(Move(Vector3.back));
            }
        }

    }

    private void Attack()
    {
        
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
