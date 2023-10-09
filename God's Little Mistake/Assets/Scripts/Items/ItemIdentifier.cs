using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ItemIdentifier : GameBehaviour
{
    bool inRange;
    public Item itemInfo;
    Selecting selecting;
    bool isHovering;

    [Header("Animation")]
    public Animator anim;
    public Animator anim1;
    public Animator anim2;

    public GameObject statPop;

    bool itemSpawned;
    bool itemRemoved;
    bool itemAdd;

    public void Start()
    {
        statPop = GameObject.Find("Stat Popup");
            
        selecting = GetComponent<Selecting>();
        anim = _UI.statsPopUpPanel.GetComponent<Animator>();
        anim1 = _UI.statComp1.GetComponent<Animator>();
        anim2 = _UI.statComp2.GetComponent<Animator>();
    }

    private void Update()
    {
        //check if selected item is arms!!!


        if (isHovering)
        {
            print("we hover");

            if (itemInfo.segment == Item.Segment.Torso)
            {
                float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
                print(scrollDelta);

                if (scrollDelta > 0)
                {
                    print("Left arm");
                    _UI.leftArmItem = itemInfo;
                    //Changes item to left here
                }
                if (scrollDelta < 0)
                {
                    print("right arm");

                    _UI.rightArmItem = itemInfo;
                    //Changes item to left here
                }
            }
        }


        if (inRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                //check which segment it is

                //check if item is already there
                bool itemInSlot = selecting.CheckIfItemIsInSlot();

                if (itemInSlot)
                {
                    print("thers an item in this slot");

                    if(!itemRemoved)
                    {
                        itemRemoved = true;

                        //destroy old item from player avatar
                        selecting.RemovePreviousItem();

                    }


                    if(!itemSpawned)
                    {
                        itemSpawned = true;

                        var newSpawnPoint = new Vector3();
                        NavMeshHit hit;
                        if (NavMesh.SamplePosition(_PC.transform.position, out hit, 1f, NavMesh.AllAreas))
                        {
                            newSpawnPoint = hit.position;
                        }
                        //place old item on ground

                        GameObject item = Instantiate(Resources.Load("Item") as GameObject, newSpawnPoint, Quaternion.identity);
                        _UI.statComp1.SetActive(false);
                        _UI.statComp2.SetActive(false);


                        item.GetComponent<ItemIdentifier>().itemInfo = selecting.previousItem;
                        item.GetComponentInChildren<SpriteRenderer>().sprite = item.GetComponent<ItemIdentifier>().itemInfo.icon;
                    }

                }


                if(!itemAdd)
                {
                    itemAdd = true;
                    //_ISitemD.AddItemToInventory(itemInfo);
                    //equip new items
                    ExecuteAfterFrames(20, () => _UI.CreateItemSelected(itemInfo));
                    
                }



                ExecuteAfterFrames(25, ()=> Destroy(this.gameObject));
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

    public void OnMouseOver()
    {
        print("ENTER");

        isHovering = true;

        _UI.statsPopUpPanel.SetActive(true);
        _UI.statsPopUpPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        _UI.UpdateItemPopUp(itemInfo);
        anim.SetTrigger("Open");
        _UI.arrowComp.SetActive(true);

        Item itemSlot3 = new();
        Item itemSlot4 = new();

        foreach (var item in _PC.playerInventory)
        {
            if (item.inSlot == 3) itemSlot3 = item;
            if (item.inSlot == 4) itemSlot4 = item;
        }

        if (itemInfo.segment == Item.Segment.Torso)
        {
            print("Its an arm");
            _UI.statComp1.SetActive(true);
            _UI.statComp2.SetActive(true);

            anim1.SetTrigger("Open");
            anim2.SetTrigger("Open");

            _UI.UpdateItemPopUpComp1(itemSlot3);
            _UI.UpdateItemPopUpComp2(itemSlot4);
        }
        else
        {
            print("its Not an arm");
            _UI.statComp1.SetActive(true);
            _UI.statComp2.SetActive(false);

            Item itemMatch = new();

            foreach (var item in _PC.playerInventory)
            {
                if (item.category == itemInfo.category) itemMatch = item;
            }

            anim1.SetTrigger("Open");

            _UI.UpdateItemPopUpComp1(itemMatch);
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
        isHovering = false;

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
