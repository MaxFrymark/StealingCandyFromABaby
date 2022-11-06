using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D playerRigidBody;
    [SerializeField] protected Animator animator;
    [SerializeField] float walkSpeed;

    protected int direction;
    bool isMoving;

    protected virtual void Update()
    {
        if (isMoving)
        {
            playerRigidBody.velocity = new Vector2(direction * walkSpeed, 0);
        }
        HandleWalkingAnimation();
    }

    public void Move(int direction)
    {
        isMoving = true;
        this.direction = direction;
    }

    public virtual void Stop()
    {
        isMoving = false;
        playerRigidBody.velocity = Vector2.zero;
    }

    private void HandleWalkingAnimation()
    {
        animator.SetBool("isWalking", isMoving);

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
