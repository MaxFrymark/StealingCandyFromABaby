using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Parent parent = collision.GetComponent<Parent>();
        if (parent.GetMovingToChair())
        {
            parent.SitDown(transform);
        }
    }
}
