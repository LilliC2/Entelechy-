using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderScript : MonoBehaviour
{
    SpriteRenderer spriteRender;

    [SerializeField]
    float timeBetweenFade;

    [SerializeField]
    float fadeStrength;

    float alpha = 1;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        InvokeRepeating("Fade", 0, timeBetweenFade);
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha < 0) Destroy(gameObject);


    }

    void Fade()
    {
        alpha -= fadeStrength;
        Color newColor = new Color(1f, 1, 1f, alpha);
        spriteRender.color = newColor;

    }
}
