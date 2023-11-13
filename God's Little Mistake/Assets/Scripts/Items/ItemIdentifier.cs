using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    bool itemRemoved = false;
    bool itemAdd;

    [Header("Hold E")]
    public Image holdEFill;
    public float startTimer;
    public float holdTimer = 2f;
    public bool isTiming;
    public Image glowingHold;


    public void Start()
    {
        statPop = GameObject.Find("Stat Popup");

        selecting = GetComponent<Selecting>();
    }

    private void Update()
    {
   

        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isTiming = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                isTiming = false;
            }

            if (isTiming)
            {
                //turn on holdding timer
                startTimer += Time.deltaTime;
                holdEFill.fillAmount = startTimer / holdTimer;

                if (startTimer > holdTimer)
                {
                    //check which segment it is

                Item prevItem = new();
                bool itemOnPlayer = false;
                //determine if there is already an item of that segment equipped

                switch (itemInfo.segment)
                {
                    case Item.Segment.Head:
                        if (_PC.headItem.itemName != "NULL")
                        {
                            itemOnPlayer = true;
                            prevItem = _PC.headItem;
                        }
                        break;
                    case Item.Segment.Torso:
                        if (_PC.torsoItem.itemName != "NULL")
                        {
                            itemOnPlayer = true;
                            prevItem = _PC.torsoItem;
                        }

                        break;
                    case Item.Segment.Legs:
                        if (_PC.legItem.itemName != "NULL")
                        {
                            itemOnPlayer = true;
                            prevItem = _PC.legItem;
                        }

                            break;

                    }

                    if (itemOnPlayer)
                    {
                        //remove item
                        selecting.RemovePreviousItem();

                        //spawn old item on ground
                        if (!itemSpawned)
                        {
                            itemSpawned = true;

                            var newSpawnPoint = new Vector3();
                            UnityEngine.AI.NavMeshHit hit;
                            if (UnityEngine.AI.NavMesh.SamplePosition(_PC.transform.position, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
                            {
                                newSpawnPoint = hit.position;
                            }
                            //place old item on ground

                        GameObject item = Instantiate(Resources.Load("Item") as GameObject, newSpawnPoint, Quaternion.identity);
                        item.GetComponent<ItemIdentifier>().itemInfo = prevItem;
                    }
                }


                    if (!itemAdd)
                    {

                    _AM.ItemPickUp();
                    itemAdd = true;
                    
                    //equip new items
                    _UI.CreateItemSelected(itemInfo);

                    _IM.AddItemToInventory(itemInfo);



                }



                Destroy(this.gameObject);
            }

        }

            //if (inRange)
            //{
            //    if (Input.GetKey(KeyCode.E))
            //    {
            //        //check which segment it is


            //        bool itemOnPlayer = false;
            //        //determine if there is already an item of that segment equipped

            //        switch (itemInfo.segment)
            //        {
            //            case Item.Segment.Head:
            //                if (_PC.headItem.itemName != "NULL") itemOnPlayer = true;

            //                break;
            //            case Item.Segment.Torso:
            //                if (_PC.torsoItem.itemName != "NULL") itemOnPlayer = true;

            //                break;
            //            case Item.Segment.Legs:
            //                if (_PC.legItem.itemName != "NULL") itemOnPlayer = true;

            //                break;

            //        }

            //        if (itemOnPlayer)
            //        {
            //            //remove item
            //            selecting.RemovePreviousItem();

            //            //spawn old item on ground
            //            if (!itemSpawned)
            //            {
            //                itemSpawned = true;

            //                var newSpawnPoint = new Vector3();
            //                UnityEngine.AI.NavMeshHit hit;
            //                if (UnityEngine.AI.NavMesh.SamplePosition(_PC.transform.position, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
            //                {
            //                    newSpawnPoint = hit.position;
            //                }
            //                //place old item on ground

            //                GameObject item = Instantiate(Resources.Load("Item") as GameObject, newSpawnPoint, Quaternion.identity);
            //                item.GetComponent<ItemIdentifier>().enabled = false;
            //            }
            //        }


            //        if (!itemAdd)
            //        {

            //            _AM.ItemPickUp();
            //            itemAdd = true;

    }

    public void OnMouseExit()
    {
        isHovering = false;

        print("EXIT");


        _UI.PlayPopupClose();
        _UI.PlayPopup1Close();
        _UI.PlayPopup2Close();
        _UI.popupContent.SetActive(false);
        _UI.popupContent.SetActive(false);
        _UI.popupContent2.SetActive(false);
        ExecuteAfterSeconds(1, () => TurnOff());


            _PM.popupPanel.SetActive(true);
            _PM.popupPanel.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _PM.UpdateItemPopUp(itemInfo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("player");
            inRange = false;

        _PM.popupPanel.SetActive(false);
        }
    }

}
