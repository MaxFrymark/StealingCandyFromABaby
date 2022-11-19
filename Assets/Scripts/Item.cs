using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Interactable
{
    [SerializeField] protected BoxCollider2D boxCollider;
    [SerializeField] bool isItemHiden;

    private void Start()
    {
        if (isItemHiden)
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

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
