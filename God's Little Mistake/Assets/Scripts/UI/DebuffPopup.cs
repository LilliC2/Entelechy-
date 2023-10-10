using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebuffPopup : MonoBehaviour
{
    public float durationTimer;
    public float duration;
    [SerializeField]
    Image fill;


    // Update is called once per frame
    void Update()
    {
        durationTimer -= Time.deltaTime;

        fill.fillAmount = durationTimer / durationTimer;

        if (fill.fillAmount <= 0) Destroy(this.gameObject);
    }
}
