using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippingItems : Singleton<EquippingItems>
{
    public GameObject HeadAvatar;
    public GameObject TorsoAvatar;
    public GameObject LegAvatar;

    // Update is called once per frame
    public void EquipItem(Item _item)
    {
        var ID = _item.ID;

        switch(_item.segment)
        {
            case Item.Segment.Head: //pea shooter

                if (_item.avatarPrefab != null)
                {
                    if (HeadAvatar.transform.childCount != 0) Destroy(HeadAvatar.transform.GetChild(0).gameObject);
                    var head = Instantiate(_item.avatarPrefab, HeadAvatar.transform);
                    _PC.headFiringPoint = TransformDeepChildExtension.FindDeepChild(head.transform, "FiringPoint").gameObject;

                }
                else
                {
                    _PC.headFiringPoint = _PC.headFiringPointDefault;

                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.headSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;

                }

                break;
            case Item.Segment.Torso:

                //if has animations
                if(_item.avatarPrefab != null)
                {
                    if (TorsoAvatar.transform.childCount != 0) Destroy(TorsoAvatar.transform.GetChild(0).gameObject);

                    var torso = Instantiate(_item.avatarPrefab, TorsoAvatar.transform);
                    _PC.torsoFiringPoint = TransformDeepChildExtension.FindDeepChild(torso.transform, "FiringPoint").gameObject;


                }
                else
                {
                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.torsoSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;
                }

                break;
            case Item.Segment.Legs:

                if (_item.avatarPrefab != null)
                {
                    if (LegAvatar.transform.childCount != 0) Destroy(LegAvatar.transform.GetChild(0).gameObject);
                    print("add legfs");
                    var legs = Instantiate(_item.avatarPrefab, LegAvatar.transform);
                    var anim = legs.GetComponent<Animator>();
                    print(anim);
                    _PC.legsAnim = anim;
                }
                else
                {
                    if (_IM.itemDataBase[ID].staticTempSprite != null) _PC.legSprite.sprite = _IM.itemDataBase[ID].staticTempSprite;

                }

                break;
                    
            
        }
    }

   
}

public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    /*
    //Depth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    */
}
