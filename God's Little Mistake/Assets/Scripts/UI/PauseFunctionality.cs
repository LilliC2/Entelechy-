using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseFunctionality : Singleton<PauseFunctionality>
{
    [Header("PlayerInventory")] //i dont know if this is needed
    public GameObject slotNumber1;
    public GameObject slotNumber2;
    public UnityEngine.UI.Image slotNumber3;
    public GameObject slotNumber4;
    public GameObject slotNumber5;
    public GameObject slotNumber6;

    [Header("TopStats")]
    public UnityEngine.UI.Image typeIcon;
    public UnityEngine.UI.Image itemIcon;
    public UnityEngine.UI.Image effectIcon;
    public TMP_Text itemName;


    [Header("CoreStats")]
    public TMP_Text itemDamage;
    public TMP_Text itemCritMultiplier;
    public TMP_Text itemCritChance;
    public TMP_Text itemFireRate;
    public UnityEngine.UI.Image attackPill;
    public UnityEngine.UI.Image attackIcon;
    public TMP_Text attackText;
    public UnityEngine.UI.Image distancePill;
    public UnityEngine.UI.Image distanceIcon;
    public TMP_Text distanceText;

    [Header("Abilities")]
    public TMP_Text abilityName;
    public TMP_Text abilityDescription;

    [Header("Icons")]
    public Sprite meleeIcon;
    public Sprite rangeIcon;
    public Sprite typeCone;
    public Sprite typeLine;
    public Sprite typeCircle;
    public Sprite typeRapid;
    public Sprite typeLob;
    public Sprite typeLaser;
    public Sprite typeCannon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausingFucntion()
    {
        _GM.OnPause(); //if needed to be use from the animations
    }

    public void SearchSlot()
    {
        //_AVTAR.slotsOnCanvas[slotNumber];

        //_PC.playerInventory.(slotNumber)

        //_PC.playerInventory.Clear();
    }


    public void UpdateStats(Item _hoverItem)
    {
        itemName.text = _hoverItem.itemName;
        itemDamage.text = _hoverItem.dmg.ToString();
        itemCritMultiplier.text = _hoverItem.critX.ToString();
        itemCritChance.text = _hoverItem.critChance.ToString();
        itemFireRate.text = _hoverItem.fireRate.ToString();
        itemIcon.sprite = _hoverItem.icon;

        if(_hoverItem.segment != Item.Segment.Legs)
        {

            attackPill.gameObject.SetActive(true);
            distancePill.gameObject.SetActive(true);

            


            if (_hoverItem.projectile == true)
            {
                attackIcon.sprite = rangeIcon;
                attackPill.color = Color.blue;
                distanceText.text = _hoverItem.longRange_range.ToString();
                attackText.text = "Ranged";
            }
            else
            {
                attackIcon.sprite = meleeIcon;
                attackPill.color = Color.red;
                distanceText.text = _hoverItem.melee_range.ToString();
                attackText.text = "Melee";
            }
        }
        else
        {
            attackPill.gameObject.SetActive(false);
            distancePill.gameObject.SetActive(false);
        }

        if(_hoverItem.meleeAttackType == Item.AttackType.Cone) 
        {
            typeIcon.sprite = typeCone;
        }

        if (_hoverItem.meleeAttackType == Item.AttackType.Line)
        {
            typeIcon.sprite = typeLine;
        }

        if (_hoverItem.meleeAttackType == Item.AttackType.Circle)
        {
            typeIcon.sprite = typeCircle;
        }

        if (_hoverItem.meleeAttackType == Item.AttackType.Rapid)
        {
            typeIcon.sprite = typeRapid;
        }

        if (_hoverItem.meleeAttackType == Item.AttackType.Lob)
        {
            typeIcon.sprite = typeLob;
        }

    }
        
        

    public void FixedUpdate()
    {
        
    }
}
