using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [Header("Item stats pop up panel")]
    public GameObject statsPopUpPanel;
    public TMP_Text popupName;
    public TMP_Text popupDmg;
    public TMP_Text popupCritX;
    public TMP_Text popupCritChance;
    public TMP_Text popupFirerate;

    [Header("Head Segment Pop up")]
    public GameObject headSegementPopUpPanel;
    public TMP_Text headPopupName;
    public TMP_Text headPopupDmg;
    public TMP_Text headPopupCritX;
    public TMP_Text headPopupCritChance;
    public TMP_Text headPopupFirerate;
    
    [Header("Torso Segment Pop up")]
    public GameObject torsoSegementPopUpPanel;
    public TMP_Text torsoPopupName;
    public TMP_Text torsoPopupDmg;
    public TMP_Text torsopupCritX;
    public TMP_Text torsoPopupCritChance;
    public TMP_Text torsoPopupFirerate;
    
    [Header("Leg Segment Pop up")]
    public GameObject legSegementPopUpPanel;
    public TMP_Text legPopupName;
    public TMP_Text legPopupDmg;
    public TMP_Text legpupCritX;
    public TMP_Text legPopupCritChance;
    public TMP_Text legPopupFirerate;



    public void UpdateItemPopUp(string _itemName, float _itemDmg, float _itemCritX, float _itemCritChance, float _itemFirerate)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName.text = _itemName;
        popupDmg.text = "DMG: " + _itemDmg.ToString();
        popupCritX.text = "CritX: " + _itemCritX.ToString();
        popupCritChance.text = "Crit%: " + _itemCritChance.ToString();
        popupFirerate.text = "Firerate%: " + _itemFirerate.ToString();
    }

    #region Item Segment Popup

    public void HeadSegmentPopUp()
    {
        var rt = headSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(234.49f, -339.00f);
        //headSegementPopUpPanel.transform.DOMove(new Vector3(234.489014f, -343f, 0), 1); //COULD ADD EASE HERE;
    }
    public void HeadSegmentPopDown()
    {
        var rt = headSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(234.49f, -561.85f);
    }
    
    public void TorsoSegmentPopUp()
    {
        var rt = torsoSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(467.30f, -339.00f);
    }
    public void TorsoSegmentPopDown()
    {
        var rt = torsoSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(467.30f, -561.85f);
    }
    
    public void LegSegmentPopUp()
    {
        var rt = legSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(705, -339.00f);
    }
    public void LegSegmentPopDown()
    {
        var rt = legSegementPopUpPanel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(705, -561.85f);
    }


    #endregion
}
