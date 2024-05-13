using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NextAction
{
    NormalMove,
    AlignmentMove,
    Attack
}

public class NewEnemyMovement : MonoBehaviour
{
    // find direction of player
    // check in that direction
    // attack or move based on the output 

    private NextAction nextAction;

    [SerializeField] private float speed = 2;
    private float moveDist = 0.5f;
    private bool moving = false;

    [SerializeField] private float detectDist = 0.5f;

    [SerializeField] private Transform closestPlayer;
    private Vector3 moveDirection;
    private Vector3 alignmentMoveDirection;

    private IEnumerator attackCoroutine;
    private bool attacking = false;

    [SerializeField] private float attackCooldownDuration = 2;

    private void Awake()
    {
        attackCoroutine = Attack();
    }

    private void Update()
    {

        if (!moving && !attacking)
        {
            Debug.Log("making new decision");
            // enemy is going to make a new decision
            // stop attacking
            StopCoroutine(attackCoroutine);
            // find the next direction to move in
            moveDirection = DetermineMoveDirection();
            // find what is in the next tile in the direction to move in
            DetectInDirection(moveDirection);
            // Move, align, or start attacking
            DoNextAction();
        }
        else if (attacking)
        {
            Debug.Log("Checking player position");
            // check if the player is still adjacent
            attacking = PlayerStillInRange(moveDirection);
        }
    }

    private void DoNextAction()
    {
            switch (nextAction)
            {
                case NextAction.NormalMove:
                    StartCoroutine(MoveTowards(moveDirection));
                    break;
                case NextAction.AlignmentMove:
                    StartCoroutine(MoveTowards(alignmentMoveDirection));
                    break;
                case NextAction.Attack:
                StartCoroutine(attackCoroutine);
                    break;
                default:
                    break;
            }
    }

    private IEnumerator MoveTowards(Vector3 direction)
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

    // returns the next direction that the enemy needs to move in
    private Vector3 DetermineMoveDirection()
    {
        float xDif = Mathf.Abs(transform.position.x - closestPlayer.position.x);
        float zDif = Mathf.Abs(transform.position.z - closestPlayer.position.z);

        Vector3 direction = Vector3.zero;

        // the enemy is further away in the x axis
        if (xDif > zDif)
        {
            if (transform.position.x < closestPlayer.position.x)
            {
                // move to the right
                Debug.Log("move right");
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
            if (transform.position.z < closestPlayer.position.z)
            {
                // move up
                Debug.Log("move up");
                direction = Vector3.forward;
            }
            else
            {
                // move down
                Debug.Log("move down");
                direction = Vector3.back;
            }
        }
        else
        {
            // player is higher than the enemy
            if (transform.position.z < closestPlayer.position.z)
            {
                // move up
                Debug.Log("move up");
                direction = Vector3.forward;
            }
            else
            {
                // move down
                Debug.Log("move down");
                direction = Vector3.back;
            }
        }
        return direction;
    }

    private IEnumerator Attack()
    {
        attacking = true;
        Debug.Log("Start attacking");
        while (true)
        {
            yield return new WaitForSeconds(attackCooldownDuration / 2);
            // attack player
            if (PlayerStillInRange(moveDirection))
            {
                // player hasn't moved
                closestPlayer.gameObject.GetComponent<Inventory>().TakeDamage(5);
                Debug.Log("attacked");
            }
            // cooldown
            yield return new WaitForSeconds(attackCooldownDuration/2);
        }
    }

    private IEnumerator PlayerInRange()
    {
        while (true)
        {

        }
    }

    private bool PlayerStillInRange(Vector3 direction)
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

            // determine left and right relative to the last move
            relativeLeft = Vector3.left;
            relativeRight = Vector3.right;
        }
        else if (direction == Vector3.left)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(-0.25f, 0, 0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.forward;
            relativeRight = Vector3.back;
        }
        else if (direction == Vector3.back)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.right;
            relativeRight = Vector3.left;
        }
        else if (direction == Vector3.right)
        {
            leftTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, 0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.forward;
            relativeRight = Vector3.back;
        }
        bool leftHit = false;
        bool rightHit = false;
        // send out left and right rays
        Debug.DrawRay(leftTileOffset, direction, Color.red, 10);
        Debug.DrawRay(rightTileOffset, direction, Color.red, 10);
        if (Physics.Raycast(leftTileOffset, direction, out hit, detectDist)) { leftHit = true; }
        if (Physics.Raycast(rightTileOffset, direction, out hit2, detectDist)) { rightHit = true; }

        if (leftHit && rightHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // determines the next action of the enemy
    private void DetectInDirection(Vector3 direction)
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

            // determine left and right relative to the last move
            relativeLeft = Vector3.right;
            relativeRight = Vector3.left;
        }
        else if (direction == Vector3.left)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(-0.25f, 0, 0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.forward;
            relativeRight = Vector3.back;
        }
        else if (direction == Vector3.back)
        {
            leftTileOffset = transform.position + new Vector3(-0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.right;
            relativeRight = Vector3.left;
        }
        else if (direction == Vector3.right)
        {
            leftTileOffset = transform.position + new Vector3(0.25f, 0, -0.25f);
            rightTileOffset = transform.position + new Vector3(0.25f, 0, 0.25f);

            // determine left and right relative to the last move
            relativeLeft = Vector3.forward;
            relativeRight = Vector3.back;
        }
        bool leftHit = false;
        bool rightHit = false;
        // send out left and right rays
        Debug.DrawRay(leftTileOffset, direction, Color.red, 10);
        Debug.DrawRay(rightTileOffset, direction, Color.red, 10);
        if (Physics.Raycast(leftTileOffset, direction, out hit, detectDist)) { leftHit = true; }
        if (Physics.Raycast(rightTileOffset, direction, out hit2, detectDist)) { rightHit = true; }

        if (leftHit && rightHit)
        {
            // something is in the way
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                nextAction = NextAction.Attack;
                // player is in the way
                // attack
            }

        }
        else if (leftHit)
        {
            // enemy needs to align itself
            nextAction = NextAction.AlignmentMove;
            // move to the relative right
            alignmentMoveDirection = relativeRight;
        }
        else if (rightHit)
        {
            // enemy needs to align itself
            nextAction = NextAction.AlignmentMove;
            // move to the relative left
            alignmentMoveDirection = relativeLeft;
        }
        else
        {
            // make a normal move
            nextAction = NextAction.NormalMove;
        }

    }
}
