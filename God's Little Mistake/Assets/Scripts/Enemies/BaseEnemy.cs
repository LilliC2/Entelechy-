using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : GameBehaviour
{
    public enum EnemyState
    {
        Patrolling, Chase, Attacking, Die
    }

    public EnemyState enemyState;

    public EnemyRandomisation enemyRnd;

    public SpriteRenderer image;
    public EnemyStats stats;
    public GameObject healPool;

    private void Start()
    {
        image = GetComponentInChildren<SpriteRenderer>();
        enemyRnd = GetComponentInChildren<EnemyRandomisation>();

        string cat = enemyRnd.categories[Random.Range(0, enemyRnd.categories.Count)];

        stats.category = (EnemyStats.Category)System.Enum.Parse(typeof(EnemyStats.Category), cat);
        print("this enemies category is " + cat + " and is set to " + stats.category);
    }

    private void Update()
    {
        HealthVisualIndicator(stats.health, stats.maxHP);

        //Health Manager
        if (stats.health <= 0)
        {
            enemyState = EnemyState.Die;
        }

    }

    void HealthVisualIndicator(float _health, float _maxHP)
    {
        float currentHPpercent = _health / _maxHP;

        float H, S, V;

        Color.RGBToHSV(image.color, out H, out S, out V);

        image.color = Color.HSVToRGB(H, currentHPpercent, V);
    }

    public void Hit()
    {
        if (stats.health > 0)
        {
            stats.health -= _PC.dmg;
            //print(enemyStats.stats.health);
        }
    }
    void MeleeHit()
    {
        if (stats.health > 0)
        {
            stats.health -= _MA.DMGOutput;
            //print(enemyStats.stats.health);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            print("hit");
            //Add hit code here;
            Hit();

            //destroy bullet that hit it
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("Melee"))
        {
            print("melee hit");
            //Add hit code here;
            MeleeHit();
        }
    }

    public void Die()
    {
        //eye is for testing
        int rand = Random.Range(0, 4);

        switch(rand)
        {
            case 1:
                Instantiate(_IG.GenerateItem(stats.category.ToString()), gameObject.transform.position, Quaternion.identity);
                print("Spawning item of " + stats.category.ToString() + " category");
                break;
            case 2:
                Instantiate(healPool, gameObject.transform.position, Quaternion.identity);
                break;
        }

        
        
        Destroy(this.gameObject);
    }
}
