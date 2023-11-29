using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : GameBehaviour
{

    [Header("Audio")]
    public AudioSource death;
    public AudioSource attack;
    public AudioSource hurt;
    public AudioSource walking;


    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die, Stunned, Charmed
    }
    public EnemyStats stats;


    public EnemyState enemyState;

    public EnemyRandomisation enemyRnd;
    [Header("Debuff")]
    public GameObject debuffPanel;
    public GameObject debuffStun;
    public GameObject debuffSlow;
    public GameObject debuffBurn;
    public GameObject debuffBleed;

    [Header("Debuff Check")]
    public bool isStunned = false;
    public bool isSlowed = false;
    public bool isBurned = false;
    public bool isBleeding = false;


    [Header("Particle Systems")]
    [SerializeField]
    ParticleSystem deathParticles;
    [SerializeField]
    GameObject charmedParticles;

    [Header("Animation and Visuals")]

    [SerializeField]
    ParticleSystem[] bleedingSpots;

    [SerializeField]
    GameObject explosionAnimOB;
    public GameObject enemyVisuals;
    public SpriteRenderer[] enemySpritesArray;
    [SerializeField]
    GameObject shadow;



    [Header("Children")]
    bool spawnItem = false;
    bool spawnHealPool = false;
    bool died = false;
    public GameObject healPool;

    private void Start()
    {
        enemySpritesArray = GetComponentsInChildren<SpriteRenderer>();
        explosionAnimOB.SetActive(false);

    }

    private void Update()
    {


        //HealthVisualIndicator(stats.health, stats.maxHP);

        //Health Manager
        if (stats.health <= 0)
        {
            print("Health is 0");
            enemyState = EnemyState.Die;
        }

        //turn on charm particles
        if(enemyState == EnemyState.Charmed)
        {
            print("Charmed"!);
            charmedParticles.SetActive(true);
        }
        else charmedParticles.SetActive(false);
        
    }

    void HealthVisualIndicator(float _health, float _maxHP)
    {
        float currentHPpercent = _health / _maxHP;

        if (currentHPpercent < 75 && currentHPpercent > 50 && bleedingSpots[0] != null) bleedingSpots[0].Play();
        if (currentHPpercent < 50 && currentHPpercent > 25 && bleedingSpots[1] != null) bleedingSpots[1].Play();
        if (currentHPpercent < 25 && bleedingSpots[2] != null) bleedingSpots[2].Play();

    }

    public void FlipSprite(Vector3 _destination)
    {
        bool positive = new();
        if(enemyVisuals.transform.localScale.x < 0) positive = false;
        else if (enemyVisuals.transform.localScale.x > 0) positive = true;

        if (_destination.x > gameObject.transform.localPosition.x)
        {

            if(positive == false) enemyVisuals.transform.localScale = new Vector3(-enemyVisuals.transform.localScale.x, enemyVisuals.transform.localScale.y, enemyVisuals.transform.localScale.z);



        }
        if (_destination.x < gameObject.transform.localPosition.x)
        {

            if (positive) enemyVisuals.transform.localScale = new Vector3(-enemyVisuals.transform.localScale.x, enemyVisuals.transform.localScale.y, enemyVisuals.transform.localScale.z);

        }
    }

    public void Hit(float _dmg)
    {
        //  hurt.Play();


        if (stats.health > 0)
        {
            stats.health -= _dmg;
            HealthVisualIndicator(stats.health, stats.maxHP);
            //print(enemyStats.stats.health);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            //play explosion when hit
            explosionAnimOB.SetActive(true);
            explosionAnimOB.GetComponent<Animator>().SetTrigger("Boom");
            ExecuteAfterFrames(30, () => explosionAnimOB.SetActive(false));

        }

    }
    
    /// <summary>
    /// Apply slowness debuff of set percent to enemyfor set duration
    /// </summary>
    /// <param name="_duration"></param>
    /// <param name="_percent"> Perctange as decimal </param>
    public void ApplySlowness(float _duration, float _percent)
    {
        //var debuffPopup = Instantiate(debuffSlow, debuffPanel.transform);
        //debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
        //debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

        //print("Slowed enemy");
        //var speedBefore = stats.speed;

        //stats.speed = speedBefore * (1 - _percent);
        //ExecuteAfterSeconds(_duration, () => stats.speed = speedBefore);

        var debuffPopup = Instantiate(debuffSlow, debuffPanel.transform);
        if (!isSlowed)
        {
            debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
            debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

            print("Slowed enemy");
            var speedBefore = stats.speed;

            stats.speed = speedBefore * (1 - _percent);
            ExecuteAfterSeconds(_duration, () => stats.speed = speedBefore);
            isSlowed = true;
        }
        else
        {
            Destroy(debuffPopup.gameObject);
            isSlowed= true;
        }

    }

    public void ApplyStun(float _duration)
    {
        //var debuffPopup = Instantiate(debuffStun, debuffPanel.transform);
        //debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
        //debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

        //print("Enemy stunned");
        //var speedBefore = stats.speed;
        //stats.speed = 0;

        //ExecuteAfterSeconds(_duration,() => stats.speed = speedBefore);

        var debuffPopup = Instantiate(debuffStun, debuffPanel.transform);

        if(!isStunned)
        {
            debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
            debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

            print("Enemy stunned");
            var speedBefore = stats.speed;
            stats.speed = 0;

            ExecuteAfterSeconds(_duration, () => stats.speed = speedBefore);
        }
        else
        {
            Destroy(debuffPopup.gameObject);
            isStunned = true;
        }

    }

    public void ApplyBleeding(float _duration, float _dmgPerTick)
    {
        var debuffPopup = Instantiate(debuffBleed, debuffPanel.transform);
        //debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
        //debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

        //bool isbleeding = true;
        //ExecuteAfterSeconds(_duration, () => isbleeding = false);
        //float timer = 0;

        //while (isbleeding)
        //{
        //    print("is bleeding");
        //    timer += Time.deltaTime;
        //    if (timer >= 1)
        //    {
        //        timer = 0;
        //        Hit(_dmgPerTick);
        //    }
        //}

        if(!isBleeding)
        {
            debuffPopup.GetComponent<DebuffPopup>().totalDuration = _duration;
            debuffPopup.GetComponent<DebuffPopup>().currentDuration = _duration;

            bool isbleeding = true;
            ExecuteAfterSeconds(_duration, () => isbleeding = false);
            float timer = 0;

            while (isbleeding)
            {
                print("is bleeding");
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    timer = 0;
                    Hit(_dmgPerTick);
                }
            }
        }
        else
        {
            Destroy(debuffPopup.gameObject);
            isBleeding = true;
        }

    }
    

    public void Die()
    {
        if(!died)
        {

            foreach (var PS in bleedingSpots)
            {
                PS.Stop();
            }

            //death.Play();
            print("Enemy dies");
            died = true;

            Destroy(GetComponent<EnemyLongRange>());

            //turn off collider
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            enemyVisuals.SetActive(false);
            shadow.SetActive(false);

            //run partciles
            deathParticles.Play();

            explosionAnimOB.SetActive(true);
            //ooze animation
            explosionAnimOB.GetComponent<Animator>().SetTrigger("Boom");

            int rand = Random.Range(0, 6);

            print("Number gen when enemy dies " + rand);

            switch (rand)
            {
  
                case 1:
                    if(!spawnHealPool)
                    {
                        spawnHealPool = true;
                        Instantiate(healPool, gameObject.transform.position, Quaternion.identity);
                        print("Heal pool spawns");
                    }
                    break;
                case 2:

                    //nothing
                    print("Enemy dies and nothing drops");

                    break;
                default:
                    if (!spawnItem)
                    {
                        spawnItem = true;
                        GameObject item = Instantiate(_IG.GenerateItem(), gameObject.transform.position, Quaternion.identity);

                        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;

                        print(item.name);
                    }
                    break;
            }

            //make death splatter
            _EM.DeathSplatter(transform.position);

            //remove from list
            _EM.enemiesSpawned.Remove(this.gameObject);

            ExecuteAfterSeconds(1f, () => Destroy(this.gameObject));
        }

        
        
    }
}
