using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : Interactable
{
    [SerializeField] Sprite hiddenSprite;
    bool hasHiddenPlayer = false;

    public override void Interact()
    {
        if (!hasHiddenPlayer)
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
        hasHiddenPlayer = true;
        spriteRenderer.sprite = hiddenSprite;
    }

    private void RevealPlayer()
    {
        hasHiddenPlayer = false;
        spriteRenderer.sprite = baseSprite;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (!hasHiddenPlayer)

        {
            base.OnTriggerExit2D(collision);
        }
    }
}
