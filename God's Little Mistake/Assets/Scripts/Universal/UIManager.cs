using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    
    public TMP_Text playerHPText;
    public TMP_Text roomLevelText;

    [Header("Equip")]
    public Sprite defaultCursor;
    public Image cursor;
    public GameObject canvas;
    public GameObject playerCanvas;
    public Item heldItem;

    [Header("Inventory Images")]
    public Sprite emptySlotSprite;
    public Image invenSlot0;
    public Image invenSlot1;
    public Image invenSlot2;
    public Image invenSlot3;
    public Image invenSlot4;
    public Image invenSlot5;

    [Header("Item Hover Over Panel")]
    public GameObject statsPopUpPanel;
    public TMP_Text popupName;
    public TMP_Text popupDmg;
    public TMP_Text popupCritX;
    public TMP_Text popupCritChance;
    public TMP_Text popupFirerate;

    [Header("Inventory Pop up")]
    public GameObject invenSegementPopUpPanel;
    public TMP_Text invenPopupName;
    public TMP_Text invenPopupDmg;
    public TMP_Text invenPopupCritX;
    public TMP_Text invenPopupCritChance;
    public TMP_Text invenPopupFirerate;


    private void Start()
    {
        UpdateInventorySlotImages();
    }

    public void UpdateItemPopUp(string _itemName, float _itemDmg, float _itemCritX, float _itemCritChance, float _itemFirerate)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName.text = _itemName;
        popupDmg.text = "DMG: " + _itemDmg.ToString();
        popupCritX.text = "CritX: " + _itemCritX.ToString();
        popupCritChance.text = "Crit%: " + _itemCritChance.ToString();
        popupFirerate.text = "Firerate%: " + _itemFirerate.ToString();
    }

    #region Inventory Item Stats Popup

    public void InvenSegmentPopUp()
    {
        var rt = invenSegementPopUpPanel.GetComponent<RectTransform>();
        print(rt.anchoredPosition);
        rt.anchoredPosition = new Vector2(522.75f, -400.00f);
        //headSegementPopUpPanel.transform.DOMove(new Vector3(234.489014f, -343f, 0), 1); //COULD ADD EASE HERE;
    }
    public void InvenSegmentPopDown()
    {
        var rt = invenSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(522.75f, -561.85f);
    }

    public void UpdateInventorySlotImages()
    {
        for (int i = 0; 6 > i; i++)
        {
            switch (i)
            {
                case 0:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot0.sprite = emptySlotSprite;
                    }
                    else invenSlot0.sprite = _PC.playerInventory[i].icon; //images for icon
                    break;

                case 1:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot1.sprite = emptySlotSprite;
                    }
                    else
                    {
                        invenSlot1.sprite = _PC.playerInventory[i].icon; //images for icon
                        
                    }
                    break;

                case 2:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot2.sprite = emptySlotSprite;
                    }
                    else
                    {
                        invenSlot2.sprite = _PC.playerInventory[i].icon; //images for icon
                        invenSlot2.GetComponentInParent<Transform>().localScale = new Vector3(-1, 1, 1);
                    }
                    break;

                case 3:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot3.sprite = emptySlotSprite;
                    }
                    else invenSlot3.sprite = _PC.playerInventory[i].icon; 
                    break;

                case 4:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot4.sprite = emptySlotSprite;
                    }
                    else invenSlot4.sprite = _PC.playerInventory[i].icon; 
                    break;

                case 5:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot5.sprite = emptySlotSprite;
                    }
                    else invenSlot5.sprite = _PC.playerInventory[i].icon; 
                    break;
            }


        }


    }

    public void InventorySlotHover(int _whichSlot)
    {
        switch (_whichSlot)
        {
            case 0:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();

                break;
            case 1:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
                break;
            case 2:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
                break;
            case 3:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
                break;
            case 4:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
                break;
            case 5:
                invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
                invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
                invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
                invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
                invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
                break;
        }
    }


    #endregion

    #region Item Equip

    public void CreateItemSelected(int _inSceneId)
    {
        Sprite itemSprite = GameObject.Instantiate(_ISitemD.inSceneItemDataBase[_inSceneId].icon, canvas.transform);
        cursor.sprite = itemSprite;

        heldItem = _ISitemD.inSceneItemDataBase[_inSceneId];

    }

    public void EquipImage(int _slot)
    {
        //might not need _avatar just hold it on playercontroller
        if (_AVTAR.slotsOnPlayer[_slot].transform.childCount == 0) //check if child object is there
        {
            //check right segment if slot from 1 to 2 the head etc. 

            if (_AVTAR.slotsOnPlayer[_slot].name.Contains(heldItem.segment.ToString()))
            {
                _ISitemD.AddItemToInventory(heldItem.inSceneID);
                //then instantiate prefab on player and detroy this iamge
                var item = Instantiate(heldItem.avtarPrefab, _AVTAR.slotsOnPlayer[_slot].transform);
                item.name = item.name + heldItem.ID;

                cursor.sprite = defaultCursor;

                
            }

        }
        else print("slot taken");
    }

    #endregion

}