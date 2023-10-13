using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;


public class Dummy : GameBehaviour
{
    BaseEnemy BaseEnemy;
    public TMP_Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        BaseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = BaseEnemy.stats.health.ToString();
    }
}
