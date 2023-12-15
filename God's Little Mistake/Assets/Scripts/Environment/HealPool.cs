using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPool : GameBehaviour
{

    public GameObject pool;
    public float healAmount;
    public float secondBetweenHeal;
    public float shrinkTime;
    public bool inRange;

    ParticleSystem healingEffect;
    AudioSource audiosource;

    private void Start()
    {
        StartCoroutine(ScaleOverTime(shrinkTime));
        StartCoroutine(HealPlayer());
        audiosource = GetComponent<AudioSource>();
        healingEffect = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = false;
            //how to add audio fade?
            healingEffect.Stop();

            audiosource.Stop();
            _UI.heathAnim.SetBool("IsHealing", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;

            healingEffect.Play();

            audiosource.Play();
            _UI.heathAnim.SetBool("IsHealing", true);
        }
    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = pool.transform.localScale;
        Vector3 destinationScale = new Vector3(0, 0, 0);

        float currentTime = 0.0f;

        do
        {
            pool.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        Destroy(gameObject);
        _UI.heathAnim.SetBool("IsHealing", false);
    }

    IEnumerator HealPlayer()
    {
        if(inRange)
        {
            _PE.greenVignetteAnim.SetTrigger("Healing");

            print("healing");
            if(_PC.health < _PC.maxHP) _PC.health += healAmount;
            if (_PC.health > _PC.maxHP) _PC.health = _PC.maxHP;

        }

        yield return new WaitForSeconds(secondBetweenHeal);
        StartCoroutine(HealPlayer());
    }
}
