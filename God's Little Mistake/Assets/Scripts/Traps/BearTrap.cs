using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : GameBehaviour
{
    public float damageOnCollision = 5f;
    public Sprite triggeredSprite;
    private bool isTrapActive = true;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ExecuteAfterSeconds(6f, () => Destroy(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrapActive && collision.gameObject.CompareTag("Enemy"))
        {
            isTrapActive = false;
            var baseEn = collision.gameObject.GetComponent<BaseEnemy>();

            baseEn.Hit(damageOnCollision);
            var speed = baseEn.stats.speed;
            baseEn.stats.speed = 0;
            ExecuteAfterSeconds(2f, () => baseEn.stats.speed = speed);
            ExecuteAfterSeconds(2f, () => Destroy(gameObject));



            spriteRenderer.sprite = triggeredSprite;

        }

    }
}