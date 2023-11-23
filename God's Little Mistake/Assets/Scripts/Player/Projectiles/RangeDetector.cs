using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : GameBehaviour
{
    public float range;
    public Vector3 positionShotFrom;

    public ParticleSystem endOfRangePS;

    bool playedPS;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, positionShotFrom) >= range)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (endOfRangePS != null)
            {
                if(!playedPS)
                {
                    playedPS = true;
                    endOfRangePS.Play();

                    ExecuteAfterSeconds(endOfRangePS.main.duration, () => Destroy(gameObject));
                }

            }
            else
            {
                Destroy(gameObject);
            }

            //print("Range is " + range + ". Out of range at (PC)" + Vector3.Distance(transform.position, _PC.transform.position));
            //print("Range is " + range + ". Out of range at (pos shot) " + Vector3.Distance(transform.position, positionShotFrom));
            //ExecuteAfterFrames(3, () => Destroy(gameObject));

        }

    }
}
