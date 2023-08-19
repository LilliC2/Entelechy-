using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    GameObject player;

    [Header("Dungeon Generation")]
    bool readyForGeneration = true;
    public int dungeonLevel;
    public Transform levelParent;


    public enum GameState
    {
        Playing, Pause, Iventory, Dead
    }

    public GameState gameState;


    //generate room
    //generate mini map

    //find room end condition
   
    //if room is kill all enemies
        /*spawn player in center of the room
        */

    //if room is find exit

        /*spawn player at beginning of the room
         * 
         */
  
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if(readyForGeneration)
        {
            GenerateLevel();

        }

    }

    void GenerateLevel()
    {
        readyForGeneration = false;

        //increase dungeon lvl
        dungeonLevel++;

        //update UI
        //_UI.UpdateLevelext(dungeonLevel);
        //clear dungeon
        ClearPreviousLevel();

        //pick prefab
        //use this to load any thing with room and can search for difficulty too 
        //FLOOR TEMP
        //instantiate new prefab

        var randomLevel = Random.Range(0, 2); // last digit excluded

        GameObject currentLevel = Instantiate(Resources.Load("Floor "+randomLevel, typeof(GameObject)), levelParent) as GameObject;

        //find beginning room

        Transform levelStartRoom = currentLevel.transform.Find("BeginningRoom");
        Transform levelEndRoom = currentLevel.transform.Find("EndRoom");

        //move player to room
        player.transform.position = new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z);

    }

    void ClearPreviousLevel()
    {
        //destroy all room and any enemies, item etc. (could be all in same layer)
        if (levelParent.childCount != 0)
        {
            foreach (GameObject child in levelParent.transform)
            {
                Destroy(child);
            }

        }

        //destroy any remaining items
        var items = GameObject.FindGameObjectsWithTag("Item Drop");

        if(items.Length != 0)
        {
            foreach (var item in items)
            {
                Destroy(item);
            }
        }
        

        //clear any remaing enemies
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length != 0)
        {
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}


