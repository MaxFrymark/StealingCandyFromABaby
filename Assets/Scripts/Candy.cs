using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    
    public override void Interact()
    {
        base.Interact();
        FindObjectOfType<Baby>().OnCandyStolen();
    }
}
