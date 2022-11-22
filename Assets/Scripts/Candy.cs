using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    [SerializeField] GameObject levelExit;
    
    public override void UseItem()
    {
        return;
    }


    public override void Interact()
    {
        base.Interact();
        levelExit.SetActive(true);
        FindObjectOfType<Baby>().OnCandyStolen();
    }
}
