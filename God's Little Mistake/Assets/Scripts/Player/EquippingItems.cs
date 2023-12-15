using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippingItems : Singleton<EquippingItems>
{
    [SerializeField]
    GameObject HeadAvatar;
    [SerializeField]
    GameObject TorsoAvatar;
    [SerializeField]
    GameObject LegAvatar;

    // Update is called once per frame
    public void EquipItem(Item _item)
    {
        var ID = _item.ID;

        switch(_item.segment)
        {
            case Item.Segment.Head: //pea shooter

                if (_item.avatarPrefab != null)
                {
                    if (HeadAvatar.transform.childCount != 0) Destroy(HeadAvatar.transform.GetChild(0));
                    Instantiate(_item.avatarPrefab, HeadAvatar.transform);
                }
                else
                {

                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;

                }

                break;
            case Item.Segment.Torso:

                //if has animations
                if(_item.avatarPrefab != null)
                {
                    if (TorsoAvatar.transform.childCount != 0) Destroy(TorsoAvatar.transform.GetChild(0));

                    Instantiate(_item.avatarPrefab, TorsoAvatar.transform);
                }
                else
                {
                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;
                }

                break;
            case Item.Segment.Legs:

                if (_item.avatarPrefab != null)
                {
                    if (LegAvatar.transform.childCount != 0) Destroy(LegAvatar.transform.GetChild(0));

                    Instantiate(_item.avatarPrefab, LegAvatar.transform);
                }
                else
                {
                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.legSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;

                }

                break;
                    
            
        }
    }
}
