using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseFunctionality : Singleton<PauseFunctionality>
{
    [Header("PlayerInventory")]
    public Image slotNumber1;
    public Image slotNumber2;
    public Image slotNumber3;
    public Animator anim;
    public GameObject pausePanel;

    [Header("TopStats")]
    public Image itemIcon;
    public TMP_Text itemName;


    [Header("CoreStats")]
    public TMP_Text damageValue;
    public TMP_Text critValue;
    public TMP_Text speedValue;


    [Header("Abilities")]
    public TMP_Text abilityName;
    public TMP_Text cdValue;
    public TMP_Text abilityDescription;

    public enum ItemType { Attack, Movement}
    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        anim = pausePanel.GetComponent<Animator>();
        UpdateStats(_PC.headItem);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeType()
    {
        switch(itemType)
        {
            case ItemType.Attack:
                //play transition for attacks
                //anim.SetBool("IsMove", false);
                break;

            case ItemType.Movement:
                //play transition for movements
                //anim.SetBool("IsMove", true);
                break;
        }
    }


    public void UpdateStats(Item _hoverItem)
    {
        itemName.text = _hoverItem.itemName;
        itemIcon.sprite = _hoverItem.icon;

        if(_hoverItem.segment == Item.Segment.Legs)
        {
            itemType = ItemType.Movement;
            speedValue.text = _hoverItem.movementSpeed.ToString();
            Debug.Log("isLegs");
            anim.Play("Pause_Movement");

            damageValue.text = "";
            critValue.text = damageValue.text = "";
            //Ability name change here
            //Ability description change here
            //Ability cooldown change here
        }
        else
        {
            itemType = ItemType.Attack;
            damageValue.text = _hoverItem.dmg.ToString();
            critValue.text = _hoverItem.critChance.ToString();
            Debug.Log("isNotLegs");
            anim.Play("Pause_Attack");

            speedValue.text = "";


        }

    }
        
        

    public void FixedUpdate()
    {
        
    }
}
