using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PauseFunctionality : GameBehaviour
{
    [Header("PlayerInventory")] //i dont know if this is needed
    public int slotNumber1;
    public int slotNumber2;
    public int slotNumber3;
    public int slotNumber4;
    public int slotNumber5;
    public int slotNumber6;

    [Header("TopStats")]
    public Sprite itemIcon;
    public Sprite typeIcon;
    public Sprite effectIcon;
    public TMP_Text itemName;

    [Header("CoreStats")]
    public TMP_Text itemDamage;
    public TMP_Text itemCritMultiplier;
    public TMP_Text itemCritChance;
    public TMP_Text itemFireRate;
    public Sprite attackPill;
    public Sprite attackIcon;
    public TMP_Text attackText;
    public Sprite rangePill;
    public Sprite rangeIcon;
    public TMP_Text rangeText;

    [Header("Abilities")]
    public TMP_Text abilityName;
    public string abilityDescription = "";

    [Header("Icons")]
    public List<Sprite> itemTypeIcons;
    public List<Sprite> itemEffectIcons;
    public List<Sprite> itemAttackEffects;

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

    public void UpdateStats(PauseSlotStats _hoverItem)
    {
        itemName.text = _hoverItem.itemName;
        itemDamage.text = _hoverItem.dmg.ToString();
        itemCritMultiplier.text = _hoverItem.critX.ToString();
        itemCritChance.text = _hoverItem.critChance.ToString();
        itemFireRate.text = _hoverItem.fireRate.ToString();
        itemIcon = _hoverItem.icon;


        //var matchItem = SearchForItemMatch(_hoverItem);

        //if (matchItem != null)
        //    print(matchItem[0]);
        //else print("no match");
    }

    public void FixedUpdate()
    {
        
    }

    public List<Item> SearchForItemMatch(Item _hoverItem)
    {
        List<Item> itemMatchInPlayerInven = new();

        print("Hover item is " + _hoverItem.itemName);

        foreach (var item in _PC.playerInventory)
        {
            if (item.segment == _hoverItem.segment)
            {
                itemMatchInPlayerInven.Add(item);
            }
            var icon = item.icon;
        }
        return itemMatchInPlayerInven;

    }
}
