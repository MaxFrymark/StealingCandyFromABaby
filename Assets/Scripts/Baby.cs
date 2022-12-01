using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : Resident
{
    [SerializeField] AudioClip cry;

    public void OnCandyStolen()
    {
        if(isTargetSeen && !isDistracted)
        {
            animator.SetTrigger("isCrying");
            AudioSource.PlayClipAtPoint(cry, Camera.main.transform.position);
            AlertClosestParent();
        }
    }

    private void AlertClosestParent()
    {
        Parent[] parents = FindObjectsOfType<Parent>();
        Parent closestParent = null;
        foreach(Parent parent in parents)
        {
            if(closestParent == null)
            {
                closestParent = parent;
            }
            else
            {
                if(Vector2.Distance(transform.position, parent.transform.position) < Vector2.Distance(transform.position, closestParent.transform.position))
                {
                    closestParent = parent;
                }
            }
        }
        closestParent.HeardBabyCry(transform);
    }

    public void HearRattle(Transform rattle)
    {
        transform.localScale = new Vector2(Mathf.Sign(rattle.transform.position.x - transform.position.x), 1);
    }
}
