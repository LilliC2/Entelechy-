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

    [Header("Player Feedback")]
    public TMP_Text hpText;
    public TMP_Text levelText;

    [Header("Equip")]
    public Sprite defaultCursor;
    public Image cursor;
    public Sprite cursorClick;
    public GameObject canvas;
    public GameObject playerCanvas;
    public Item heldItem = null;
    bool canEquip;
    float rotation;
    public bool isHoldingItem;

    [Header("Inventory Images")]
    public Sprite emptySlotSprite;
    public Image invenSlot0;
    public Image invenSlot1;
    public Image invenSlot2;
    public Image invenSlot3;
    public Image invenSlot4;
    public Image invenSlot5;
    public Image invenSlot6;

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
        heldItem = null;
        isHoldingItem = false;
    }

    private void Update()
    {
        if(heldItem == null)
        {
            if (Input.GetKey(KeyCode.Mouse0)) cursor.sprite = cursorClick;
            else cursor.sprite = defaultCursor;
        }

        
    }

    public void UpdateItemPopUp(string _itemName, float _itemDmg, float _itemCritX, float _itemCritChance, float _itemFirerate)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName.text = _itemName;
        popupDmg.text = _itemDmg.ToString();
        popupCritX.text = _itemCritX.ToString();
        popupCritChance.text = _itemCritChance.ToString();
        popupFirerate.text = _itemFirerate.ToString();
    }

    public void UpdateHealthText(float _hp)
    {
        hpText.text = _hp.ToString("F0"); //removes any decimals
    }
    public void UpdateLevelext(int _lvl)
    {
        levelText.text = _lvl.ToString();
    }

    #region Inventory Item Stats Popup

    public void InvenSegmentPopUp()
    {
        var rt = invenSegementPopUpPanel.GetComponent<RectTransform>();
        //print(rt.anchoredPosition);
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
        for (int i = 0; 7 > i; i++)
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
        invenPopupName.text = _PC.playerInventory[_whichSlot].itemName;
        invenPopupDmg.text = "Dmg: " + _PC.playerInventory[_whichSlot].dmg.ToString();
        invenPopupCritX.text = "CritX: " + _PC.playerInventory[_whichSlot].critX.ToString();
        invenPopupCritChance.text = "Crit%: " + _PC.playerInventory[_whichSlot].critChance.ToString();
        invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].fireRate.ToString();
    }

    #endregion

    #region Item Equip
    /// <summary>
    /// Changes cursor to most recently picked up item
    /// </summary>
    /// <param name="_slot"></param>
    public void CreateItemSelected(int _inSceneId)
    {
        print("Create item, id is " + _inSceneId);
        heldItem = _ISitemD.inSceneItemDataBase[_inSceneId];

        //Sprite itemSprite = GameObject.Instantiate(_ISitemD.inSceneItemDataBase[_inSceneId].icon, canvas.transform);
        //cursor.sprite = itemSprite;


        print(heldItem);
        
        statsPopUpPanel.SetActive(false);
        isHoldingItem = true;

        int slot = 0;

        //find segement
        if(heldItem.segment == Item.Segment.Head)
        {
            //find category
            if (heldItem.category == Item.Category.Horns)
            {
                //check if slot is free
                if (_AVTAR.slotsOnPlayer[0].transform.childCount == 0)
                    slot = 0;   
            }
            if (heldItem.category == Item.Category.Eyes)
            {
                if (_AVTAR.slotsOnPlayer[1].transform.childCount == 0)
                    slot = 1;
            }
            if (heldItem.category == Item.Category.Mouth)
            {
                if (_AVTAR.slotsOnPlayer[2].transform.childCount == 0)
                    slot = 2;
            }
        }
        else if (heldItem.segment == Item.Segment.Torso)
        {
            if (_AVTAR.slotsOnPlayer[3].transform.childCount == 0)
            {
                slot = 3;
            }
            else if (_AVTAR.slotsOnPlayer[4].transform.childCount == 0)
            {
                slot = 4;
            }

        }
        else if (heldItem.segment == Item.Segment.Legs)
        {
            if (_AVTAR.slotsOnPlayer[5].transform.childCount == 0)
                slot = 5;
        }

        EquipImage(slot);

        //check category

            //check if slots in cateogry are free

            //if free then add it


    }

    /// <summary>
    /// Equips item to player inventory and resets cursor to default
    /// </summary>
    /// <param name="_slot"></param>
    public void EquipImage(int _slot)
    {
        bool flip = false;

        if (_slot == 4) flip = true;

        print("Creating item for slot " + _slot);

        _ISitemD.AddItemToInventory(heldItem.inSceneID);
        //then instantiate prefab on player and detroy this iamge

        var itemSide = Instantiate(heldItem.avtarPrefab, _AVTAR.slotsOnPlayer[_slot].transform);

        _PC.itemsAnim.Add(itemSide.GetComponentInChildren<Animator>());

        if (flip) itemSide.transform.localScale = new Vector3(-itemSide.transform.rotation.x, itemSide.transform.rotation.y, itemSide.transform.rotation.z);

        //FOR ALL OTHER ITEMS

        cursor.sprite = defaultCursor;
        heldItem = null;
        isHoldingItem = false;
        //rotate image

        _PC.CloseSlots();
    }

    /// <summary>
    /// Checks whether held item can be placed on slot that is hovered over
    /// </summary>
    /// <param name="_slot"></param>
    //public void CheckSlotHover(int _slot)
    //{

    //    if (cursor.sprite != defaultCursor && cursor.sprite != cursorClick)
    //    {
    //        print(heldItem.itemName);

    //        if (_AVTAR.slotsOnPlayer[_slot].transform.childCount == 0) //check if child object is there
    //        {
    //            //check right segment if slot from 1 to 2 the head etc. 

    //            if (_AVTAR.slotsOnPlayer[_slot].name.Contains(heldItem.segment.ToString()))
    //            {
    //                canEquip = true;
    //            }
    //            else
    //            {
    //                canEquip = false;
    //                //_AVTAR.slotsOnCanvas[_slot].GetComponent<Image>().color = Color.red;
    //            }
    //        }
    //        else
    //        {
    //            canEquip = false;
    //            //_AVTAR.slotsOnCanvas[_slot].GetComponent<Image>().color = Color.red;
    //        }
    //    }
    //}

    //public void DropHeldItem()
    //{

    //    print("Drop held item");
    //    //change cursor
    //    cursor.sprite = defaultCursor;

    //    //create item on player
    //    var item = Instantiate(_IG.itemTemp, GameObject.Find("Player").transform.position, Quaternion.identity);
    //    _ISitemD.inSceneItemDataBase.Add(heldItem);

    //    item.GetComponent<ItemIdentifier>().id = heldItem.inSceneID;
    //    var id = item.GetComponent<ItemIdentifier>().id;

    //    print("item dropping id = " + id);
    //    print(_ISitemD.inSceneItemDataBase[id].icon.name);
    //    //ERROR
    //    item.GetComponentInChildren<SpriteRenderer>().sprite = _ISitemD.inSceneItemDataBase[id].icon;

    //    //add to scene array
    //    int index = _ISitemD.inSceneItemDataBase.Count - 1;
    //    _ISitemD.inSceneItemDataBase[index].inSceneID = index;

    //    heldItem = null;
    //    isHoldingItem = false;
    //}
        
    /// <summary>
    /// Changes colour of slots when mouse exits hover
    /// </summary>
    /// <param name="_slot"></param>
    //public void CheckSlotHoverExit(int _slot)
    //{
    //    //_AVTAR.slotsOnCanvas[_slot].GetComponent<Image>().color = Color.yellow;
    //}

    #endregion

}