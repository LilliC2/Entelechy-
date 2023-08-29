using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineTrap : GameBehaviour
{
    public float explosionRadius = 5f;
    public int damageAmount = 50;
    public float explosionDelay = 1f;

    public Color triggeredColor = Color.red;

    private bool exploded = false;
    private SpriteRenderer trapRenderer;

    private void Start()
    {
        trapRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!exploded)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                Explode(other.gameObject);
            }
        }
    }

    private void Explode(GameObject target)
    {
        exploded = true;
        StartCoroutine(ExplodeAfterDelay(target));

        trapRenderer.color = triggeredColor;
    }

    private IEnumerator ExplodeAfterDelay(GameObject target)
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitCollider in colliders)
        {
            if (hitCollider.CompareTag("Player") || hitCollider.CompareTag("Enemy"))
            {
                if (hitCollider.gameObject == target)
                {
                    if (target.CompareTag("Player"))
                    {
                        //ensures that it doesn't include hit colliders in calcuation
                        if(target.name.Contains("Player"))
                        {
                            _PC.health -= damageAmount;

                        }

                    }
                    else if (target.CompareTag("Enemy"))
                    {
                        EnemyStats enemyStats = target.GetComponent<EnemyStats>();
                        if (enemyStats != null)
                        {
                            enemyStats.health -= damageAmount;
                        }
                    }
                }
            }
        }


        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}