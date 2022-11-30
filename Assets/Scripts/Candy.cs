using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    [SerializeField] GameObject levelExit;

    protected override void Start()
    {
        description = "You have the candy now get out!";
        base.Start();
    }

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
