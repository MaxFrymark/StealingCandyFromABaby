using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public void PickUp()
    {
        gameObject.SetActive(false);
    }

    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}
