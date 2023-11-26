using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoomerangProjectile : GameBehaviour
{
    float halfway;
    float totalDistance;

    [SerializeField]
    GameObject explosionAnimOB;
    [SerializeField]
    public GameObject image;
    [SerializeField]
    Animator explosionAnim;
    Rigidbody rb;

    [SerializeField]
    float maxSize;
    [SerializeField]
    float minSize;

    bool turn = false;

    public Vector3 initalTarget;


    Vector3 playerPosWhenThrown;

    bool turnTime;
    public float duration = 3; // in seconds

    public Vector3 beginPoint;
    Vector3 endPoint;

    Vector3 center;

    private float startTime;
    public bool startAgain = true;


    private void Awake()
    {
        beginPoint = _PC.transform.position;

        endPoint = initalTarget;

        rb = GetComponent<Rigidbody>();
        explosionAnim = explosionAnimOB.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        ExecuteAfterSeconds(duration, () => turn = true);


        totalDistance = _IM.itemDataBase[2].projectileRange*2;
        halfway = totalDistance / 2;

        image.transform.DOScale(maxSize, halfway);
        ExecuteAfterSeconds(halfway, () => image.transform.DOScale(minSize, halfway));
    }

    // Update is called once per frame
    void Update()
    {
        //if(!turn)
        //{
        //    if (startAgain) Start();

        //    Vector3 center = (beginPoint + finalPoint) * .5f;
        //    center -= farPoint;

        //    Vector3 riseRelCenter = beginPoint - center;
        //    Vector3 setRelCenter = finalPoint - center;

        //    transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, (Time.time - startTime) / duration);
        //    transform.position += center;

        //    if (Vector3.Distance(transform.position, finalPoint) < 1) print("Close");

        //}

        center = (beginPoint + endPoint) * .5f;

        transform.position = Vector3.Slerp(beginPoint - center, endPoint - center, (Time.time - startTime) / duration) + center;

        //if (turn)
        //{
        //    if (turnTime)
        //    {
        //        print("turn time");
        //        turnTime = false;
        //        startTime = Time.time;
        //    }

        //    print("go back");
        //    Vector3 center2 = (beginPoint + finalPoint) * .5f;
        //    center2 -= farPoint;

        //    Vector3 riseRelCenter2 = finalPoint - center2;
        //    Vector3 setRelCenter2 = beginPoint - center2;

        //    transform.position = Vector3.Slerp(riseRelCenter2, setRelCenter2, (Time.time - startTime) / duration);
        //    transform.position += center2;
        //}

        //if(turn && Vector3.Distance(playerPosWhenThrown, transform.position) > halfway)
        //{
        //    transform.position = Vector3.Slerp(playerPosWhenThrown, initalTarget, Time.deltaTime * _IM.itemDataBase[2].projectileSpeed);

        //    print("we half");
        //    turn = false;
        //}

        //if (!turn)
        //{
        //    //travel back

        //    rb.velocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;
        //    transform.position = Vector3.Slerp(transform.position, new Vector3(_PC.transform.position.x, _PC.transform.position.y + 1, _PC.transform.position.z), Time.deltaTime * _IM.itemDataBase[2].projectileSpeed/2);
        //}
        //if(!turn && Vector3.Distance(_PC.transform.position, transform.position) < 1.5f)
        //{
        //    _PAtk.returned = true;
        //    Destroy(gameObject);
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //explosionAnimOB.SetActive(true);

            //image.SetActive(false);
            //print("Destroy Projectile");
            ////ooze animation
            //explosionAnim.SetTrigger("Death");


            //rb.velocity = Vector3.zero;
            //Destroy(this.gameObject);

            collision.gameObject.GetComponent<BaseEnemy>().Hit(_IM.itemDataBase[2].dmg);

            //get dmg from enemy
            //Hit(collision.collider.gameObject.GetComponent<BaseEnemy>().stats.dmg);
        }
    }
}
