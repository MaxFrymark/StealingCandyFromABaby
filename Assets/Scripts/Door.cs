using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    
    [SerializeField] protected Sprite closedDoor;
    [SerializeField] Sprite closedDoorHighlighted;

    [SerializeField] protected BoxCollider2D doorCollider;

    [SerializeField] protected AudioClip doorSoundEffect;
    protected bool doorOpen;
    
    public override void Interact()
    {
        if (!doorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().AddInteractableToList(this);
            HighlightInteractable();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().RemoveInteractableFromList(this);
            RemoveHighlight();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.GetComponent<Parent>())
        {
            OpenDoor();
        }
    }

    public override void HighlightInteractable()
    {
        if (doorOpen)
        {
            spriteRenderer.sprite = highlightedSprite;
        }
        else
        {
            spriteRenderer.sprite = closedDoorHighlighted;
        }
    }

    public override void RemoveHighlight()
    {
        if (doorOpen)
        {
            spriteRenderer.sprite = baseSprite;
        }

        else
        {
            spriteRenderer.sprite = closedDoor;
        }
    }

    protected virtual void OpenDoor()
    {
        AudioSource.PlayClipAtPoint(doorSoundEffect, Camera.main.transform.position);
        doorOpen = true;
        spriteRenderer.sprite = baseSprite;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        doorCollider.isTrigger = true;
    }

    protected virtual void CloseDoor()
    {
        AudioSource.PlayClipAtPoint(doorSoundEffect, Camera.main.transform.position);
        doorOpen = false;
        gameObject.layer = LayerMask.NameToLayer("Walls");
        doorCollider.isTrigger = false;
    }
}
