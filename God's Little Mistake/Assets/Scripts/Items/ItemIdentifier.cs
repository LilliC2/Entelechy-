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
    public Animator anim2;

    public GameObject statPop;
    public GameObject statComp1;
    public GameObject statComp2;


    public void Start()
    {
        statPop = GameObject.Find("Stat Popup");
        statComp1 = GameObject.Find("Stat Comp 1");
        statComp2 = GameObject.Find("Stat Comp 2");

        anim = statPop.GetComponent<Animator>();
        anim1 = statComp1.GetComponent<Animator>();
        anim2 = statComp2.GetComponent<Animator>();
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


        //Stats for item mouse is hovering
        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _UI.UpdateItemPopUp(itemInfo);
        anim.SetTrigger("Open");

        //search for matches
        var matchItemList = _UI.SearchForItemMatch(itemInfo);

        //Check if there are any matches
        if (matchItemList.Count == 0)
            print(matchItemList);
        else print("no match");

        //If there is only one match
        if(matchItemList.Count == 1)
        {
            //stats for matched item
            print("ITS A MATCH");
            _UI.statComp1.SetActive(true);
            anim1.SetTrigger("Open");
            _UI.arrowComp.SetActive(true);
            _UI.UpdateItemPopUpComp1(matchItemList[0]);
        }

        //If there is two matches
        if (matchItemList.Count == 2)
        {
            //stats for matched item 1
            print("ITS A MATCH");
            _UI.statComp1.SetActive(true);
            anim1.SetTrigger("Open");
            _UI.arrowComp.SetActive(true);
            _UI.UpdateItemPopUpComp1(matchItemList[0]);

            //add in updating ui here for the second one.
            _UI.statComp2.SetActive(true);
            //anim1.SetTrigger("Open");
            _UI.arrowComp.SetActive(true);
            _UI.UpdateItemPopUpComp2(matchItemList[1]);
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
