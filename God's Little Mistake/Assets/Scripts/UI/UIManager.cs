using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public Item leftArmItem;
    public Item rightArmItem;

    public GameObject gameOverMenu;

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

    [Header("Missy Height")]
    public GameObject playerAvatar;
    Vector3 standard = new Vector3(0.239999443f, 0.0800049901f, -0.555116713f);
    Vector3 tall = new Vector3(-0.0209999997f, -0.136000007f, 0.0540000014f);

    [Header("Item Hover Over Panel")]
    public GameObject statsPopUpPanel;
    public TMP_Text popupName;
    public TMP_Text popupDmg;
    public TMP_Text popupCritX;
    public TMP_Text popupCritChance;
    public TMP_Text popupFirerate;
    public Image popupIcon;

    [Header("Global Scroll UI")]
    public RectTransform scrollContent;
    public float scrollSpeed = 10f;


    [Header("Inventory Pop up")]
    public GameObject invenSegementPopUpPanel;
    public TMP_Text invenPopupName;
    public TMP_Text invenPopupDmg;
    public TMP_Text invenPopupCritX;
    public TMP_Text invenPopupCritChance;
    public TMP_Text invenPopupFirerate;
    public GameObject topEye;
    public GameObject middleEye;
    public GameObject bottomEye;
    public Image attackPill;
    public TMP_Text attackPillText;
    public Image attackIcon;
    public Image rangePill;
    public TMP_Text rangePillText;
    public Image rangeIcon;
    public Image itemIcon;
    public Image typeIcon;

    [Header("Inventory Comparison1")]
    public GameObject statComp1;
    public GameObject arrowComp;
    public TMP_Text popupName1;
    public TMP_Text popupDmg1;
    public TMP_Text popupCritX1;
    public TMP_Text popupCritChance1;
    public TMP_Text popupFirerate1;
    public Image popupIcon1;

    [Header("Animation")]
    public Animator hoverItemAnimator;
    public Animator hoverItemStatComp1Animator;
    public Animator hoverItemStatComp2Animator;

    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject pauseFunctionalityPanel;
    public GameObject optionPanel;


    [Header("Inventory Comparison2")]
    public GameObject statComp2;
    public TMP_Text popupName2;
    public TMP_Text popupDmg2;
    public TMP_Text popupCritX2;
    public TMP_Text popupCritChance2;
    public TMP_Text popupFirerate2;
    public Image popupIcon2;

    [Header("Icons")]
    public Sprite meleeIcon;
    public Sprite rangedIcon;
    public Sprite typeCone;
    public Sprite typeLine;
    public Sprite typeCircle;
    public Sprite typeRapid;
    public Sprite typeLob;
    public Sprite typeLaser;
    public Sprite typeCannon;





    private void Start()
    {   
        UpdateInventorySlotImages();
        heldItem = null;
        isHoldingItem = false;
        statsPopUpPanel.SetActive(false);
        statComp1.SetActive(false);
        arrowComp.SetActive(false);
        playerAvatar = GameObject.FindGameObjectWithTag("Player");

        hoverItemStatComp1Animator = statComp1.GetComponent<Animator>();
        hoverItemStatComp2Animator = statComp2.GetComponent<Animator>();

        //Pause Related
        //pausePanel.SetActive(false);
        //optionPanel.SetActive(false);

    }

    private void Update()
    {
        if(heldItem == null)
        {
            if (Input.GetKey(KeyCode.Mouse0)) cursor.sprite = cursorClick;
            else cursor.sprite = defaultCursor;
        }

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            Vector3 newPosition = scrollContent.localPosition + Vector3.up * scrollDelta * scrollSpeed;
            scrollContent.localPosition = newPosition;
        }


    }

    #region Pause
    public void OnPause()
    {
        pausePanel.SetActive(true);
    }

    public void OnResume()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void OptionsOpen()
    {
        optionPanel.SetActive(true);
        pauseFunctionalityPanel.SetActive(false);
        //change to options panel here
    }

    public void OptionsClose()
    {
        optionPanel.SetActive(false);
        pauseFunctionalityPanel.SetActive(true);
        //change to options panel here
    }

    public void OnExit()
    {
        pausePanel.SetActive(false); //temporary
        //Turn on death panel
        //Show score, time, etc.
    }

    


    #endregion

    public void UpdateItemPopUp(Item _hoverItem)
    {
        //ADD LATER FORMATTING FOR FLOATS

        invenPopupName.text = _hoverItem.itemName;
        invenPopupDmg.text = _hoverItem.dmg.ToString();
        invenPopupCritX.text = _hoverItem.critX.ToString();
        invenPopupCritChance.text = _hoverItem.critChance.ToString();
        invenPopupFirerate.text = _hoverItem.fireRate.ToString();
        popupIcon.sprite = _hoverItem.icon;

        //segment check
        if(_hoverItem.segment == Item.Segment.Head)
        {
            topEye.SetActive(true);
            middleEye.SetActive(false);
            bottomEye.SetActive(false);
        }
        if (_hoverItem.segment == Item.Segment.Torso)
        {
            topEye.SetActive(false);
            middleEye.SetActive(true);
            bottomEye.SetActive(false);
        }
        if (_hoverItem.segment == Item.Segment.Legs)
        {
            topEye.SetActive(false);
            middleEye.SetActive(false);
            bottomEye.SetActive(true);
        }

        //range or melee check
        if (_hoverItem.projectile == true)
        {
            attackIcon.sprite = rangedIcon;
            attackPill.color = Color.blue;
            rangePillText.text = _hoverItem.longRange_range.ToString();
            attackPillText.text = "Ranged";
        }
        else
        {
            attackIcon.sprite = meleeIcon;
            attackPill.color = Color.red;
            rangePillText.text = _hoverItem.melee_range.ToString();
            attackPillText.text = "Melee";

        }

        //type check
        if (_hoverItem.meleeAttackType == Item.AttackType.Line)
        {
            typeIcon.sprite = typeLine;
        }
        if (_hoverItem.meleeAttackType == Item.AttackType.Cone)
        {
            typeIcon.sprite = typeCone;
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
        if (_hoverItem.meleeAttackType == Item.AttackType.Cannon)
        {
            typeIcon.sprite = typeCannon;
        }
        if (_hoverItem.meleeAttackType == Item.AttackType.Laser)
        {
            typeIcon.sprite = typeLaser;
        }

        print("Update pop up");

        //Search for matches to item mouse is hovering over
        var matchItem = SearchForItemMatch(_hoverItem);

        //if (matchItem != null)
        //    print(matchItem[0]);
        //else print("no match");
    }

    public void PopupStat(Item _hoverItem)
    {
        UpdateItemPopUp(_hoverItem);

        Item itemSlot3 =new();
        Item itemSlot4 =new();

        foreach (var item in _PC.playerInventory)
        {
            if (item.inSlot == 3) itemSlot3 = item;
            if (item.inSlot == 4) itemSlot4 = item;
        }

        if(_hoverItem.inSlot == 3 || _hoverItem.inSlot == 4)
        {
            statComp1.SetActive(true);
            statComp2.SetActive(true);

            UpdateItemPopUpComp1(itemSlot3);
            UpdateItemPopUpComp2(itemSlot4);
        }
        else
        {
            statComp1.SetActive(true);
            statComp2.SetActive(false);

            Item itemMatch = new();

            foreach (var item in _PC.playerInventory)
            {
                if (item.category == _hoverItem.category) itemMatch = item;
            }

            UpdateItemPopUpComp1(itemMatch);
        }


    }

    //For comparison 1
    public void UpdateItemPopUpComp1(Item _itemInfo)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName1.text = _itemInfo.itemName;
        popupDmg1.text = _itemInfo.dmg.ToString();
        popupCritX1.text = _itemInfo.critX.ToString();
        popupCritChance1.text = _itemInfo.critChance.ToString();
        popupFirerate1.text = _itemInfo.fireRate.ToString();
        popupIcon1.sprite = _itemInfo.icon;

        




    }
    public void UpdateItemPopUpComp2(Item _itemInfo)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName2.text = _itemInfo.itemName;
        popupDmg2.text = _itemInfo.dmg.ToString();
        popupCritX2.text = _itemInfo.critX.ToString();
        popupCritChance2.text = _itemInfo.critChance.ToString();
        popupFirerate2.text = _itemInfo.fireRate.ToString();
        popupIcon2.sprite = _itemInfo.icon;



    }

    public void UpdateHealthText(float _hp)
    {
        hpText.text = _hp.ToString("F0"); //removes any decimals
    }
    public void UpdateLevelext(int _lvl)
    {
        //levelText.text = "Level " + _lvl.ToString();
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
                        _HUD.hasItem1 = false;
                    }
                    else
                    {
                        invenSlot0.sprite = _PC.playerInventory[i].icon; //images for icon
                        _HUD.hasItem1 = true;

                    }
                    break;

                case 1:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot1.sprite = emptySlotSprite;
                        _HUD.hasItem2 = false;
                    }
                    else
                    {
                        invenSlot1.sprite = _PC.playerInventory[i].icon; //images for icon
                        _HUD.hasItem2 = true;
                    }
                    break;

                case 2:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot2.sprite = emptySlotSprite;
                        _HUD.hasItem3 = false;
                    }
                    else
                    {
                        invenSlot2.sprite = _PC.playerInventory[i].icon; //images for icon
                        _HUD.hasItem3 = true;
                    }
                    break;

                case 3:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot3.sprite = emptySlotSprite;
                        _HUD.hasItem4 = false;
                    }
                    else
                    {
                        invenSlot3.sprite = _PC.playerInventory[i].icon;
                        _HUD.hasItem4 = true;
                    }
                    break;

                case 4:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot4.sprite = emptySlotSprite;
                        _HUD.hasItem5 = false;
                    }
                    else
                    {
                        invenSlot4.sprite = _PC.playerInventory[i].icon;
                        _HUD.hasItem5 = true;
                    }
                    break;

                case 5:
                    if (!(i >= -1 && i < _PC.playerInventory.Count))
                    {
                        invenSlot5.sprite = emptySlotSprite;
                        _HUD.hasItem6 = false;
                    }
                    else
                    {
                        invenSlot5.sprite = _PC.playerInventory[i].icon;
                        _HUD.hasItem6 = true;
                    }
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
        invenPopupFirerate.text = "Firerate%: " + _PC.playerInventory[_whichSlot].longRangeSpeed.ToString();

        
    }

    #endregion

    #region Item Equip
    /// <summary>
    /// Changes cursor to most recently picked up item
    /// </summary>
    /// <param name="_slot"></param>
    public void CreateItemSelected(Item _itemInfo)
    {
        print("Create item " + _itemInfo.itemName + " " + _itemInfo.ID);

        heldItem = _itemInfo;

        isHoldingItem = true;

        int slot = -1;

        switch(heldItem.segment)
        {
            case Item.Segment.Head:

                switch(heldItem.category)
                {
                    case Item.Category.Horns:
                        if (_AVTAR.slotsOnPlayerFront[0].transform.childCount == 0)
                        {
                            slot = 0;
                            heldItem.inSlot = slot;
                        }
                        break;
                    case Item.Category.Eyes:
                        if (_AVTAR.slotsOnPlayerFront[1].transform.childCount == 0)
                        {
                            slot = 1;
                            heldItem.inSlot = slot;
                        }
                        break;
                    case Item.Category.Mouth:
                        print("itws a mouth");

                        if (_AVTAR.slotsOnPlayerFront[2].transform.childCount == 0)
                        {
                            print("no childern so its 2");
                            slot = 2;
                            heldItem.inSlot = slot;
                        }
                        break;
                    
                }

                break;
            case Item.Segment.Torso:
                if (_AVTAR.slotsOnPlayerFront[3].transform.childCount == 0)
                {
                    slot = 3;
                }
                else if (_AVTAR.slotsOnPlayerFront[4].transform.childCount == 0)
                {
                    slot = 4;
                    heldItem.inSlot = slot;

                }
                break;
            case Item.Segment.Legs:
                if (_AVTAR.slotsOnPlayerFront[5].transform.childCount == 0)
                {
                    slot = 5;
                    heldItem.inSlot = slot;

                }
                break;  
        }


        if(slot != -1)
        {
            print(heldItem.itemName + " is on slot " + slot);
            heldItem.inSlot = slot;
            EquipImage(slot);

        }


    }

    /// <summary>
    /// Equips item to player inventory and resets cursor to default
    /// </summary>
    /// <param name="_slot"></param>
    public void EquipImage(int _slot)
    {


        //print("Creating item for slot " + _slot);

        //print("in EquipImage ID is " + heldItem.ID);


        var itemExsists = false;
        foreach (var item in _PC.playerInventory)
        {
            if (item == heldItem) itemExsists = true;
        }

        if (itemExsists == false) _ISitemD.AddItemToInventory(heldItem);



        var itemLeftSide = Instantiate(heldItem.avtarPrefabLeft, _AVTAR.slotsOnPlayerLeft[_slot].transform);
        var itemRightSide = Instantiate(heldItem.avtarPrefabRight, _AVTAR.slotsOnPlayerRight[_slot].transform);
        _PC.itemsAnimLeftSide.Add(itemLeftSide.GetComponentInChildren<Animator>());
        _PC.itemsAnimRightSide.Add(itemRightSide.GetComponentInChildren<Animator>());

        //if torso piece make sure its correct

        if (heldItem.segment == Item.Segment.Torso)
        {
            if (_slot == 4) //  RIGHT
            {
                //FRONT
                var itemFront = Instantiate(heldItem.avatarPrefabFrontRight, _AVTAR.slotsOnPlayerFront[_slot].transform);
                _PC.itemsAnimForward.Add(itemFront.GetComponentInChildren<Animator>());

                //BACKL
                var itemBackSide = Instantiate(heldItem.avtarPrefabBackLeft, _AVTAR.slotsOnPlayerBack[_slot].transform);
                _PC.itemsAnimBack.Add(itemBackSide.GetComponentInChildren<Animator>());

            }
            if (_slot == 3) // LEFT
            {
                //FRONT
                var itemFront = Instantiate(heldItem.avatarPrefabFrontLeft, _AVTAR.slotsOnPlayerFront[_slot].transform);
                _PC.itemsAnimForward.Add(itemFront.GetComponentInChildren<Animator>());


                var itemBackSide = Instantiate(heldItem.avtarPrefabBackRight, _AVTAR.slotsOnPlayerBack[_slot].transform);
                _PC.itemsAnimBack.Add(itemBackSide.GetComponentInChildren<Animator>());

                //BACKK

            }

        }
        else
        {
            //default left
            var itemFront = Instantiate(heldItem.avatarPrefabFrontLeft, _AVTAR.slotsOnPlayerFront[_slot].transform);
            _PC.itemsAnimForward.Add(itemFront.GetComponentInChildren<Animator>());

            var itemBackSide = Instantiate(heldItem.avtarPrefabBackLeft, _AVTAR.slotsOnPlayerBack[_slot].transform);
            _PC.itemsAnimBack.Add(itemBackSide.GetComponentInChildren<Animator>());
        }

        if (heldItem.segment == Item.Segment.Legs) _PC.UpdateLegAnimators();


        CheckHeight();
        _PIA.PassiveAbilityItemCheck();

        // (flip) itemFront.transform.localScale = new Vector3(-itemFront.transform.rotation.x, itemFront.transform.rotation.y, itemFront.transform.rotation.z);

        //FOR ALL OTHER ITEMS

        cursor.sprite = defaultCursor;
        heldItem = null;
        isHoldingItem = false;
        //rotate image

        //_PC.CloseSlots();
    }


    public void CheckHeight()
    {
        //find leg item
        for (int i = 0; i < _PC.playerInventory.Count; i++)
        {
            if (_PC.playerInventory[i].segment == Item.Segment.Legs)
            {
                //6 tripod legs, 10 hoover
                if (_PC.playerInventory[i].ID == 10 || _PC.playerInventory[i].ID == 6)
                {
                    print("Go taller");
                    playerAvatar.transform.position = new Vector3(playerAvatar.transform.position.x, tall.y, playerAvatar.transform.position.z);
                }
                else
                {
                    playerAvatar.transform.position = new Vector3(playerAvatar.transform.position.x, standard.y, playerAvatar.transform.position.z);
                }
            }
        } 

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


    public List<Item> SearchForItemMatch(Item _hoverItem)
    {
        List<Item> itemMatchInPlayerInven = new();

        //print("Hover item is " + _hoverItem.itemName);

        foreach (var item in _PC.playerInventory)
        {
            if(item.segment == _hoverItem.segment)
            {
                itemMatchInPlayerInven.Add(item);
            }

            //var icon = item.icon;

        }

        foreach (var item in itemMatchInPlayerInven)
        {

        }
        if (itemMatchInPlayerInven.Count == 0) print("No Matches found");


        return itemMatchInPlayerInven;
    }

    #region HUD

    

    #endregion


    #region Indicators

    //public void TopSegmentIndicator()
    //{
    //    topEye.SetActive(true);
    //    topEye.GetComponent<SpriteRenderer>().color = Color.yellow;
    //    middleEye.SetActive(false);
    //    bottomEye.SetActive(false);
    //}

    //public void MiddleSegmentIndicator()
    //{
    //    middleEye.SetActive(true);
    //    middleEye.GetComponent<SpriteRenderer>().color = Color.red;
    //    topEye.SetActive(false);
    //    bottomEye.SetActive(false);

    //}

    //public void BottomSegmentIndicator()
    //{
    //    bottomEye.SetActive(true);
    //    bottomEye.GetComponent<SpriteRenderer>().color = Color.red;
    //    topEye.SetActive(false);
    //    middleEye.SetActive(false);
    //}

    //public void AttackPillChange(int num)
    //{
    //    attackPill.SetActive(true);
    //    if (num == 1)
    //    {
    //        attackPillText.text = "Melee";
    //        //attackIcon.SetActive(true); change the icon
    //        attackPill.GetComponent<SpriteRenderer>().color = Color.red;
    //    }
    //    if (num == 2) 
    //    {
    //        attackPillText.text = "Range";
    //        //attackIcon.SetActive(true); change the icon
    //        attackPill.GetComponent<SpriteRenderer>().color = Color.red;
    //    }
    //}

    //public void RangePillChange(int num)
    //{
    //    rangePill.SetActive(true);
    //    attackPillText.text = num.ToString();
    //}

    //public void ChangeItemIcon()
    //{
    //    //Chnage the item icons
    //}

    //public void ChangeItemType()
    //{
    //    //Chnage the type icons
    //}

    #endregion



}