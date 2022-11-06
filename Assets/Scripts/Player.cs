using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable currentInteractable;

    protected override void Update()
    {
        base.Update();
        if(interactables.Count > 0)
        {
            SetCurrentInteractable();
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
        }
    }
}
