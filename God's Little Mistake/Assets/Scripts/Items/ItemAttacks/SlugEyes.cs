using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugEyes : MonoBehaviour
{
    bool slowProjectile;
    [SerializeField]
    float slowDuration;
    [SerializeField]
    float slowPercent; //decimal

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (slowProjectile)
            {
                print("Slug eye will slow enemy");
                collision.gameObject.GetComponent<BaseEnemy>().ApplySlowness(slowDuration, slowPercent);
            }
        }
    }
}
