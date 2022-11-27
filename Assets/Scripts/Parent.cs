using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Parent : Resident
{
    private enum ParentBehavior { Idle, Patrolling, Chasing, Shouting, MovingToDestination }
    ParentBehavior currentBehavior = ParentBehavior.Idle;

    Transform pointOfOrigin;

    [SerializeField] float runSpeed;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;
    private Transform destination;

    bool changingFloors = false;
    bool movingToChair = false;
    WineGlass wineGlass = null;

    Player player;

    protected override void Start()
    {
        pointOfOrigin = Instantiate(new GameObject(), transform.position, Quaternion.identity).transform;
        
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
            case ParentBehavior.MovingToDestination:
                CheckIfAtMovingDestination();
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

    private void CheckIfAtMovingDestination()
    {

        if (Mathf.Abs(transform.position.x - destination.position.x) < 0.1f)
        {

            if (wineGlass != null && destination == wineGlass.transform)
            {
                Debug.Log("hi");

                DrinkWine();
            }

            else if (destination == pointOfOrigin)
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
                    SetToMovingToDestination(pointOfOrigin);
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
        player.EndGame();
        Stop();
    }

    private void SetToIdle()
    {
        isDistracted = false;
        currentBehavior = ParentBehavior.Idle;
        Stop();
    }

    private void SetToChasing(Transform position)
    {
        isDistracted = false;
        currentBehavior = ParentBehavior.Chasing;
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
        currentMoveSpeed = runSpeed;
        HandleParentMove(position);
    }

    private void SetToPatrolling()
    {
        isDistracted = false;
        currentBehavior = ParentBehavior.Patrolling;
        currentMoveSpeed = walkSpeed;
        destination = waypoints[0];
        HandleParentMove(waypoints[0]);
    }

    private void SetToMovingToDestination(Transform destination)
    {
        isDistracted = false;
        currentBehavior = ParentBehavior.MovingToDestination;
        currentMoveSpeed = walkSpeed;
        this.destination = destination;
        HandleParentMove(destination);
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

        animator.SetBool("isWalking", currentBehavior == ParentBehavior.Patrolling | currentBehavior == ParentBehavior.MovingToDestination);
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
            Debug.Log("hi");

            if (Mathf.Abs(transform.position.y - stairwayDoor.transform.position.y) < 0.25)
            {
                Debug.Log("meow");
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
                    SetToMovingToDestination(targetChair);
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
    }

    public bool GetIsTargetSeen()
    {
        return isTargetSeen;
    }

    public void GetWine(WineGlass wine)
    {
        if (wineGlass == null)
        {
        
            if (currentBehavior == ParentBehavior.Idle || currentBehavior == ParentBehavior.Patrolling)
            {
                if (Mathf.Sign(transform.position.x - wine.transform.position.x) != transform.localScale.x)
                {

                    wineGlass = wine;
                    SetToMovingToDestination(wine.transform);
                }

            }
        }
    }

    private void DrinkWine()
    {
        SetToIdle();
        wineGlass.gameObject.SetActive(false);
        animator.SetTrigger("isDrinking");
        isDistracted = true;
    }

    public void StopDrinking()
    {
        wineGlass = null;
        if(waypoints.Length > 0)
        {
            SetToPatrolling();
        }
        else
        {
            SetToMovingToDestination(pointOfOrigin);
        }
    }
}
