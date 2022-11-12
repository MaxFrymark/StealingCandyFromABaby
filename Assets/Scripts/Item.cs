using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Interactable
{
    [SerializeField] protected BoxCollider2D boxCollider;

    public abstract void UseItem();

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
