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



    public void UpdateItemPopUp(string _itemName, float _itemDmg, float _itemCritX, float _itemCritChance, float _itemFirerate)
    {
        //ADD LATER FORMATTING FOR FLOATS

        popupName.text = _itemName;
        popupDmg.text = "DMG: " + _itemDmg.ToString();
        popupCritX.text = "CritX: " + _itemCritX.ToString();
        popupCritChance.text = "Crit%: " + _itemCritChance.ToString();
        popupFirerate.text = "Firerate%: " + _itemFirerate.ToString();
    }

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
}
