using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable currentInteractable;

    private bool isHidden;

    private Item[] inventory = new Item[5];
    Item activeItem;

    protected override void Update()
    {
        if (!isHidden)
        {
            base.Update();
            if (interactables.Count > 0)
            {
                SetCurrentInteractable();
            }
        }
    }

    private void SetCurrentInteractable()
    {
        if(interactables.Count == 1)
        {
            currentInteractable = interactables[0];
            currentInteractable.HighlightInteractable();
        }

        else if(interactables.Count > 1)
        {
            currentInteractable = FindClosestInteracable();
            currentInteractable.HighlightInteractable();
        }
    }



    private Interactable FindClosestInteracable()
    {
        Interactable closestInteractable = null;
        foreach(Interactable interactable in interactables)
        {
            if (interactable.gameObject.activeInHierarchy)
            {
                if (closestInteractable == null)
                {
                    closestInteractable = interactable;
                }
                else
                {
                    if (Vector2.Distance(transform.position, interactable.transform.position) < Vector2.Distance(transform.position, closestInteractable.transform.position))
                    {
                        closestInteractable.RemoveHighlight();
                        closestInteractable = interactable;
                    }
                }
            }
        }

        return closestInteractable;
    }

    public void AddInteractableToList(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractableFromList(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    public void Interact()
    {
        if(currentInteractable != null && currentInteractable.gameObject.activeInHierarchy)
        {
            currentInteractable.Interact();

            switch (currentInteractable)
            {
                case HidingPlace:
                    HandleHiding();
                    break;
                case Item:
                    PickUp();
                    break;
            }
        }
    }

    private void PickUp()
    {
        Item newItem = currentInteractable as Item;
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                Inventory.Instance.AssignItemToSlot(newItem);
                currentInteractable = null;
                if(activeItem == null)
                {
                    activeItem = inventory[i];
                    Inventory.Instance.SelectItem(i);
                }
                return;
            }
        }

        
    }

    private void HandleHiding()
    {
        if (!isHidden)
        {
            HidePlayer();
        }

        else
        {
            RevealPlayer();
        }
    }

    private void HidePlayer()
    {
        interactables.Clear();
        isHidden = true;
        playerRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    private void RevealPlayer()
    {
        isHidden = false;
        playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    public void SelectItem(int itemIndex)
    {
        if (inventory[itemIndex] != null)
        {
            activeItem = inventory[itemIndex];
            Inventory.Instance.SelectItem(itemIndex);
        }
    }
}