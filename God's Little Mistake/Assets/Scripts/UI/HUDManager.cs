using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : GameBehaviour
{
    [Header("Primary Details")]
    public Item inSlot;

    [Header("Primary Details")]
    public TMP_Text primaryName;
    public TMP_Text damageValue;
    public TMP_Text critMultiplierValue;
    public TMP_Text critChanceValue;
    public TMP_Text firerateValue;
    public Sprite emptySlotSprite;

    [Header("Ability Slot 1")]
    public Image invenSlot1;
    public Image frame1;
    public GameObject frameHighlight1;
    public GameObject keyHighlight1;
    public Image cooldownFill1;
    public TMP_Text cooldownText1;

    [Header("Ability Slot 2")]
    public Image invenSlot2;
    public Image frame2;
    public GameObject frameHighlight2;
    public GameObject keyHighlight2;
    public Image cooldownFill2;
    public TMP_Text cooldownText2;

    [Header("Ability Slot 3")]
    public Image invenSlot3;
    public Image frame3;
    public GameObject frameHighlight3;
    public GameObject keyHighlight3;
    public Image cooldownFill3;
    public TMP_Text cooldownText3;

    [Header("Ability Slot 4")]
    public Image invenSlot4;
    public Image frame4;
    public GameObject frameHighlight4;
    public GameObject keyHighlight4;
    public Image cooldownFill4;
    public TMP_Text cooldownText4;

    [Header("Ability Slot 5")]
    public Image invenSlot5;
    public Image frame5;
    public GameObject frameHighlight5;
    public GameObject keyHighlight5;
    public Image cooldownFill5;
    public TMP_Text cooldownText5;

    [Header("Ability Slot 6")]
    public Image invenSlot6;
    public Image frame6;
    public GameObject frameHighlight6;
    public GameObject keyHighlight6;
    public Image cooldownFill6;
    public TMP_Text cooldownText6;


    // Start is called before the first frame update
    void Start()
    {
        //turning off frame highlight by def
        frameHighlight1.SetActive(false);
        frameHighlight2.SetActive(false);
        frameHighlight3.SetActive(false);
        frameHighlight4.SetActive(false);
        frameHighlight5.SetActive(false);
        frameHighlight6.SetActive(false);

        //turning off key highlight by def
        keyHighlight1.SetActive(false);
        keyHighlight2.SetActive(false);
        keyHighlight3.SetActive(false);
        keyHighlight4.SetActive(false);
        keyHighlight5.SetActive(false);
        keyHighlight6.SetActive(false);

        //turning off text cooldwon by def
        cooldownText1.text = "";
        cooldownText2.text = "";
        cooldownText3.text = "";
        cooldownText4.text = "";
        cooldownText5.text = "";
        cooldownText6.text = "";

        //fill 0 by def
        cooldownFill1.fillAmount = 0;
        cooldownFill2.fillAmount = 0;
        cooldownFill3.fillAmount = 0;
        cooldownFill4.fillAmount = 0;
        cooldownFill5.fillAmount = 0;
        cooldownFill6.fillAmount = 0;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HUDBoxHighlightFrame1(bool activeItem)
    {
        if (activeItem == true)
        {
            frameHighlight1.SetActive(true);
        }
        else
        {
            frameHighlight1.SetActive(false);
        }
    }

    public void HUDBoxHighlightKey1(bool activeItem)
    {
        if (activeItem == true)
        {
            keyHighlight1.SetActive(true);
        }
        else
        {
            keyHighlight1.SetActive(false);
        }
    }

    public void CooldownTextSlot1(int cooldown_1)
    {
        if (cooldown_1 == 0)
        {
            cooldownText1.text = "";
        }
        else
        {
            cooldownText1.text = "";
            cooldownText1.text = cooldown_1.ToString();
        }
    }

    public void StartCooldownFillSlot1(int maxValue, int cooldown)
    {
        cooldownFill1.fillAmount = cooldown / maxValue;
    }

    public void ChangeFrame1()
    {


    }
}
