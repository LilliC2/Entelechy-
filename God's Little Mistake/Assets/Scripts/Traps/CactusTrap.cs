using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusTrap : MonoBehaviour
{
    public PlayerController playerController;
    public float damageOnCollision = 2f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.5f;

    private CharacterController characterController;
    private Vector3 knockbackDirection;
    private bool isKnockbackActive = false;
    private float knockbackStartTime;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            characterController = playerObject.GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (isKnockbackActive)
        {
            // Calculate time since the knockback started
            float timeSinceKnockback = Time.time - knockbackStartTime;

            // Check if knockback duration has passed
            if (timeSinceKnockback >= knockbackDuration)
            {
                isKnockbackActive = false;
            }
            else
            {
                // Apply knockback over time using Move
                float knockbackProgress = timeSinceKnockback / knockbackDuration;
                Vector3 currentKnockbackForce = Vector3.Lerp(knockbackDirection * knockbackForce, Vector3.zero, knockbackProgress);
                characterController.Move(currentKnockbackForce * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            playerController.health -= damageOnCollision;

            // Calculate knockback direction
            knockbackDirection = collision.gameObject.transform.position - transform.position;
            knockbackDirection.y = 0f; // Keep the knockback in the horizontal plane
            knockbackDirection.Normalize();

            // Activate knockback
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
        }
    }
}