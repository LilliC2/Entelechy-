using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : GameBehaviour
{
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
    GameObject explosionAnimOB;
    [SerializeField]
    GameObject enemyVisuals;
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
        enemyRnd = GetComponentInChildren<EnemyRandomisation>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) ApplySlowness(3, 10);


        HealthVisualIndicator(stats.health, stats.maxHP);

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

        float H, S, V;

        foreach (var sprite in enemySpritesArray)
        {
            Color.RGBToHSV(sprite.color, out H, out S, out V);

            sprite.color = Color.HSVToRGB(H, currentHPpercent, V);
        }

        
    }

    public void Hit(float _dmg)
    {
        //check for debuffs
        if(_PIA.slugEyesEquipped)
        {
            if (_PIA.SlugEyes()) ApplySlowness(_PIA.slowDuration, _PIA.slowPercent);
        }

        if (_PIA.wolfClawsEquipped)
        {
            if (_PIA.WolfClaw()) ApplyBleeding(_PIA.bleedDuration, _PIA.bleedTickDmg);
        }

        if (stats.health > 0)
        {
            stats.health -= _dmg;

            //print(enemyStats.stats.health);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            print("hit");
            //Add hit code here;
            Hit(_PC.dmg);

            //play explosion when hit
            explosionAnimOB.SetActive(true);
            explosionAnimOB.GetComponent<Animator>().SetTrigger("Boom");
            ExecuteAfterFrames(30, () => explosionAnimOB.SetActive(false));

            //destroy bullet that hit it
            Destroy(collision.gameObject);
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

            //eye is for testing
            int rand = Random.Range(0, 4);

            print("Number gen when enemy dies " + rand);

            switch (rand)
            {
                case 1:

                    if (!spawnItem)
                    {
                        spawnItem = true;
                        GameObject item = Instantiate(_IG.GenerateItem(stats.category), gameObject.transform.position, Quaternion.identity);

                        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;

                        print(item.name);
                        print("Spawning item of " + stats.category.ToString() + " category");
                    }

                    break;
                case 2:
                    if(!spawnHealPool)
                    {
                        spawnHealPool = true;
                        Instantiate(healPool, gameObject.transform.position, Quaternion.identity);
                        print("Heal pool spawns");
                    }
                    break;
                case 3:

                    //nothing
                    print("Enemy dies and nothing drops");

                    break;
            }


            ExecuteAfterSeconds(1f, () => Destroy(this.gameObject));
        }

        
        
    }
}
