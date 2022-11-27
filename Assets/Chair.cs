using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    //Television attachedTelevision;
    Parent parentInChair;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (parentInChair == null)
        {
            Parent parent = collision.GetComponent<Parent>();
            if (parent.GetMovingToChair())
            {
                //attachedTelevision.SetIsTelevisionOccupied(true);
                parent.SitDown(transform);
                parentInChair = parent;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == parentInChair.gameObject)
        {
            //attachedTelevision.SetIsTelevisionOccupied(false);
            parentInChair = null;
        }
    }

    public void AttachTelevision(Television television)
    {
        //attachedTelevision = television;
    }
}
