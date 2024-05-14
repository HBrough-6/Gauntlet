using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Vector3 moveDirection;
    private Vector3 alignmentMoveDirection;

    private bool attacking = false;

    [SerializeField] private float attackCooldownDuration = 2;

    private bool onScreen = false;

    [SerializeField] private bool deathAttack = false;

    public EnemyCategory type;

    public bool invisible = false;

    private void Awake()
    {
        type = GetComponent<Enemy>().stats.Type;
        switch (type)
        {
            case EnemyCategory.Death:
                StartCoroutine(DeathAttack());
                break;
            case EnemyCategory.Grunt:
                StartCoroutine(Attack());
                break;
            case EnemyCategory.Ghost:
                StartCoroutine(GhostAttack());
                break;
            case EnemyCategory.Sorcerer:
                StartCoroutine(Attack());
                StartCoroutine(Disappear());
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (onScreen)
        {
            if (!moving && !attacking)
            {
                FindClosestPlayer();
                Debug.Log("Attacking: " + attacking);
                Debug.Log("making new decision");
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
                Debug.Log("Player in range: " + PlayerStillInRange(moveDirection));
                attacking = PlayerStillInRange(moveDirection);
            }
        }
    }

    private void FindClosestPlayer()
    {
        float dist = 10000;
        Transform player = null;
        for (int i = 0; i < CameraController.Instance.players.Count; i++)
        {
            float distance = Vector3.Distance(CameraController.Instance.players[i].position, transform.position);
            if (distance < dist)
            {
                dist = distance;
                player = CameraController.Instance.players[i];
            }
        }
        closestPlayer = player;
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
                //Debug.Log("move right");
                direction = Vector3.right;
            }
            else
            {
                // move to the left
                //Debug.Log("move left");
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
                //Debug.Log("move up");
                direction = Vector3.forward;
            }
            else
            {
                // move down
                //Debug.Log("move down");
                direction = Vector3.back;
            }
        }
        else
        {
            // player is higher than the enemy
            if (transform.position.z < closestPlayer.position.z)
            {
                // move up
                //Debug.Log("move up");
                direction = Vector3.forward;
            }
            else
            {
                // move down
                //Debug.Log("move down");
                direction = Vector3.back;
            }
        }
        return direction;
    }

    private IEnumerator Attack()
    {
        Debug.Log("started attacking");
        //Debug.Log("Start attacking");
        while (true)
        {
            yield return new WaitForSeconds(attackCooldownDuration / 2);
            // attack player
            Debug.Log(PlayerStillInRange(moveDirection));
            if (attacking = PlayerStillInRange(moveDirection))
            {
                // player hasn't moved
                closestPlayer.gameObject.GetComponent<PlayerData>().TakeDamage(GetComponent<Enemy>().GetDamageAmount());
                //Debug.Log("attacked");
            }
            // cooldown
            yield return new WaitForSeconds(attackCooldownDuration/2);
        }
    }

    private IEnumerator GhostAttack()
    {
        Debug.Log("started attacking");
        //Debug.Log("Start attacking");
        while (true)
        {
            yield return new WaitForSeconds(attackCooldownDuration / 2);
            // attack player
            Debug.Log(PlayerStillInRange(moveDirection));
            if (attacking = PlayerStillInRange(moveDirection))
            {
                // player hasn't moved
                closestPlayer.gameObject.GetComponent<PlayerData>().TakeDamage(GetComponent<Enemy>().GetDamageAmount());
                Destroy(gameObject);
                //Debug.Log("attacked");
            }
        }
    }

    private IEnumerator DeathAttack()
    {
        while (true)
        {
            if (PlayerStillInRange(moveDirection))
            {
                // player hasn't moved
                closestPlayer.gameObject.GetComponent<PlayerData>().TakeDamage(GetComponent<Enemy>().GetDamageAmount());
                //Debug.Log("attacked");
                if (closestPlayer.gameObject.GetComponent<PlayerData>().health < 200)
                    Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            ToggleInvisible();
        }
    }

    private void ToggleInvisible()
    {
        invisible = !invisible;
        GetComponent<MeshRenderer>().enabled = invisible;
    }

    public bool PlayerStillInRange(Vector3 direction)
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

    public void ToggleOnScreen()
    {
        onScreen = !onScreen;
    }
}
