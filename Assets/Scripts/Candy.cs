using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Item
{
    private void OnDisable()
    {
        FindObjectOfType<Baby>().OnCandyStolen();
    }
}
