using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] GameObject gameOverScreen;
    
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable currentInteractable;

    private bool isHidden;

    private Item[] inventory = new Item[5];
    Item activeItem;

    Parent[] parents;
    bool hasAlreadyWon = false;

    bool isMovingBetweenFloors = false;

    protected override void Start()
    {
        base.Start();
        parents = FindObjectsOfType<Parent>();
    }

    protected override void Update()
    {
        if (!isHidden)
        {
            base.Update();
            if (interactables.Count > 0)
            {
                SetCurrentInteractable();
            }
            else
            {
                currentInteractable = null;
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
                    if (transform.position.x - interactable.transform.position.x < transform.position.x - closestInteractable.transform.position.x)
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
        if (!isHidden && !IsPlayerWatched())
        {
            AttemptToPickUpItem();
            HidePlayer();
        }

        else
        {
            RevealPlayer();
        }
    }

    private bool IsPlayerWatched()
    {
        foreach(Parent parent in parents)
        {
            if (parent.GetIsTargetSeen()) { return true; }
        }
        return false;
    }

    private void AttemptToPickUpItem()
    {
        HidingPlace hidingPlace = currentInteractable as HidingPlace;
        if(hidingPlace.GetHiddenItem() != null)
        {
            currentInteractable = hidingPlace.GetHiddenItem();
            PickUp();
            hidingPlace.RemoveHiddenItem();
            currentInteractable = hidingPlace;
        }
    }

    private void HidePlayer()
    {
        transform.position = currentInteractable.transform.position;
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

    public override void StartMoveBetweenFloors()
    {
        isMovingBetweenFloors = true;
        HidePlayer();
    }

    public override void EndMoveBetweenFloors()
    {
        isMovingBetweenFloors = false;
        RevealPlayer();
    }

    public bool GetIsMovingBetweenFloors()
    {
        return isMovingBetweenFloors;
    }

    public void SelectItem(int itemIndex)
    {
        if (inventory[itemIndex] != null)
        {
            activeItem = inventory[itemIndex];
            Inventory.Instance.SelectItem(itemIndex);
        }
    }

    public void UseActiveItem()
    {
        if(activeItem != null)
        {
            activeItem.UseItem();
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        if(itemToRemove == activeItem)
        {
            activeItem = null;
        }

        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == itemToRemove)
            {
                inventory[i] = null;
                Inventory.Instance.RemoveItemFromSlot(i);
                return;
            }
        }
    }

    public void EndGame()
    {
        if (!hasAlreadyWon)
        {
            Stop();
            gameOverScreen.SetActive(true);
        }
    }

    public void SetHasAlreadyWon()
    {
        hasAlreadyWon = true;
    }
}
