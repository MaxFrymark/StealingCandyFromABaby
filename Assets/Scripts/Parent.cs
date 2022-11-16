using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Resident
{
    private enum ParentBehavior { Idle, Patrolling, Chasing, Shouting }
    ParentBehavior currentBehavior = ParentBehavior.Idle;

    float runSpeed = 6;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;

    protected override void Start()
    {
        if(waypoints.Length > 0)
        {
            SetToPatrolling();
        }
        base.Start();
    }

    protected override void Update()
    {
        if(currentBehavior == ParentBehavior.Patrolling)
        {
            CheckIfAtWaypoint();
        }
        
        base.Update();
    }

    private void CheckIfAtWaypoint()
    {
        if(Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    protected override void WatchForTarget()
    {
        base.WatchForTarget();
        if (isTargetSeen)
        {
            PlayerSpotted();
        }
    }

    public void HeardBabyCry(Transform babyTransform)
    {
        SetToChasing((int)Mathf.Sign(babyTransform.position.x - transform.position.x));
    }

    private void PlayerSpotted()
    {
        if (currentBehavior != ParentBehavior.Chasing && currentBehavior != ParentBehavior.Shouting)
        {
            SetToChasing((int)transform.localScale.x);
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

    private void SetToChasing(int direction)
    {
        Move(direction);
        currentBehavior = ParentBehavior.Chasing;
        currentMoveSpeed = runSpeed;
    }

    private void SetToPatrolling()
    {
        currentBehavior = ParentBehavior.Patrolling;
        currentMoveSpeed = walkSpeed;
        Move((int)Mathf.Sign(waypoints[0].position.x - transform.position.x));
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
        Move((int)Mathf.Sign(waypoints[currentWaypoint].position.x - transform.position.x));
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
}
