using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Value Editor Tester")]
    public float critValTest1;
    public float critValTest2;
    public bool isHead;
    public bool isTorso;
    public bool isLegs;

    [Header("Stat Pop up")]
    public GameObject popupPanel;
    public GameObject popupPanel1;
    public TMP_Text ItemName;
    public Image ItemIcon;

    [Header("Damage1")]
    public GameObject damagePanel;
    public GameObject damageBad;
    public GameObject damageGood;
    public GameObject damageNeutral;
    public TMP_Text damageText;

    [Header("Crit1")]
    public GameObject critPanel;
    public GameObject critBad;
    public GameObject critGood;
    public GameObject critNeutral;
    public TMP_Text critText;

    [Header("Speed1")]
    public GameObject speedPanel;
    public GameObject speedBad;
    public GameObject speedGood;
    public GameObject speedNeutral;
    public TMP_Text speedText;

    [Header("Indicator1")]
    public GameObject headGlow;
    public GameObject torsoGlow;
    public GameObject legsGlow;
    public Image holdFill;

    [Header("Comparison Pop up")]
    public GameObject popupPanel2;
    public TMP_Text ItemName2;
    public Image ItemIcon2;

    [Header("Damage2")]
    public GameObject damagePanel2;
    public TMP_Text damageText2;

    [Header("Crit2")]
    public GameObject critPanel2;
    public GameObject critNeutral2;
    public TMP_Text critText2;

    [Header("Speed2")]
    public GameObject speedPanel2;
    public GameObject speedNeutral2;
    public TMP_Text speedText2;

    [Header("Indicator2")]
    public GameObject headGlow2;
    public GameObject torsoGlow2;
    public GameObject legsGlow2;

    [Header("Hold E")]
    public Image holdEFill;
    public float startTimer;
    public float holdTimer = 1f;
    public bool isTiming;
    public Image glowingHold;

    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
        popupPanel1.SetActive(false);
        popupPanel2.SetActive(false);
        critGood.SetActive(false);
        critBad.SetActive(false);
        isTiming = false;
        holdEFill.fillAmount = 0;

        isHead = false;
        isTorso = false;
        isLegs = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTiming = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {

            isTiming = false;
            holdEFill.fillAmount = 0;
            startTimer = 0;
        }

        if (isTiming)
        {
            startTimer += Time.deltaTime;
            holdEFill.fillAmount = startTimer / holdTimer;

            if (startTimer > holdTimer)
            {
                isTiming = false;
                holdEFill.fillAmount = 1;
                startTimer = 0;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Tab))
        {
            popupPanel2.SetActive(true);
            UpdateComparison();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Tab))
        {
            popupPanel2.SetActive(false);
        }
    }

    public void UpdateItemPopUp(Item _hoverItem)
    {
        popupPanel1.SetActive(true);
        ItemName.text = _hoverItem.itemName;
        ItemIcon.sprite = _hoverItem.icon;


        if (_hoverItem.segment == Item.Segment.Head)
        {
            isHead = true;
            isTorso = false;
            isLegs = false;

            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            critText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(true);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(false);

            ////Check if player has an item on the slot
            //if (_PC.headItem != null)
            //{
            //    ItemName2.text = _PC.headItem.itemName;
            //    ItemIcon2.sprite = _PC.headItem.icon;

            //    popupPanel2.SetActive(true);
            //    damagePanel2.SetActive(true);
            //    damageText2.text = _PC.headItem.dmg.ToString();
            //    critPanel2.SetActive(true);
            //    critText2.text = _PC.headItem.critChance.ToString();
            //    speedPanel2.SetActive(false);

            //    //set glow indicator
            //    headGlow2.SetActive(true);
            //    torsoGlow2.SetActive(false);
            //    legsGlow2.SetActive(false);
            //}
            //else
            //{
            //    popupPanel2.SetActive(false);
            //}

            //CritIndicators
            if (_hoverItem.critChance < _PC.headItem.critChance)
            {
                critBad.SetActive(true);
                critGood.SetActive(false);
            }

            if (_hoverItem.critChance == _PC.headItem.critChance)
            {
                critBad.SetActive(false);
                critGood.SetActive(false);
            }

            if (_hoverItem.critChance > _PC.headItem.critChance)
            {
                critBad.SetActive(false);
                critGood.SetActive(true);
            }

            //DmgIndicators
            if (_hoverItem.dmg < _PC.headItem.dmg)
            {
                damageBad.SetActive(true);
                damageGood.SetActive(false);
            }

            if (_hoverItem.dmg == _PC.headItem.dmg)
            {
                damageBad.SetActive(false);
                damageGood.SetActive(false);
            }

            if (_hoverItem.dmg > _PC.headItem.dmg)
            {
                damageBad.SetActive(false);
                damageGood.SetActive(true);
            }

        }

        if (_hoverItem.segment == Item.Segment.Torso)
        {
            isHead = false;
            isTorso = true;
            isLegs = false;

            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            critText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(true);
            legsGlow.SetActive(false);

            //Check if player has an item on the slot
            //if (_PC.torsoItem != null)
            //{
            //    ItemName2.text = _PC.torsoItem.itemName;
            //    ItemIcon2.sprite = _PC.torsoItem.icon;

            //    popupPanel2.SetActive(true);
            //    damagePanel2.SetActive(true);
            //    damageText2.text = _PC.torsoItem.dmg.ToString();
            //    critPanel2.SetActive(true);
            //    critText2.text = _PC.torsoItem.critChance.ToString();
            //    speedPanel2.SetActive(false);

            //    //set glow indicator
            //    headGlow2.SetActive(false);
            //    torsoGlow2.SetActive(true);
            //    legsGlow2.SetActive(false);
            //}
            //else
            //{
            //    popupPanel2.SetActive(false);
            //}

            //CritIndicators
            if (_hoverItem.critChance < _PC.torsoItem.critChance)
            {
                critBad.SetActive(true);
                critGood.SetActive(false);
            }

            if (_hoverItem.critChance == _PC.torsoItem.critChance)
            {
                critBad.SetActive(false);
                critGood.SetActive(false);
            }

            if (_hoverItem.critChance > _PC.torsoItem.critChance)
            {
                critBad.SetActive(false);
                critGood.SetActive(true);
            }

            //DmgIndicators
            if (_hoverItem.dmg < _PC.torsoItem.dmg)
            {
                damageBad.SetActive(true);
                damageGood.SetActive(false);
            }

            if (_hoverItem.dmg == _PC.torsoItem.dmg)
            {
                damageBad.SetActive(false);
                damageGood.SetActive(false);
            }

            if (_hoverItem.dmg > _PC.torsoItem.dmg)
            {
                damageBad.SetActive(false);
                damageGood.SetActive(true);
            }
        }

        if (_hoverItem.segment == Item.Segment.Legs)
        {
            isHead = false;
            isTorso = false;
            isLegs = true;

            damagePanel.SetActive(false);
            critPanel.SetActive(false);
            speedPanel.SetActive(true);
            speedText.text = _hoverItem.movementSpeed.ToString();

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(true);

            //Check if player has an item on the slot
            //if (_PC.legItem != null)
            //{
            //    ItemName2.text = _PC.legItem.itemName;
            //    ItemIcon2.sprite = _PC.legItem.icon;

            //    speedPanel2.SetActive(true);
            //    speedText2.text = _PC.legItem.movementSpeed.ToString();
            //    critPanel2.SetActive(false);
            //    damagePanel2.SetActive(false);


            //    //set glow indicator
            //    headGlow2.SetActive(false);
            //    torsoGlow2.SetActive(false);
            //    legsGlow2.SetActive(true);
            //}
            //else
            //{
            //    popupPanel2.SetActive(false);
            //}

            if (_hoverItem.movementSpeed > _PC.torsoItem.movementSpeed)
            {
                speedBad.SetActive(false);
                speedGood.SetActive(true);
            }

            if (_hoverItem.movementSpeed < _PC.torsoItem.movementSpeed)
            {
                speedBad.SetActive(true);
                speedGood.SetActive(false);
            }

            if (_hoverItem.movementSpeed == _PC.torsoItem.movementSpeed)
            {
                speedBad.SetActive(false);
                speedGood.SetActive(false);
            }
        }
    }

    public void UpdateComparison()
    {
        if(isHead)
        {
            if (_PC.headItem != null)
            {
                ItemName2.text = _PC.headItem.itemName;
                ItemIcon2.sprite = _PC.headItem.icon;

                popupPanel2.SetActive(true);
                damagePanel2.SetActive(true);
                damageText2.text = _PC.headItem.dmg.ToString();
                critPanel2.SetActive(true);
                critText2.text = _PC.headItem.critChance.ToString();
                speedPanel2.SetActive(false);

                //set glow indicator
                headGlow2.SetActive(true);
                torsoGlow2.SetActive(false);
                legsGlow2.SetActive(false);
            }
        }
        
        if(isTorso)
        {
            if (_PC.torsoItem != null)
            {
                ItemName2.text = _PC.torsoItem.itemName;
                ItemIcon2.sprite = _PC.torsoItem.icon;

                popupPanel2.SetActive(true);
                damagePanel2.SetActive(true);
                damageText2.text = _PC.torsoItem.dmg.ToString();
                critPanel2.SetActive(true);
                critText2.text = _PC.torsoItem.critChance.ToString();
                speedPanel2.SetActive(false);

                //set glow indicator
                headGlow2.SetActive(false);
                torsoGlow2.SetActive(true);
                legsGlow2.SetActive(false);
            }
        }
        
        if(isLegs)
        {
            if (_PC.legItem != null)
            {
                ItemName2.text = _PC.legItem.itemName;
                ItemIcon2.sprite = _PC.legItem.icon;

                speedPanel2.SetActive(true);
                speedText2.text = _PC.legItem.movementSpeed.ToString();
                critPanel2.SetActive(false);
                damagePanel2.SetActive(false);


                //set glow indicator
                headGlow2.SetActive(false);
                torsoGlow2.SetActive(false);
                legsGlow2.SetActive(true);
            }
        }
    }


}
