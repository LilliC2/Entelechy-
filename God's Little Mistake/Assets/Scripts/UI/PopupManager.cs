using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Inventory Pop up")]
    public GameObject popupPanel;
    public TMP_Text ItemName;
    public Image ItemIcon;
    public GameObject damagePanel;
    public GameObject damageBad;
    public GameObject damageGood;
    public GameObject damageNeutral;
    public TMP_Text damageText;
    public GameObject critPanel;
    public GameObject critBad;
    public GameObject critGood;
    public GameObject critNeutral;
    public TMP_Text critText;
    public GameObject speedPanel;
    public GameObject speedBad;
    public GameObject speedGood;
    public GameObject speedNeutral;
    public TMP_Text speedText;
    public GameObject headGlow;
    public GameObject torsoGlow;
    public GameObject legsGlow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
