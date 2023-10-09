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

    [Header("Ability Slot 4")]
    public Image invenSlot4;
    public Image frame4;
    public GameObject frameHighlight4;
    public GameObject keyHighlight4;
    public Image cooldownFill4;
    public TMP_Text cooldownText4;
    public GameObject backgroundGlow4;
    public GameObject keyGlow4;
    public float cooldownTime4;
    public float cooldownTimer4;
    public bool isCooldown4 = false;
    public bool hasItem4 = false;

    [Header("Ability Slot 5")]
    public Image invenSlot5;
    public Image frame5;
    public GameObject frameHighlight5;
    public GameObject keyHighlight5;
    public Image cooldownFill5;
    public TMP_Text cooldownText5;
    public GameObject backgroundGlow5;
    public GameObject keyGlow5;
    public float cooldownTime5;
    public float cooldownTimer5;
    public bool isCooldown5 = false;
    public bool hasItem5 = false;

    [Header("Ability Slot 6")]
    public Image invenSlot6;
    public Image frame6;
    public GameObject frameHighlight6;
    public GameObject keyHighlight6;
    public Image cooldownFill6;
    public TMP_Text cooldownText6;
    public GameObject backgroundGlow6;
    public GameObject keyGlow6;
    public float cooldownTime6;
    public float cooldownTimer6;
    public bool isCooldown6 = false;
    public bool hasItem6 = false;


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

        //disable background glow by def
        backgroundGlow1.SetActive(false);
        backgroundGlow2.SetActive(false);
        backgroundGlow3.SetActive(false);
        backgroundGlow4.SetActive(false);
        backgroundGlow5.SetActive(false);
        backgroundGlow6.SetActive(false);

        //disable key glow by def
        keyGlow1.SetActive(false);
        keyGlow2.SetActive(false);
        keyGlow3.SetActive(false);
        keyGlow4.SetActive(false);
        keyGlow5.SetActive(false);
        keyGlow6.SetActive(false);

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
            isCooldown4 = true;
            isCooldown5 = true;
            isCooldown6 = true;

            SetCooldownSlo1(5);
            SetCooldownSlo2(6);
            SetCooldownSlo3(1);
            SetCooldownSlo4(2);
            SetCooldownSlo5(9);
            SetCooldownSlo6(7);

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

        if (isCooldown4 == true)
        {
            CooldownSlot4();
        }

        if (isCooldown5 == true)
        {
            CooldownSlot5();
        }

        if (isCooldown6 == true)
        {
            CooldownSlot6();
        }

        UpdateSlots();
    }
    public void UpdateSlots()
    {
        //Slot 1
        if (hasItem1)
        {
            keyHighlight1.SetActive(true);
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
        else
        {
            keyHighlight1.SetActive(false);
            keyGlow1.SetActive(false);
        }

        //Slot 2
        if (hasItem2)
        {
            keyHighlight2.SetActive(true);
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
        else
        {
            keyHighlight2.SetActive(false);
            keyGlow2.SetActive(false);
        }

        //Slot 3
        if (hasItem3)
        {
            keyHighlight3.SetActive(true);
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
        else
        {
            keyHighlight3.SetActive(false);
            keyGlow3.SetActive(false);
        }

        //Slot4
        if (hasItem4)
        {
            keyHighlight4.SetActive(true);
            keyGlow4.SetActive(true);
            if (isCooldown4)
            {
                frameHighlight4.SetActive(false);
                backgroundGlow4.SetActive(false);
            }
            else
            {
                frameHighlight4.SetActive(true);
                backgroundGlow4.SetActive(true);
            }
        }
        else
        {
            keyHighlight4.SetActive(false);
            keyGlow4.SetActive(false);
        }

        //Slot5
        if (hasItem5)
        {
            keyHighlight5.SetActive(true);
            keyGlow5.SetActive(true);
            if (isCooldown5)
            {
                frameHighlight5.SetActive(false);
                backgroundGlow5.SetActive(false);
            }
            else
            {
                frameHighlight5.SetActive(true);
                backgroundGlow5.SetActive(true);
            }
        }
        else
        {
            keyHighlight5.SetActive(false);
            keyGlow5.SetActive(false);
        }

        //Slot6
        if (hasItem6)
        {
            keyHighlight6.SetActive(true);
            keyGlow6.SetActive(true);
            if (isCooldown6)
            {
                frameHighlight6.SetActive(false);
                backgroundGlow6.SetActive(false);
            }
            else
            {
                frameHighlight6.SetActive(true);
                backgroundGlow6.SetActive(true);
            }
        }
        else
        {
            keyHighlight6.SetActive(false);
            keyGlow6.SetActive(false);
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

    #region Slot 4

    public void SetCooldownSlo4(int cooldown)
    {
        cooldownTime4 = cooldown;
        cooldownTimer4 = cooldown;
    }

    public void CooldownSlot4()
    {
        cooldownTimer4 -= Time.deltaTime;

        if(hasItem4)
        {
            if (cooldownTimer4 < 0)
            {
                isCooldown4 = false;
                cooldownText4.gameObject.SetActive(false);
                cooldownFill4.fillAmount = 0;
                backgroundGlow4.SetActive(true);
                frameHighlight4.SetActive(true);
            }
            else
            {
                cooldownText4.gameObject.SetActive(true);
                cooldownText4.text = Mathf.RoundToInt(cooldownTimer4).ToString();
                cooldownFill4.fillAmount = cooldownTimer4 / cooldownTime4;
                backgroundGlow4.SetActive(false);
                frameHighlight4.SetActive(false);
            }
        }
        
    }

    #endregion

    #region Slot 5

    public void SetCooldownSlo5(int cooldown)
    {
        cooldownTime5 = cooldown;
        cooldownTimer5 = cooldown;
    }

    public void CooldownSlot5()
    {
        cooldownTimer5 -= Time.deltaTime;

        if(hasItem5) 
        {
            if (cooldownTimer5 < 0)
            {
                isCooldown5 = false;
                cooldownText5.gameObject.SetActive(false);
                cooldownFill5.fillAmount = 0;
                backgroundGlow5.SetActive(true);
                frameHighlight5.SetActive(true);
            }
            else
            {
                cooldownText5.gameObject.SetActive(true);
                cooldownText5.text = Mathf.RoundToInt(cooldownTimer5).ToString();
                cooldownFill5.fillAmount = cooldownTimer5 / cooldownTime5;
                backgroundGlow5.SetActive(false);
                frameHighlight5.SetActive(false);
            }
        }
        
    }

    #endregion

    #region Slot 6

    public void SetCooldownSlo6(int cooldown)
    {
        cooldownTime6 = cooldown;
        cooldownTimer6 = cooldown;
    }

    public void CooldownSlot6()
    {
        cooldownTimer6 -= Time.deltaTime;

        if (hasItem6)
        {
            if (cooldownTimer6 < 0)
            {
                isCooldown6 = false;
                cooldownText6.gameObject.SetActive(false);
                cooldownFill6.fillAmount = 0;
                backgroundGlow6.SetActive(true);
                frameHighlight6.SetActive(true);
            }
            else
            {
                cooldownText6.gameObject.SetActive(true);
                cooldownText6.text = Mathf.RoundToInt(cooldownTimer6).ToString();
                cooldownFill6.fillAmount = cooldownTimer6 / cooldownTime6;
                backgroundGlow6.SetActive(false);
                frameHighlight6.SetActive(false);
            }
        }
    }

    #endregion
}
