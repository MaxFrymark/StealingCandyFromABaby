using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Interactable
{
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D televisionBoxCollider;
    [SerializeField] BoxCollider2D chairBoxCollider;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask wallLayer;
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
            DistractParent(-1);
            DistractParent(1);
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

    private void DistractParent(int direction)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 10f, wallLayer | targetLayer);
        if (LayerMask.GetMask(LayerMask.LayerToName(hit2D.collider.gameObject.layer)) == targetLayer)
        {
            if (hit2D.collider.GetComponent<Parent>().NoticeTelevision(transform, chairBoxCollider.transform))
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
