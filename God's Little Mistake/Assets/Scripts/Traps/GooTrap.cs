using UnityEngine;
using System.Collections;

public class GooTrap : MonoBehaviour
{
    public float gooSlowdownFactor = 0.5f; // The factor by which the player's speed will be reduced when in goo.
    public float damageOverTime = 10f; // The amount of damage the player takes per second when in goo.
    public float minSpeedInGoo = 1f; // The minimum speed the player can have while in the goo.

    private bool isInGoo = false;
    private PlayerController playerController;
    private float originalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the goo trap area.
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Apply the goo effect on the player.
                isInGoo = true;
                originalSpeed = playerController.speed;
                playerController.StartCoroutine(GooEffectCoroutine());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the goo trap area.
            isInGoo = false;
        }
    }

    private System.Collections.IEnumerator GooEffectCoroutine()
    {
        while (isInGoo)
        {
            // Reduce player's speed when in goo.
            playerController.speed *= gooSlowdownFactor;

            // Cap the speed to the minimum value.
            playerController.speed = Mathf.Max(playerController.speed, minSpeedInGoo);

            // Deal damage over time to the player.
            playerController.health -= damageOverTime * Time.deltaTime;

            // Check if player health is zero or below, which means the player is dead.
            if (playerController.health <= 0f)
            {
                // Perform any actions needed when the player dies in goo (e.g., respawn, game over, etc.).
                Debug.Log("Player died in the GooTrap!");
                break; // Exit the loop if the player is dead.
            }

            yield return null;
        }

        // Restore the original speed when the player exits the goo trap area.
        playerController.speed = 4f;
    }
}