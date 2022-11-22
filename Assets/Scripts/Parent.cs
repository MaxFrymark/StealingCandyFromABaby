using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parent : Resident
{
    private enum ParentBehavior { Idle, Patrolling, Chasing, Shouting }
    ParentBehavior currentBehavior = ParentBehavior.Idle;

    float runSpeed = 6;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;
    private Transform destination;

    bool changingFloors = false;
    bool movingToChair = false;

    Player player;

    protected override void Start()
    {
        if(waypoints.Length > 0)
        {
            SetToPatrolling();
        }
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        switch (currentBehavior)
        {
            case ParentBehavior.Patrolling:
                CheckIfAtWaypoint();
                break;
            case ParentBehavior.Chasing:
                CheckIfAtChasingDestination();
                break;
        }
        if (isMoving)
        {
            playerRigidBody.velocity = new Vector2(direction * currentMoveSpeed, 0);
        }
        WatchForTarget();
        HandleWalkingAnimation();
    }

    private void CheckIfAtWaypoint()
    {
        if(Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    private void CheckIfAtChasingDestination()
    {
        if (Vector2.Distance(transform.position,destination.position) < 0.1f)
        {
            if (isTargetSeen)
            {
                WatchForTarget();
            }
            else
            {
                if(waypoints.Length > 0)
                {
                    SetToPatrolling();
                }
                else
                {
                    SetToIdle();
                }
            }
        }
    }

    protected override void WatchForTarget()
    {
        base.WatchForTarget();



        if (isTargetSeen)
        {
            PlayerSpotted();
        }
        else
        {
            isTargetSeen = false;
        }
    }

    public void HeardBabyCry(Transform babyTransform)
    {
        destination = babyTransform;
        SetToChasing(babyTransform);
    }

    private void PlayerSpotted()
    {
        if (currentBehavior != ParentBehavior.Shouting)
        {
            destination = player.transform;
            SetToChasing(player.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTargetSeen && collision.GetComponent<Player>())
        {
            SetToShouting();
        }
    }

    private void SetToShouting()
    {
        currentBehavior = ParentBehavior.Shouting;
        animator.SetTrigger("isShouting");
        Stop();
    }

    private void SetToIdle()
    {
        currentBehavior = ParentBehavior.Idle;
        Stop();
    }

    private void SetToChasing(Transform position)
    {
        currentBehavior = ParentBehavior.Chasing;
        currentMoveSpeed = runSpeed;
        HandleParentMove(position);
    }

    private void SetToPatrolling()
    {
        currentBehavior = ParentBehavior.Patrolling;
        currentMoveSpeed = walkSpeed;
        destination = waypoints[0];
        HandleParentMove(waypoints[0]);
    }

    private void GetNextWaypoint()
    {
        if(currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
        destination = waypoints[currentWaypoint];
        HandleParentMove(waypoints[currentWaypoint]);
    }

    protected override void HandleWalkingAnimation()
    {

        animator.SetBool("isWalking", currentBehavior == ParentBehavior.Patrolling);
        animator.SetBool("isRunning", currentBehavior == ParentBehavior.Chasing);

        if (playerRigidBody.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (playerRigidBody.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private bool CheckDestinationLevel(Transform position)
    {
        Debug.Log("meow");

        if (Mathf.Abs(transform.position.y - position.position.y) < 0.25)
        {
            return true;
        }
        else
        {
            HandleParentMove(FindStairway());
            return false;
        }
    }

    private Transform FindStairway()
    {
        Transform targetStairway = null;
        foreach(StairwayDoor stairwayDoor in FindObjectsOfType<StairwayDoor>())
        {
            if(stairwayDoor.transform.position.y == transform.position.y)
            {
                if(stairwayDoor.GetStairwayDirection() == destination.transform.position.y > transform.position.y)
                {
                    changingFloors = true;
                    targetStairway = stairwayDoor.transform;
                }
            }
        }
        return targetStairway;
    }

    private void HandleParentMove(Transform position)
    {
        if (CheckDestinationLevel(position))
        {
            Move(GetDirection(position));
        }
    }

    private int GetDirection(Transform position)
    {
        return (int)Mathf.Sign(position.position.x - transform.position.x);
    }

    public bool GetChangingFloors()
    {
        return changingFloors;
    }

    public override void StartMoveBetweenFloors()
    {
        gameObject.SetActive(false);
    }

    public override void EndMoveBetweenFloors()
    {
        gameObject.SetActive(true);
        changingFloors = false;
        HandleParentMove(destination);
    }

    public bool NoticeTelevision(Transform television, Transform targetChair)
    {
        if (Mathf.Sign(transform.position.x - television.position.x) != transform.localScale.x)
        {
            if (currentBehavior == ParentBehavior.Idle || currentBehavior == ParentBehavior.Patrolling)
            {
                if (!movingToChair)
                {
                    SetToPatrolling();
                    HandleParentMove(targetChair);
                    movingToChair = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool  GetMovingToChair()
    {
        return movingToChair;
    }

    public void SitDown(Transform sitPosition)
    {
        SetToIdle();
        animator.SetTrigger("isSitting");
        transform.localScale = sitPosition.localScale;
        transform.position = sitPosition.position;
        isDistracted = true;
        //movingToChair = false;
    }
}
