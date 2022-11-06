using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Resident
{
    [SerializeField] bool isRunning = false;
    float runSpeed = 8;
    bool isChasingPlayer;


    protected override void Update()
    {
        if (isRunning)
        {
            playerRigidBody.velocity = new Vector2(direction * runSpeed, 0);
        }
        
        base.Update();
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
        direction = (int)Mathf.Sign(babyTransform.position.x - transform.position.x);
        isRunning = true;
        animator.SetBool("isRunning", true);
    }

    private void PlayerSpotted()
    {
        if (!isChasingPlayer)
        {
            isChasingPlayer = true;
            direction = (int)transform.localScale.x;
            isRunning = true;
            animator.SetBool("isRunning", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTargetSeen && collision.GetComponent<Player>())
        {
            Stop();
            animator.SetTrigger("isShouting");
        }
    }

    public override void Stop()
    {
        isRunning = false;
        base.Stop();
    }
}
