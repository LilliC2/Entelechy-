using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : GameBehaviour
{
    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die, Stunned, Charmed
    }

    public EnemyState enemyState;

    public EnemyRandomisation enemyRnd;


    [SerializeField]
    ParticleSystem deathParticles;
    [SerializeField]
    GameObject charmedParticles;

    [SerializeField]
    GameObject shadow;

    [SerializeField]
    Animator explosionAnim;
    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    GameObject enemyVisuals;

    bool spawnItem = false;
    bool spawnHealPool = false;
    bool died = false;

    public SpriteRenderer[] enemySpritesArray;
    public EnemyStats stats;
    public GameObject healPool;

    private void Start()
    {
        enemySpritesArray = GetComponentsInChildren<SpriteRenderer>();
        enemyRnd = GetComponentInChildren<EnemyRandomisation>();

    }

    private void Update()
    {
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

            //destroy bullet that hit it
            //Destroy(collision.gameObject);
        }

    }
    
    /// <summary>
    /// Apply slowness debuff of set percent to enemyfor set duration
    /// </summary>
    /// <param name="_duration"></param>
    /// <param name="_percent"> Perctange as decimal </param>
    public void ApplySlowness(float _duration, float _percent)
    {
        print("Slowed enemy");
        var speedBefore = stats.speed;

        stats.speed = speedBefore * (1 - _percent);
        ExecuteAfterSeconds(_duration, () => stats.speed = speedBefore);
    }

    public void ApplyStun(float _duration)
    {
        print("Enemy stunned");
        var speedBefore = stats.speed;
        stats.speed = 0;

        ExecuteAfterSeconds(_duration,() => stats.speed = speedBefore);

    }

    public void ApplyBleeding(float _duration, float _dmgPerTick)
    {
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
            explosionAnim.SetTrigger("Death");

            //eye is for testing
            int rand = Random.Range(0, 4);

            switch (rand)
            {
                case 1:

                    if (!spawnItem)
                    {
                        spawnItem = true;
                        GameObject item = Instantiate(_IG.GenerateItem(stats.category.ToString()), gameObject.transform.position, Quaternion.identity);

                        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;

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
