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

    [Header("Ability Slot 3")]
    public Image invenSlot3;
    public Image frame3;
    public GameObject frameHighlight3;
    public GameObject keyHighlight3;
    public Image cooldownFill3;
    public TMP_Text cooldownText3;
    public GameObject backgroundGlow3;
    public GameObject keyGlow3;

    [Header("Ability Slot 4")]
    public Image invenSlot4;
    public Image frame4;
    public GameObject frameHighlight4;
    public GameObject keyHighlight4;
    public Image cooldownFill4;
    public TMP_Text cooldownText4;
    public GameObject backgroundGlow4;
    public GameObject keyGlow4;

    [Header("Ability Slot 5")]
    public Image invenSlot5;
    public Image frame5;
    public GameObject frameHighlight5;
    public GameObject keyHighlight5;
    public Image cooldownFill5;
    public TMP_Text cooldownText5;
    public GameObject backgroundGlow5;
    public GameObject keyGlow5;

    [Header("Ability Slot 6")]
    public Image invenSlot6;
    public Image frame6;
    public GameObject frameHighlight6;
    public GameObject keyHighlight6;
    public Image cooldownFill6;
    public TMP_Text cooldownText6;
    public GameObject backgroundGlow6;
    public GameObject keyGlow6;


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

        SetCooldownSlo1(5);
        SetCooldownSlo2(5);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {

            isCooldown1 = true;
            isCooldown2 = true;
        }

        if(isCooldown1 == true)
        {
            CooldownSlot1();
        }

        if (isCooldown2 == true)
        {
            CooldownSlot2();
        }
    }


    #region Slot 1
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

    public void SetCooldownSlo1(int cooldown)
    {
        cooldownTime1 = cooldown;
        cooldownTimer1 = cooldown;
    }

    public void CooldownSlot1()
    {
        cooldownTimer1 -= Time.deltaTime;

        if (cooldownTimer1 < 0)
        {
            isCooldown1 = false;
            cooldownText1.gameObject.SetActive(false);
            cooldownFill1.fillAmount = 0;
            backgroundGlow1.SetActive(true);
        }
        else
        {
            cooldownText1.text = Mathf.RoundToInt(cooldownTimer1).ToString();
            cooldownFill1.fillAmount = cooldownTimer1/cooldownTime1;
            backgroundGlow1.SetActive(false);
        }
    }

    public void StartCooldownFillSlot1(int maxValue, int cooldown)
    {
        cooldownFill1.fillAmount = cooldown / maxValue;
    }

    public void ChangeFrame1()
    {


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

        if (cooldownTimer2 < 0)
        {
            isCooldown2 = false;
            cooldownText2.gameObject.SetActive(false);
            cooldownFill2.fillAmount = 0;
            backgroundGlow2.SetActive(true);
        }
        else
        {
            cooldownText2.text = Mathf.RoundToInt(cooldownTimer2).ToString();
            cooldownFill2.fillAmount = cooldownTimer2 / cooldownTime2;
            backgroundGlow2.SetActive(false);
        }
    }

    #endregion

    #region Slot 3

    public void HUDBoxHighlightFrame3(bool activeItem)
    {
        if (activeItem == true)
        {
            frameHighlight3.SetActive(true);
        }
        else
        {
            frameHighlight3.SetActive(false);
        }
    }

    public void HUDBoxHighlightKey3(bool activeItem)
    {
        if (activeItem == true)
        {
            keyHighlight3.SetActive(true);
        }
        else
        {
            keyHighlight3.SetActive(false);
        }
    }

    public void CooldownTextSlot3(int cooldown)
    {
        if (cooldown == 0)
        {
            cooldownText3.text = "";
        }
        else
        {
            cooldownText3.text = "";
            cooldownText3.text = cooldown.ToString();
        }
    }

    #endregion
}
