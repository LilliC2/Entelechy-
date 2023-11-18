using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlantProjectile : MonoBehaviour
{
    public float damageAmount = 7;
    public float destroyDelay = 3f;

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.health -= damageAmount;
            }

            Destroy(gameObject);
        }
    }
}