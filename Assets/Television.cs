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
    bool isTelevisionOccupied;

    Vector2 lineStart;
    Vector2 lineEnd;
    float range = 10f;

    private void Start()
    {
        chairBoxCollider.GetComponent<Chair>().AttachTelevision(this);
        lineStart = new Vector2(transform.position.x - range, transform.position.y);
        lineEnd = new Vector2(transform.position.x + range, transform.position.y);
    }

    private void Update()
    {
        if (isTelevisionOn && !isTelevisionOccupied)
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
            if(raycastHit.collider.GetComponent<Parent>().NoticeTelevision(transform, chairBoxCollider.transform))
            {
                chairBoxCollider.enabled = true;
            }
        }
    }

    public void SetIsTelevisionOccupied(bool isTelevisionOccupied)
    {
        this.isTelevisionOccupied = isTelevisionOccupied;
    }
}
