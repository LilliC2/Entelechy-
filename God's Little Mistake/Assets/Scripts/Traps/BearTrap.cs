using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public PlayerController playerController;

    public float damageOnCollision = 5f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.speed = 0f;

            playerController.health -= damageOnCollision;
            StartCoroutine(ResetSpeed(2f));
        }
    }

    private IEnumerator ResetSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerController.speed = 4f;
        Destroy(gameObject);
    }
}
