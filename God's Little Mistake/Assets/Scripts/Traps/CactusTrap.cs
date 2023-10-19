using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusTrap : GameBehaviour
{
    PlayerController playerController;
    public float damageOnCollision = 2f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.5f;
    public Sprite trapActivatedSprite;

    private CharacterController characterController;
    private Vector3 knockbackDirection;
    private bool isKnockbackActive = false;
    private float knockbackStartTime;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;

    AudioSource audioSource;
    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        audioSource = GetComponent<AudioSource>();

        if (playerObject != null)
        {
            playerController = _PC;
            characterController = _PC.controller;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        if (isKnockbackActive)
        {
            float timeSinceKnockback = Time.time - knockbackStartTime;

            if (timeSinceKnockback >= knockbackDuration)
            {
                isKnockbackActive = false;
                StartCoroutine(RevertSpriteAfterDelay(0.5f));
            }
            else
            {
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
            //play sound
            audioSource.Play();

            playerController.health -= damageOnCollision;
            knockbackDirection = collision.gameObject.transform.position - transform.position;
            knockbackDirection.y = 0f;
            knockbackDirection.Normalize();
            isKnockbackActive = true;
            knockbackStartTime = Time.time;
            spriteRenderer.sprite = trapActivatedSprite;
        }
    }

    private IEnumerator RevertSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = originalSprite;
    }
}