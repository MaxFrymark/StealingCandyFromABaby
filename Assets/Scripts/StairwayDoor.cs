using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StairwayDoor : Door
{
    bool isStairwayUp;
    [SerializeField] StairwayDoor connectedDoor;

    Character characterReadyToChangeFloors;
    Character movingCharacter;

    private void Start()
    {
        isStairwayUp = transform.position.y < connectedDoor.transform.position.y;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        characterReadyToChangeFloors = collision.GetComponent<Character>();
        if(characterReadyToChangeFloors is Player)
        {
            base.OnTriggerEnter2D(collision);
        }
        else
        {
            Parent parent = characterReadyToChangeFloors as Parent;
            if (parent.GetChangingFloors())
            {
                OpenDoor();
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (characterReadyToChangeFloors is Player)
        {
            base.OnTriggerExit2D(collision);
        }
        characterReadyToChangeFloors = null;
    }

    protected override void OpenDoor()
    {
        AudioSource.PlayClipAtPoint(doorSoundEffect, Camera.main.transform.position);
        spriteRenderer.sprite = baseSprite;
        doorOpen = true;
        StartCoroutine(HandleMovingCharacter());
        doorCollider.enabled = false;

    }

    protected override void CloseDoor()
    {
        doorCollider.enabled = true;
        doorOpen = false;
    }

    protected IEnumerator HandleMovingCharacter()
    {
        if (characterReadyToChangeFloors != null)
        {
            movingCharacter = characterReadyToChangeFloors;

            characterReadyToChangeFloors.StartMoveBetweenFloors();
        }
        yield return new WaitForSeconds(0.5f);
        if (movingCharacter != null)
        {
            connectedDoor.Interact();
            movingCharacter.transform.position = connectedDoor.transform.position;
            movingCharacter.EndMoveBetweenFloors();
        }
        movingCharacter = null;
        spriteRenderer.sprite = closedDoor;
        CloseDoor();
    }

    public bool GetStairwayDirection()
    {
        return isStairwayUp;
    }
}
