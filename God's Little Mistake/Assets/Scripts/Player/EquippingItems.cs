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
        }
    }
}
