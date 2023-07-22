using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusTrap : MonoBehaviour
{
    public PlayerController playerController;
    public float damageOnCollision = 2f;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.health -= damageOnCollision;
        }
    }
}