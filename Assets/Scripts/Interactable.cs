using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    
    [SerializeField] protected Sprite baseSprite;
    [SerializeField] protected Sprite highlightedSprite;

    public virtual void HighlightInteractable()
    {
        spriteRenderer.sprite = highlightedSprite;
    }

    public virtual void RemoveHighlight()
    {
        spriteRenderer.sprite = baseSprite;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().AddInteractableToList(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            RemoveHighlight();
            collision.GetComponent<Player>().RemoveInteractableFromList(this);
        }
    }

    public abstract void Interact();
}
