using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemIdentifier : GameBehaviour
{
    bool inRange;
    public Item itemInfo;


    [Header("Animation")]
    public Animator anim;
    public Animator anim1;

    public GameObject statPop;
    public GameObject statComp1;


    public void Start()
    {
        statPop = GameObject.Find("Stat Popup");
        statComp1 = GameObject.Find("Stat Comp 1");

        anim = statPop.GetComponent<Animator>();
        anim1 = statComp1.GetComponent<Animator>();
    }

    private void Update()
    {
        if(inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //pick up item
                if (_PC.playerInventory.Count < 5)//invenotry cap number here
                {
                    print("Destroy obj");
                    Destroy(gameObject);
                    _UI.CreateItemSelected(itemInfo);

                }  

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = false;
        }
    }

    //public void OnMouseEnter()
    //{
    //    print("ENTER");
    //    _UI.statsPopUpPanel.SetActive(true);
    //    _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //    _UI.UpdateItemPopUp(itemInfo);
    //    anim.SetTrigger("Open");

    //}

    public void OnMouseOver()
    {
        print("ENTER");
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _UI.UpdateItemPopUp(itemInfo);
        anim.SetTrigger("Open");

        List<Item> itemMatchInPlayerInven = new();

        foreach (var item in _PC.playerInventory)
        {
            if (item.segment == itemInfo.segment)
            {
                print("ITS A MATCH");
                _UI.statComp1.SetActive(true);
                anim1.SetTrigger("Open");
                _UI.arrowComp.SetActive(true);
                _UI.UpdateItemPopUpComp1(itemInfo);
            }
        }

            //var match = _UI.SearchForItemMatch(itemInfo);


            //foreach (var item in match)
            //{
            //    if (item != null)
            //    {
            //        print("ITS A MATCH");
            //        _UI.statComp1.SetActive(true);
            //        anim1.SetTrigger("Open");
            //        _UI.arrowComp.SetActive(true);
            //        _UI.UpdateItemPopUpComp1(itemInfo);
            //    }
            //}

            //foreach (var item in _PC.playerInventory)
            //{
            //    if (item.segment == itemInfo.segment)
            //    {
            //        print("ITS A MATCH");
            //        _UI.statComp1.SetActive(true);
            //        anim1.SetTrigger("Open");
            //        _UI.arrowComp.SetActive(true);
            //        _UI.UpdateItemPopUpComp1(itemInfo);
            //    }



            //}
        }

    public void OnMouseExit()
    {
        print("EXIT");
        anim.ResetTrigger("Open");
        anim1.ResetTrigger("Open");
        anim.SetTrigger("Close");
        anim1.SetTrigger("Close");
        ExecuteAfterSeconds(1, () => TurnOff());


    }

    public void TurnOff()
    {
        _UI.statsPopUpPanel.SetActive(false);
        _UI.statComp1.SetActive(false);
        _UI.arrowComp.SetActive(false);
    }

    //public void InfoSwitch()
    //{
    //    switch (itemInfo.segment)
    //    {
    //        case (Item.Segment.Head):
    //            _UI.TopSegmentIndicator();
    //            break;
    //        case (Item.Segment.Torso):
    //            _UI.MiddleSegmentIndicator();
    //            break;
    //        case (Item.Segment.Legs):
    //            _UI.BottomSegmentIndicator();
    //            break;
    //    }
    //}
            
}
