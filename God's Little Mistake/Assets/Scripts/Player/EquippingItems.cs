using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippingItems : Singleton<EquippingItems>
{

    // Update is called once per frame
    public void EquipItem(Item _item)
    {
        switch(_item.ID)
        {
            case 0: //pea shooter
                if (_IM.itemDataBase[0].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[0].staticTempSprite;

                break;
            case 1: //nubs
                if (_IM.itemDataBase[1].staticTempSprite != null) _PC.legSprite.sprite = _IM.itemDataBase[1].staticTempSprite;


                break;
            case 2: //saber

                if (_IM.itemDataBase[2].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[2].staticTempSprite;


                break;
            case 3: //lob

                if (_IM.itemDataBase[3].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[3].staticTempSprite;


                break;
            case 4: //sqwuito

                if (_IM.itemDataBase[4].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[4].staticTempSprite;


                break;
            case 5: //tripod

                if (_IM.itemDataBase[5].staticTempSprite != null) _PC.legSprite.sprite = _IM.itemDataBase[5].staticTempSprite;

                break;
            case 6: //slug

                if (_IM.itemDataBase[6].staticTempSprite != null) _PC.legSprite.sprite = _IM.itemDataBase[6].staticTempSprite;

                break;
            case 7: //rocket launcher

                if (_IM.itemDataBase[7].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[7].staticTempSprite;

                break;
            case 8: //LMG

                if (_IM.itemDataBase[8].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[8].staticTempSprite;

                break;
            case 9: //shotgun

                if (_IM.itemDataBase[9].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[9].staticTempSprite;

                break;
            case 10: //missy head

                if (_IM.itemDataBase[10].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[10].staticTempSprite;

                break;
            case 11: //missy torso

                if (_IM.itemDataBase[11].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[11].staticTempSprite;

                break;
        }
    }
}
