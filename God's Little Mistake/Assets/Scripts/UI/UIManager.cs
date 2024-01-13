using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class UIManager : Singleton<UIManager>
{
    [Header("Dungeon Level")]
    public TMP_Text dungeonLevel;
    public Animator levelAnim;

    [Header("Game Over")]
    public GameObject gameOverMenu;
    public Animator gameOverAnim;


    [Header("Player Feedback")]
    public TMP_Text hpText;
    public TMP_Text levelText;
    public Image healhBar;
    public GameObject terryIntro;

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

    [Header("Missy Height")]
    public GameObject playerAvatar;
    Vector3 standard = new Vector3(0.239999443f, 0.0800049901f, -0.555116713f);
    Vector3 tall = new Vector3(-0.0209999997f, -0.136000007f, 0.0540000014f);

    [Header("Pause")]
    public GameObject pausePanel;
    public GameObject pauseFunctionalityPanel;
    public GameObject optionPanel;
    public GameObject ingameUI;

    [Header("Health")]
    public Animator heathAnim;
    public float playerHeath;
    public float maxHeath;
    public Gradient gradient;

    [Header("Popup")]
    public float holdTimer = 1f;

    [Header("Popup")]
    public GameObject inGameTerry;
    public TMP_Text inGameTerryTextl;
    public int enemyCount;

    private void Start()
    {   
        UpdateInventorySlotImages();
        heldItem = null;
        isHoldingItem = false;
        //statPop.SetActive(false);
        //statComp1.SetActive(false);
        //arrowComp.SetActive(false);
        playerAvatar = GameObject.FindGameObjectWithTag("Player");

        //hoverItemAnimator = statPop.GetComponent<Animator>();
        //hoverItemStatComp1Animator = statComp1.GetComponent<Animator>();
        //hoverItemStatComp2Animator = statComp2.GetComponent<Animator>();

        gameOverAnim = gameOverMenu.GetComponent<Animator>();

        //Pause Related
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);

        //Game Over Related
        gameOverMenu.SetActive(false);

        //healthbar related
        healhBar.fillAmount = 1;

        maxHeath = _PC.maxHP;
        holdTimer = 1f;

    }

    private void Update()
    {
        if(heldItem == null)
        {
            if (Input.GetKey(KeyCode.Mouse0)) cursor.sprite = cursorClick;
            else cursor.sprite = defaultCursor;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            levelAnim.ResetTrigger("Reset");
            levelAnim.SetTrigger("AClick");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            levelAnim.ResetTrigger("AClick");
            levelAnim.SetTrigger("Reset");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            levelAnim.ResetTrigger("Reset");
            levelAnim.SetTrigger("DClick");
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            levelAnim.ResetTrigger("DClick");
            levelAnim.SetTrigger("Reset");
        }

        playerHeath = _PC.health;

        heathAnim.SetFloat("Health", playerHeath);

        healhBar.color = gradient.Evaluate(playerHeath/maxHeath);


        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount > 0)
        {
            if (enemyCount > 1)
            {
                inGameTerryTextl.text = enemyCount.ToString() + " Enemies Left";
            }
            else
            {
                inGameTerryTextl.text = enemyCount.ToString() + " MORE MISTAKE TO ELIMINATE";
            }
        }
        else
        {
            inGameTerryTextl.text = "Mistake Free At last";
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

    public void PausingFromUI()
    {
        _GM.OnPause();
    }

    public void ResumingFromUI()
    {
        _GM.OnResume();
    }

    public void ExitingFromUI()
    {
        _GM.QuitGame();
    }

    public void TitleFromUI()
    {
        _GM.ToTitle();
    }

    public void ReloadFromUI()
    {
        _GM.GameLevel();
    }




    #endregion


    #region Heath & Other HUD

    public void UpdateHealthText(float _hp)
    {
        hpText.text = _hp.ToString("F0"); //removes any decimals

        if(_hp < 0)
        {
            hpText.text = "0";
        }
    }

    public void UpdateHealthBar(float _currentHp, float _maxHp)
    {
        healhBar.fillAmount = _currentHp / _maxHp;
        //Debug.Log(_currentHp / _maxHp);
        //Debug.Log(_currentHp / _maxHp);
    }

    public void CloseTerryInstruction()
    {
        terryIntro.SetActive(false);
        _GM.gameState = GameManager.GameState.Playing;
        Time.timeScale = 1;
        print("Close Terry");
        _GM.PlayerToStart();
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

    //public void InventorySlotHover(int _whichSlot)
    //{

    //    switch(_whichSlot)
    //    {
    //        case 0:
    //            popupName.text = _PC.headItem.itemName;
    //            popupDmg.text = "Dmg: " + _PC.headItem.dmg.ToString();
    //            //popupCritX.text = "CritX: " + _PC.headItem.critX.ToString();
    //            popupCritChance.text = "Crit%: " + _PC.headItem.critChance.ToString();
    //            popupFirerate.text = "Firerate%: " + _PC.headItem.firerate.ToString();

    //            break;
    //        case 1:
    //            popupName.text = _PC.torsoItem.itemName;
    //            popupDmg.text = "Dmg: " + _PC.torsoItem.dmg.ToString();
    //            //popupCritX.text = "CritX: " + _PC.torsoItem.critX.ToString();
    //            popupCritChance.text = "Crit%: " + _PC.torsoItem.critChance.ToString();
    //            popupFirerate.text = "Firerate%: " + _PC.torsoItem.firerate.ToString();

    //            break;
    //        case 2:
    //            popupName.text = _PC.legItem.itemName;
    //            popupDmg.text = "Dmg: " + _PC.legItem.dmg.ToString();
    //            //popupCritX.text = "CritX: " + _PC.legItem.critX.ToString();
    //            popupCritChance.text = "Crit%: " + _PC.legItem.critChance.ToString();
    //            popupFirerate.text = "Firerate%: " + _PC.legItem.firerate.ToString();

    //            break;
    //    }

  
    //}

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
            //print(heldItem.itemName + " is on slot " + slot);
            EquipImage(slot);

        }


    }

    /// <summary>
    /// Equips item to player inventory and resets cursor to default
    /// </summary>
    /// <param name="_slot"></param>
    public void EquipImage(int _slot)
    {
        _EI.EquipItem(heldItem);

        cursor.sprite = defaultCursor;
        heldItem = null;
        isHoldingItem = false;

        UpdateInventorySlotImages();

        //rotate image

        //_PC.CloseSlots();
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