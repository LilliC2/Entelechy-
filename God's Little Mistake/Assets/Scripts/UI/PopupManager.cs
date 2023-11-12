using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Stat Pop up")]
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
    public Image holdFill;

    [Header("Comparison Pop up")]
    public GameObject popupPanel2;
    public TMP_Text ItemName2;
    public Image ItemIcon2;
    public GameObject damagePanel2;
    public GameObject damageBad2;
    public GameObject damageGood2;
    public GameObject damageNeutral2;
    public TMP_Text damageText2;
    public GameObject critPanel2;
    public GameObject critBad2;
    public GameObject critGood2;
    public GameObject critNeutral2;
    public TMP_Text critText2;
    public GameObject speedPanel2;
    public GameObject speedBad2;
    public GameObject speedGood2;
    public GameObject speedNeutral2;
    public TMP_Text speedText2;
    public GameObject headGlow2;
    public GameObject torsoGlow2;
    public GameObject legsGlow2;

    [Header("Hold E")]
    public Image holdEFill;
    public float startTimer;
    public float holdTimer = 2f;
    public bool isTiming;
    public Image glowingHold;

    // Start is called before the first frame update
    void Start()
    {
        popupPanel.SetActive(false);
        popupPanel2.SetActive(false);
        critGood.SetActive(false);
        critBad.SetActive(false);
        isTiming = false;
        holdEFill.fillAmount = 0;
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
    }

    public void UpdateItemPopUp(Item _hoverItem)
    {
        ItemName.text = _hoverItem.itemName;
        ItemIcon.sprite = _hoverItem.icon;

        if (_hoverItem.segment == Item.Segment.Head)
        {
            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            damageText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(true);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(false);

            //Check if player has an item on the slot
            if(_PC.headItem != null)
            {
                popupPanel2.SetActive(true);
                damagePanel2.SetActive(true);
                damageText2.text = _PC.headItem.dmg.ToString();
                critPanel2.SetActive(true);
                damageText2.text = _PC.headItem.critChance.ToString();
                speedPanel2.SetActive(false);

                //set glow indicator
                headGlow2.SetActive(true);
                torsoGlow2.SetActive(false);
                legsGlow2.SetActive(false);

                if(_hoverItem.dmg < _PC.headItem.dmg)
                {
                    critBad.SetActive(true);
                    critGood.SetActive(false);
                }

                if (_hoverItem.dmg == _PC.headItem.dmg)
                {
                    critBad.SetActive(false);
                    critGood.SetActive(false);
                }

                if (_hoverItem.dmg > _PC.headItem.dmg)
                {
                    critBad.SetActive(false);
                    critGood.SetActive(true);
                }


            }
            else
            {
                popupPanel2.SetActive(false);
            }

        }

        if (_hoverItem.segment == Item.Segment.Torso)
        {
            damagePanel.SetActive(true);
            damageText.text = _hoverItem.dmg.ToString();
            critPanel.SetActive(true);
            damageText.text = _hoverItem.critChance.ToString();
            speedPanel.SetActive(false);

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(true);
            legsGlow.SetActive(false);

            //Check if player has an item on the slot
            if (_PC.torsoItem != null)
            {
                popupPanel2.SetActive(true);
                damagePanel2.SetActive(true);
                damageText2.text = _PC.torsoItem.dmg.ToString();
                critPanel2.SetActive(true);
                damageText2.text = _PC.torsoItem.critChance.ToString();
                speedPanel2.SetActive(false);

                //set glow indicator
                headGlow2.SetActive(false);
                torsoGlow2.SetActive(true);
                legsGlow2.SetActive(false);

                if (_hoverItem.dmg < _PC.torsoItem.dmg)
                {
                    critBad.SetActive(true);
                    critGood.SetActive(false);
                }

                if (_hoverItem.dmg == _PC.torsoItem.dmg)
                {
                    critBad.SetActive(false);
                    critGood.SetActive(false);
                }

                if (_hoverItem.dmg > _PC.torsoItem.dmg)
                {
                    critBad.SetActive(false);
                    critGood.SetActive(true);
                }
            }
            else
            {
                popupPanel2.SetActive(false);
            }
        }

        if (_hoverItem.segment == Item.Segment.Legs)
        {
            damagePanel.SetActive(false);
            critPanel.SetActive(false);
            speedPanel.SetActive(true);

            //set glow indicator
            headGlow.SetActive(false);
            torsoGlow.SetActive(false);
            legsGlow.SetActive(true);

            //Check if player has an item on the slot
            if (_PC.legItem != null)
            {
                popupPanel2.SetActive(true);
                damagePanel2.SetActive(true);
                damageText2.text = _PC.legItem.dmg.ToString();
                critPanel2.SetActive(true);
                damageText2.text = _PC.legItem.critChance.ToString();
                speedPanel2.SetActive(false);

                //set glow indicator
                headGlow2.SetActive(false);
                torsoGlow2.SetActive(false);
                legsGlow2.SetActive(true);
            }
            else
            {
                popupPanel2.SetActive(false);
            }
        }

        

    }
}
