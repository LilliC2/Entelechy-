using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
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
    public GameObject backgroundGlow1;
    public GameObject keyGlow1;
    public float cooldownTime1;
    public float cooldownTimer1;
    public bool isCooldown1=false;
    public bool hasItem1=false;
    public bool isPrimary1=false;


    [Header("Ability Slot 2")]
    public Image invenSlot2;
    public Image frame2;
    public GameObject frameHighlight2;
    public GameObject keyHighlight2;
    public Image cooldownFill2;
    public TMP_Text cooldownText2;
    public GameObject backgroundGlow2;
    public GameObject keyGlow2;
    public float cooldownTime2;
    public float cooldownTimer2;
    public bool isCooldown2 = false;
    public bool hasItem2 = false;
    public bool isPrimary2 = false;


    [Header("Ability Slot 3")]
    public Image invenSlot3;
    public Image frame3;
    public GameObject frameHighlight3;
    public GameObject keyHighlight3;
    public Image cooldownFill3;
    public TMP_Text cooldownText3;
    public GameObject backgroundGlow3;
    public GameObject keyGlow3;
    public float cooldownTime3;
    public float cooldownTimer3;
    public bool isCooldown3 = false;
    public bool hasItem3 = false;
    public bool isPrimary3 = false;



    // Start is called before the first frame update
    void Start()
    {
        //turning off frame highlight by def
        frameHighlight1.SetActive(false);
        frameHighlight2.SetActive(false);
        frameHighlight3.SetActive(false);

        //turning off key highlight by def
        keyHighlight1.SetActive(false);
        keyHighlight2.SetActive(false);
        keyHighlight3.SetActive(false);

        //turning off text cooldwon by def
        cooldownText1.text = "";
        cooldownText2.text = "";
        cooldownText3.text = "";

        //fill 0 by def
        cooldownFill1.fillAmount = 0;
        cooldownFill2.fillAmount = 0;
        cooldownFill3.fillAmount = 0;

        //disable background glow by def
        backgroundGlow1.SetActive(false);
        backgroundGlow2.SetActive(false);
        backgroundGlow3.SetActive(false);

        //disable key glow by def
        keyGlow1.SetActive(false);
        keyGlow2.SetActive(false);
        keyGlow3.SetActive(false);

        UpdateSlots();

    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {

            isCooldown1 = true;
            isCooldown2 = true;
            isCooldown3 = true;


            SetCooldownSlo1(5);
            SetCooldownSlo2(6);
            SetCooldownSlo3(1);


        }

        //Activate the cooldown UIs if the ability is on cooldown
        if(isCooldown1 == true)
        {
            CooldownSlot1();
        }

        if (isCooldown2 == true)
        {
            CooldownSlot2();
        }

        if (isCooldown3 == true)
        {
            CooldownSlot3();
        }

        UpdateSlots();
    }
    public void UpdateSlots()
    {
        //Slot 1
        if (hasItem1)
        {
            keyGlow1.SetActive(true);
            if(isCooldown1)
            {
                frameHighlight1.SetActive(false);
                backgroundGlow1.SetActive(false);
            }
            else
            {
                frameHighlight1.SetActive(true);
                backgroundGlow1.SetActive(true);
            }


        }

        //Slot 2
        if (hasItem2)
        {
            keyGlow2.SetActive(true);
            if (isCooldown2)
            {
                frameHighlight2.SetActive(false);
                backgroundGlow2.SetActive(false);
            }
            else
            {
                frameHighlight2.SetActive(true);
                backgroundGlow2.SetActive(true);
            }
        }

        //Slot 3
        if (hasItem3)
        {
            keyGlow3.SetActive(true);
            if (isCooldown3)
            {
                frameHighlight3.SetActive(false);
                backgroundGlow3.SetActive(false);
            }
            else
            {
                frameHighlight3.SetActive(true);
                backgroundGlow3.SetActive(true);
            }
        }
    }

    #region Slot 1

    public void SetCooldownSlo1(int cooldown)
    {
        cooldownTime1 = cooldown;
        cooldownTimer1 = cooldown;
    }

    public void CooldownSlot1()
    {
        cooldownTimer1 -= Time.deltaTime;

        if (hasItem1)
        {
            if (cooldownTimer1 < 0)
            {
                isCooldown1 = false;
                cooldownText1.gameObject.SetActive(false);
                cooldownFill1.fillAmount = 0;
                backgroundGlow1.SetActive(true);
                frameHighlight1.SetActive(true);
            }
            else
            {
                cooldownText1.gameObject.SetActive(true);
                cooldownText1.text = Mathf.RoundToInt(cooldownTimer1).ToString();
                cooldownFill1.fillAmount = cooldownTimer1 / cooldownTime1;
                backgroundGlow1.SetActive(false);
                frameHighlight1.SetActive(false);
            }
        }
    }

    #endregion

    #region Slot 2

    public void SetCooldownSlo2(int cooldown)
    {
        cooldownTime2 = cooldown;
        cooldownTimer2 = cooldown;
    }

    public void CooldownSlot2()
    {
        cooldownTimer2 -= Time.deltaTime;

        if (hasItem2)
        {
            if (cooldownTimer2 < 0)
            {
                isCooldown2 = false;
                cooldownText2.gameObject.SetActive(false);
                cooldownFill2.fillAmount = 0;
                backgroundGlow2.SetActive(true);
                frameHighlight2.SetActive(true);
            }
            else
            {
                cooldownText2.gameObject.SetActive(true);
                cooldownText2.text = Mathf.RoundToInt(cooldownTimer2).ToString();
                cooldownFill2.fillAmount = cooldownTimer2 / cooldownTime2;
                frameHighlight2.SetActive(true);
                backgroundGlow2.SetActive(true);
            }
        }
    }

    #endregion

    #region Slot 3

    public void SetCooldownSlo3(int cooldown)
    {
        cooldownTime3 = cooldown;
        cooldownTimer3 = cooldown;
    }

    public void CooldownSlot3()
    {
        cooldownTimer3 -= Time.deltaTime;

        if (hasItem3)
        {
            if (cooldownTimer3 < 0)
            {
                isCooldown3 = false;
                cooldownText3.gameObject.SetActive(false);
                cooldownFill3.fillAmount = 0;
                backgroundGlow3.SetActive(true);
                frameHighlight3.SetActive(true);
            }
            else
            {
                cooldownText3.gameObject.SetActive(true);
                cooldownText3.text = Mathf.RoundToInt(cooldownTimer3).ToString();
                cooldownFill3.fillAmount = cooldownTimer3 / cooldownTime3;
                backgroundGlow3.SetActive(false);
                frameHighlight3.SetActive(false);
            }
        }
    }

    #endregion

}
