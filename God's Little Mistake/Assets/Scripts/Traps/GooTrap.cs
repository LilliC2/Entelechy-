using UnityEngine;
using System.Collections;

public class GooTrap : MonoBehaviour
{
    public float gooSlowdownFactor = 0.5f;
    public float damageOverTime = 10f;
    public float minSpeedInGoo = 1f;

    private bool isInGoo = false;
    private PlayerController playerController;
    private float originalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
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
            isInGoo = false;
        }
    }

    private IEnumerator GooEffectCoroutine()
    {
        while (isInGoo)
        {
            playerController.speed *= gooSlowdownFactor;

            playerController.speed = Mathf.Max(playerController.speed, minSpeedInGoo);

            playerController.health -= damageOverTime * Time.deltaTime;

            if (playerController.health <= 0f)
            {
                Debug.Log("Player died in the GooTrap!");
                break;
            }

            yield return null;
        }

        playerController.speed = 4f;
    }
}