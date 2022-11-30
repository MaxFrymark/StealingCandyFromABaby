using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rattle : Item
{
    int defaultLayer = 7;
    int thrownLayer = 11;
    [SerializeField] Rigidbody2D body;
    [SerializeField] AudioClip soundEffect;

    private float throwSpeed = 4f;
    private bool isThrown;

    protected override void Start()
    {
        description = "Rattle: Throw to make the baby look the other way.";
        base.Start();
    }


    public override void UseItem()
    {
        isThrown = true;
        Player player = FindObjectOfType<Player>();
        transform.position = player.transform.position;
        player.RemoveItem(this);
        gameObject.layer = thrownLayer;
        boxCollider.isTrigger = false;
        boxCollider.enabled = true;
        spriteRenderer.enabled = true;
        body.bodyType = RigidbodyType2D.Dynamic;
        body.velocity = new Vector2(player.transform.localScale.x * throwSpeed, 2);
        StartCoroutine(ResetItemAfterThrow());
    }

    private IEnumerator ResetItemAfterThrow()
    {
        yield return new WaitForSeconds(1.5f);
        body.bodyType = RigidbodyType2D.Static;
        boxCollider.isTrigger = true;
        gameObject.layer = defaultLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isThrown)
        {
            RattleEffect();
        }
    }

    private void RattleEffect()
    {
        Baby baby = FindObjectOfType<Baby>();
        AudioSource.PlayClipAtPoint(soundEffect, Camera.main.transform.position);
        if(Vector2.Distance(transform.position, baby.transform.position) < 3)
        {
            baby.HearRattle(transform);
        }
    }
}
