using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineGlass : Item
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask wallLayer;

    bool isGlassActive = false;
    

    private void Update()
    {
        if (isGlassActive)
        {
            DistractParent(-1);
            DistractParent(1);
        }
    }

    public override void Interact()
    {
        base.Interact();
        isGlassActive = false;
    }

    public override void UseItem()
    {
        PlaceWineGlass();
    }

    private void PlaceWineGlass()
    {
        isGlassActive = true;
        Player player = FindObjectOfType<Player>();
        player.RemoveItem(this);
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.2f);
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
    }

    private void DistractParent(int direction)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 10f, wallLayer | targetLayer);
        Debug.DrawLine(transform.position, hit2D.point, Color.green, 0.1f);

        if (hit2D)
        {
            if (LayerMask.GetMask(LayerMask.LayerToName(hit2D.collider.gameObject.layer)) == targetLayer)
            {
                hit2D.collider.GetComponent<Parent>().GetWine(this);
            }
        }
    }
}
