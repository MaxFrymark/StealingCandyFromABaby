using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite highlightedSprite;

    public void HighlightInteractable()
    {
        spriteRenderer.sprite = highlightedSprite;
    }

    public void RemoveHighlight()
    {
        spriteRenderer.sprite = baseSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>().AddInteractableToList(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveHighlight();
        collision.GetComponent<Player>().RemoveInteractableFromList(this);
    }
}
