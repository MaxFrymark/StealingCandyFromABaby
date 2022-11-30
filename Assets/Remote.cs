using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : Item
{
    protected override void Start()
    {
        description = "Remote: Turns on nearest TV.";
        base.Start();
    }

    public override void UseItem()
    {
        TurnOnNearestTV();
    }

    private void TurnOnNearestTV()
    {
        Television nearestTelevision = null;
        foreach(Television television in FindObjectsOfType<Television>())
        {
            if(nearestTelevision == null)
            {
                nearestTelevision = television;
            }
            else if(Vector2.Distance(transform.position, television.transform.position) < Vector2.Distance(transform.position, nearestTelevision.transform.position))
            {
                nearestTelevision = television;
            }
        }

        nearestTelevision.Interact();
        FindObjectOfType<Player>().RemoveItem(this);
        gameObject.SetActive(false);
    }
}
