using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] BoxCollider2D boxCollider;
    
    

    public override void Interact()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    public Sprite GetSprite()
    {
        return baseSprite;
    }
}
