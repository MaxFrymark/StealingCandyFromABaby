using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Resident : Character
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask targetLayer;

    protected bool isDistracted = false;
    protected bool isTargetSeen;

    protected override void Update()
    {
        base.Update();
        if (!isDistracted)
        {
            WatchForTarget();
        }
    }

    protected virtual void WatchForTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 10f, targetLayer | wallLayer);
        isTargetSeen = LayerMask.GetMask(LayerMask.LayerToName(hit.collider.gameObject.layer)) == targetLayer;
    }
}
