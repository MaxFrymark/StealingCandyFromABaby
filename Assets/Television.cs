using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Interactable
{
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D televisionBoxCollider;
    [SerializeField] BoxCollider2D chairBoxCollider;
    [SerializeField] LayerMask targetLayer;
    bool isTelevisionOn;

    Vector2 lineStart;
    Vector2 lineEnd;
    float range = 2f;

    private void Start()
    {
        lineStart = new Vector2(transform.position.x - range, transform.position.y);
        lineEnd = new Vector2(transform.position.x + range, transform.position.y);
    }

    private void Update()
    {
        if (isTelevisionOn)
        {
            DistractParent();
        }
    }

    public override void Interact()
    {
        TurnOnTV();
    }

    private void TurnOnTV()
    {
        animator.enabled = true;
        isTelevisionOn = true;
        televisionBoxCollider.enabled = false;
    }

    private void DistractParent()
    {
        RaycastHit2D raycastHit = Physics2D.Linecast(lineStart, lineEnd, targetLayer);
        if (raycastHit)
        {
            Debug.Log("hi");
            if(raycastHit.collider.GetComponent<Parent>().NoticeTelevision(transform, chairBoxCollider.transform))
            {
                chairBoxCollider.enabled = true;

            }
        }
    }
}
