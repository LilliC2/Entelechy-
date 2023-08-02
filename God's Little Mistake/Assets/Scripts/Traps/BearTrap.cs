using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public PlayerController playerController;
    public float damageOnCollision = 5f;
    public Sprite triggeredSprite;
    private bool isTrapActive = true;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrapActive && collision.gameObject.CompareTag("Player"))
        {
            isTrapActive = false;
            playerController.health -= damageOnCollision;

            spriteRenderer.sprite = triggeredSprite;

            playerController.GetComponent<CharacterController>().enabled = false;
            StartCoroutine(EnableMovementAfterDelay(2f));
        }
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerController.GetComponent<CharacterController>().enabled = true;
        Destroy(gameObject);
    }
}