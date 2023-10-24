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

    public TMP_Text dungeonLevel;

    [Header("Hover Popup Content")]
    public GameObject popupContent;
    public GameObject popupContent1;
    public GameObject popupContent2;

    [Header("Game Over")]
    public GameObject gameOverMenu;
    public Animator gameOverAnim;


    [Header("Player Feedback")]
    public TMP_Text hpText;
    public TMP_Text levelText;
    public Image healhBar;

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

    //[Header("Item Hover Over Panel")]
    //public GameObject statsPopUpPanel;
    //public TMP_Text popupName;
    //public TMP_Text popupDmg;
    //public TMP_Text popupCritX;
    //public TMP_Text popupCritChance;
    //public TMP_Text popupFirerate;
    //public Image popupIcon;

    [Header("Global Scroll UI")]
    public RectTransform scrollContent;
    public float scrollSpeed = 10f;


    [Header("Inventory Pop up")]
    public GameObject statPop;
    public TMP_Text popupName;
    public TMP_Text popupDmg;
    public TMP_Text popupCritX;
    public TMP_Text popupCritChance;
    public TMP_Text popupFirerate;
    public GameObject topEye;
    public GameObject middleEye;
    public GameObject bottomEye;
    public Image attackPill;
    public TMP_Text attackPillText;
    public Image attackIcon;
    public Image rangePill;
    public TMP_Text rangePillText;
    public Image rangeIcon;
    public Image popupIcon;
    public Image typeIcon;
    public GameObject arrowComp;

    [Header("Inventory Comparison1")]
    public GameObject statComp1;
    public TMP_Text popupName1;
    public TMP_Text popupDmg1;
    public TMP_Text popupCritX1;
    public TMP_Text popupCritChance1;
    public TMP_Text popupFirerate1;
    public GameObject topEye1;
    public GameObject middleEye1;
    public GameObject bottomEye1;
    public Image attackPill1;
    public TMP_Text attackPillText1;
    public Image attackIcon1;
    public Image rangePill1;
    public TMP_Text rangePillText1;
    public Image rangeIco1;
    public Image popupIcon1;
    public Image typeIcon1;

    [Header("Inventory Comparison2")]
    public GameObject statComp2;
    public TMP_Text popupName2;
    public TMP_Text popupDmg2;
    public TMP_Text popupCritX2;
    public TMP_Text popupCritChance2;
    public TMP_Text popupFirerate2;
    public GameObject topEye2;
    public GameObject middleEye2;
    public GameObject bottomEye2;
    public Image attackPill2;
    public TMP_Text attackPillText2;
    public Image attackIcon2;
    public Image rangePill2;
    public TMP_Text rangePillText2;
    public Image rangeIco2;
    public Image popupIcon2;
    public Image typeIcon2;

    [Header("Animation")]
    public Animator hoverItemAnimator;
    public Animator hoverItemStatComp1Animator;
    public Animator hoverItemStatComp2Animator;

    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject pauseFunctionalityPanel;
    public GameObject optionPanel;
    public GameObject ingameUI;



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
        statPop.SetActive(false);
        statComp1.SetActive(false);
        arrowComp.SetActive(false);
        playerAvatar = GameObject.FindGameObjectWithTag("Player");

        hoverItemAnimator = statPop.GetComponent<Animator>();
        hoverItemStatComp1Animator = statComp1.GetComponent<Animator>();
        hoverItemStatComp2Animator = statComp2.GetComponent<Animator>();

        gameOverAnim = gameOverMenu.GetComponent<Animator>();

        //Pause Related
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);

        //Game Over Related
        gameOverMenu.SetActive(false);

        //healthbar related
        healhBar.fillAmount = 1;

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
        ingameUI.SetActive(false);
    }

    public void OnResume()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        ingameUI.SetActive(true);
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
        pausePanel.SetActive(false);
        ingameUI.SetActive(true);
        //temporary
        //Turn on death panel
        //Show score, time, etc.
    }




    #endregion


    #region Popups


    public void UpdateItemPopUp(Item _hoverItem)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName.text = _hoverItem.itemName;
        popupDmg.text = _hoverItem.dmg.ToString();
        popupCritX.text = _hoverItem.critX.ToString();
        popupCritChance.text = _hoverItem.critChance.ToString();
        popupFirerate.text = _hoverItem.firerate.ToString();
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
            rangePillText.text = _hoverItem.projectile_range.ToString();
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
        if (_hoverItem.attackType == Item.AttackType.Line)
        {
            typeIcon.sprite = typeLine;
        }
        if (_hoverItem.attackType == Item.AttackType.Cone)
        {
            typeIcon.sprite = typeCone;
        }
        if (_hoverItem.attackType == Item.AttackType.Circle)
        {
            typeIcon.sprite = typeCircle;
        }
        if (_hoverItem.attackType == Item.AttackType.Rapid)
        {
            typeIcon.sprite = typeRapid;
        }
        if (_hoverItem.attackType == Item.AttackType.Lob)
        {
            typeIcon.sprite = typeLob;
        }
        if (_hoverItem.attackType == Item.AttackType.Cannon)
        {
            typeIcon.sprite = typeCannon;
        }
        if (_hoverItem.attackType == Item.AttackType.Laser)
        {
            typeIcon.sprite = typeLaser;
        }

        print("Update pop up");

 
    }

    public void PopupStat(Item _hoverItem)
    {
        UpdateItemPopUp(_hoverItem);
        statComp1.SetActive(true);
        statComp2.SetActive(false);

        Item itemMatch = new();

        switch (_hoverItem.segment)
        {
            case Item.Segment.Head:
                itemMatch = _PC.headItem;
                break;
            case Item.Segment.Torso:
                itemMatch = _PC.torsoItem;

                break;
            case Item.Segment.Legs:
                itemMatch = _PC.legItem;

                break;

        }

        UpdateItemPopUpComp1(itemMatch);


    }

    //For comparison 1
    public void UpdateItemPopUpComp1(Item _itemInfo)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName1.text = _itemInfo.itemName;
        popupDmg1.text = _itemInfo.dmg.ToString();
        popupCritX1.text = _itemInfo.critX.ToString();
        popupCritChance1.text = _itemInfo.critChance.ToString();
        popupFirerate1.text = _itemInfo.firerate.ToString();
        popupIcon1.sprite = _itemInfo.icon;

        //segment check
        if (_itemInfo.segment == Item.Segment.Head)
        {
            topEye1.SetActive(true);
            middleEye1.SetActive(false);
            bottomEye1.SetActive(false);
        }
        if (_itemInfo.segment == Item.Segment.Torso)
        {
            topEye1.SetActive(false);
            middleEye1.SetActive(true);
            bottomEye1.SetActive(false);
        }
        if (_itemInfo.segment == Item.Segment.Legs)
        {
            topEye1.SetActive(false);
            middleEye1.SetActive(false);
            bottomEye1.SetActive(true);
        }

        //range or melee check
        if (_itemInfo.projectile == true)
        {
            attackIcon1.sprite = rangedIcon;
            attackPill1.color = Color.blue;
            rangePillText1.text = _itemInfo.projectile_range.ToString();
            attackPillText1.text = "Ranged";
        }
        else
        {
            attackIcon1.sprite = meleeIcon;
            attackPill1.color = Color.red;
            rangePillText1.text = _itemInfo.melee_range.ToString();
            attackPillText1.text = "Melee";

        }

        //type check
        if (_itemInfo.attackType == Item.AttackType.Line)
        {
            typeIcon1.sprite = typeLine;
        }
        if (_itemInfo.attackType == Item.AttackType.Cone)
        {
            typeIcon1.sprite = typeCone;
        }
        if (_itemInfo.attackType == Item.AttackType.Circle)
        {
            typeIcon1.sprite = typeCircle;
        }
        if (_itemInfo.attackType == Item.AttackType.Rapid)
        {
            typeIcon1.sprite = typeRapid;
        }
        if (_itemInfo.attackType == Item.AttackType.Lob)
        {
            typeIcon1.sprite = typeLob;
        }
        if (_itemInfo.attackType == Item.AttackType.Cannon)
        {
            typeIcon1.sprite = typeCannon;
        }
        if (_itemInfo.attackType == Item.AttackType.Laser)
        {
            typeIcon1.sprite = typeLaser;
        }




    }
    
    public void UpdateItemPopUpComp2(Item _itemInfo)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName2.text = _itemInfo.itemName;
        popupDmg2.text = _itemInfo.dmg.ToString();
        popupCritX2.text = _itemInfo.critX.ToString();
        popupCritChance2.text = _itemInfo.critChance.ToString();
        popupFirerate2.text = _itemInfo.firerate.ToString();
        popupIcon2.sprite = _itemInfo.icon;

        //segment check
        if (_itemInfo.segment == Item.Segment.Head)
        {
            topEye2.SetActive(true);
            middleEye2.SetActive(false);
            bottomEye2.SetActive(false);
        }
        if (_itemInfo.segment == Item.Segment.Torso)
        {
            topEye2.SetActive(false);
            middleEye2.SetActive(true);
            bottomEye2.SetActive(false);
        }
        if (_itemInfo.segment == Item.Segment.Legs)
        {
            topEye2.SetActive(false);
            middleEye2.SetActive(false);
            bottomEye2.SetActive(true);
        }

        //range or melee check
        if (_itemInfo.projectile == true)
        {
            attackIcon2.sprite = rangedIcon;
            attackPill2.color = Color.blue;
            rangePillText2.text = _itemInfo.projectile_range.ToString();
            attackPillText2.text = "Ranged";
        }
        else
        {
            attackIcon2.sprite = meleeIcon;
            attackPill2.color = Color.red;
            rangePillText2.text = _itemInfo.melee_range.ToString();
            attackPillText2.text = "Melee";

        }

        //type check
        if (_itemInfo.attackType == Item.AttackType.Line)
        {
            typeIcon2.sprite = typeLine;
        }
        if (_itemInfo.attackType == Item.AttackType.Cone)
        {
            typeIcon2.sprite = typeCone;
        }
        if (_itemInfo.attackType == Item.AttackType.Circle)
        {
            typeIcon2.sprite = typeCircle;
        }
        if (_itemInfo.attackType == Item.AttackType.Rapid)
        {
            typeIcon2.sprite = typeRapid;
        }
        if (_itemInfo.attackType == Item.AttackType.Lob)
        {
            typeIcon2.sprite = typeLob;
        }
        if (_itemInfo.attackType == Item.AttackType.Cannon)
        {
            typeIcon2.sprite = typeCannon;
        }
        if (_itemInfo.attackType == Item.AttackType.Laser)
        {
            typeIcon2.sprite = typeLaser;
        }


    }

    public void UpdateHealthText(float _hp)
    {
        hpText.text = _hp.ToString("F0"); //removes any decimals
    }

    public void UpdateHealthBar(float _currentHp, float _maxHp)
    {
        healhBar.fillAmount = _currentHp / _maxHp;
        //Debug.Log(_currentHp / _maxHp);
        //Debug.Log(_currentHp / _maxHp);
    }
    public void UpdateLevelext(int _lvl)
    {
        //levelText.text = "Level " + _lvl.ToString();
    }

    #endregion

    #region Inventory Item Stats Popup

    //public void InvenSegmentPopUp()
    //{
    //    var rt = invenSegementPopUpPanel.GetComponent<RectTransform>();
    //    //print(rt.anchoredPosition);
    //    rt.anchoredPosition = new Vector2(522.75f, -400.00f);
    //    //headSegementPopUpPanel.transform.DOMove(new Vector3(234.489014f, -343f, 0), 1); //COULD ADD EASE HERE;
    //}
    //public void InvenSegmentPopDown()
    //{
    //    var rt = invenSegementPopUpPanel.GetComponent<RectTransform>();
    //    rt.anchoredPosition = new Vector2(522.75f, -561.85f);
    //}

    public void UpdateInventorySlotImages()
    {
        //head item
        if(_PC.headItem !=null)
        {
            invenSlot0.sprite = _PC.headItem.icon; //images for icon
            _HUD.hasItem1 = true;
        }
        else
        {
            invenSlot0.sprite = emptySlotSprite;
            _HUD.hasItem1 = false;
        }

        //torso item
        if (_PC.torsoItem != null)
        {
            invenSlot1.sprite = _PC.torsoItem.icon; //images for icon
            _HUD.hasItem2 = true;
        }
        else
        {
            invenSlot1.sprite = emptySlotSprite;
            _HUD.hasItem2 = false;
        }
        
        //leg item
        if (_PC.legItem != null)
        {
            invenSlot2.sprite = _PC.legItem.icon; //images for icon
            _HUD.hasItem3 = true;
        }
        else
        {
            invenSlot2.sprite = emptySlotSprite;
            _HUD.hasItem3 = false;
        }

    }

    public void InventorySlotHover(int _whichSlot)
    {

        switch(_whichSlot)
        {
            case 0:
                popupName.text = _PC.headItem.itemName;
                popupDmg.text = "Dmg: " + _PC.headItem.dmg.ToString();
                popupCritX.text = "CritX: " + _PC.headItem.critX.ToString();
                popupCritChance.text = "Crit%: " + _PC.headItem.critChance.ToString();
                popupFirerate.text = "Firerate%: " + _PC.headItem.firerate.ToString();

                break;
            case 1:
                popupName.text = _PC.torsoItem.itemName;
                popupDmg.text = "Dmg: " + _PC.torsoItem.dmg.ToString();
                popupCritX.text = "CritX: " + _PC.torsoItem.critX.ToString();
                popupCritChance.text = "Crit%: " + _PC.torsoItem.critChance.ToString();
                popupFirerate.text = "Firerate%: " + _PC.torsoItem.firerate.ToString();

                break;
            case 2:
                popupName.text = _PC.legItem.itemName;
                popupDmg.text = "Dmg: " + _PC.legItem.dmg.ToString();
                popupCritX.text = "CritX: " + _PC.legItem.critX.ToString();
                popupCritChance.text = "Crit%: " + _PC.legItem.critChance.ToString();
                popupFirerate.text = "Firerate%: " + _PC.legItem.firerate.ToString();

                break;
        }

  
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

        switch (_itemInfo.segment)
        {
            case Item.Segment.Head:
                slot = 0;
                break;
            case Item.Segment.Torso:
                slot = 1;
                break;
            case Item.Segment.Legs:
                slot = 2;
                break;

        }

        if (slot != -1)
        {
            print(heldItem.itemName + " is on slot " + slot);
            EquipImage(slot);

        }


    }

    /// <summary>
    /// Equips item to player inventory and resets cursor to default
    /// </summary>
    /// <param name="_slot"></param>
    public void EquipImage(int _slot)
    {
        var itemLeftSide = Instantiate(heldItem.avtarPrefabLeft, _AVTAR.slotsOnPlayerLeft[_slot].transform);
        var itemRightSide = Instantiate(heldItem.avtarPrefabRight, _AVTAR.slotsOnPlayerRight[_slot].transform);
        _PC.itemsAnimLeftSide.Add(itemLeftSide.GetComponentInChildren<Animator>());
        _PC.itemsAnimRightSide.Add(itemRightSide.GetComponentInChildren<Animator>());

        //default left
        var itemFront = Instantiate(heldItem.avatarPrefabFrontLeft, _AVTAR.slotsOnPlayerFront[_slot].transform);
        _PC.itemsAnimForward.Add(itemFront.GetComponentInChildren<Animator>());

        var itemBackSide = Instantiate(heldItem.avtarPrefabBackLeft, _AVTAR.slotsOnPlayerBack[_slot].transform);
        _PC.itemsAnimBack.Add(itemBackSide.GetComponentInChildren<Animator>());

        if (heldItem.segment == Item.Segment.Legs) _PC.UpdateLegAnimators();


        _PIA.PassiveAbilityItemCheck();

        // (flip) itemFront.transform.localScale = new Vector3(-itemFront.transform.rotation.x, itemFront.transform.rotation.y, itemFront.transform.rotation.z);

        //FOR ALL OTHER ITEMS

        cursor.sprite = defaultCursor;
        heldItem = null;
        isHoldingItem = false;
        //rotate image

        //_PC.CloseSlots();
    }




    #endregion


    #region Popup animaions

    public void PlayPopupOpen()
    {
        hoverItemAnimator.SetTrigger("Open");
    }
    public void PlayPopupClose()
    {
        hoverItemAnimator.SetTrigger("Close");
    }

    public void PlayPopup1Open()
    {
        hoverItemStatComp1Animator.SetTrigger("Open");
    }

    public void PlayPopup1Close()
    {
        hoverItemStatComp1Animator.SetTrigger("Close");
    }

    public void PlayPopup2Open()
    {
        hoverItemStatComp2Animator.SetTrigger("Open");
    }

    public void PlayPopup2Close()
    {
        hoverItemStatComp2Animator.SetTrigger("Close");
    }


    #endregion


    #region Game Over

    public void PlayTransitionAnimation()
    {
        gameOverMenu.SetActive(true);

    }

    public void PlayLoopAnimation()
    {

    }


    #endregion



}