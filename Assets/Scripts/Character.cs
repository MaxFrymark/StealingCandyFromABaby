using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator animator;
    [SerializeField] float playerSpeed;

    protected virtual void Update()
    {
        HandleWalkingAnimation();
    }

    public void Move(int direction)
    {
        playerRigidBody.velocity = new Vector2(direction * playerSpeed, 0);
    }

    public void Stop()
    {
        playerRigidBody.velocity = Vector2.zero;
    }

    private void HandleWalkingAnimation()
    {
        if (playerRigidBody.velocity != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            if (playerRigidBody.velocity.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
