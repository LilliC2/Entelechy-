using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BabyRotate : GameBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Rotate", 0, 0.6f);
    }

    void Rotate()
    {
        if(gameObject.activeSelf)
        {

            gameObject.transform.DORotate(new Vector3(45, 0, Random.Range(45, 60)), 0.3f);

            ExecuteAfterSeconds(0.3f, () => gameObject.transform.DORotate(new Vector3(45, 0, Random.Range(-40, -60)), 0.3f));
        }

    }
}
