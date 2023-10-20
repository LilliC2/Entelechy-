using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    GameObject player;

    [Header("Dungeon Generation")]
    public bool readyForGeneration = true;
    public int dungeonLevel;
    public Transform levelParent;
    Transform levelStartRoom;
    GameObject currentLevel;
    public GameObject endRoomOB;
    public GameObject itemPF;

    [Header("Pause and game over")]
    public bool isPlaying;
    public bool isPaused;
    public string urlToOpen = "https://www.instagram.com/nlmgame/";

    public enum GameState
    {
        Playing, Pause, Iventory, Dead
    }

    public GameState gameState;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        if (readyForGeneration && SceneManager.GetActiveScene().name == "GameLoopPrototype")
        {
            print("Generate new level");
            GenerateLevel();
            _EM.SpawnEnemiesForLevel();

        }

        if (gameState == GameState.Playing)
        {
            isPlaying = true;
            isPaused = false;
        }


        if (gameState == GameState.Pause)
        {
            isPlaying = false;
            isPaused = true;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying)
        {
            OnPause();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            OnResume();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Dead)
        {

        }

        if (gameState == GameState.Dead)
        {
            GameOver();
            isPlaying = false;
            isPaused = false;
        }

        //if (Input.GetKeyDown(KeyCode.M) && isPlaying)
        //{
        //    gameState = GameState.Dead;
        //}



    }

    public void OnPause()
    {
        gameState = GameState.Pause;
        _UI.OnPause();
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        gameState = GameState.Playing;
        _UI.OnResume();
        Time.timeScale = 1;
    }


    public void GameOver()
    {
        ExecuteAfterSeconds(1.5f,() =>_UI.PlayTransitionAnimation());
        ExecuteAfterSeconds(1.5f,() => Time.timeScale = 0);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void FeedbackLink()
    {
        Application.OpenURL(urlToOpen);
    }

    public void Restart()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        dungeonLevel = 0;
        readyForGeneration = true;
        
        //SceneManager.LoadScene(currentScene);
        levelStartRoom = currentLevel.transform.Find("BeginningRoom");
        player.transform.position = new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z);

        ClearPreviousLevel();

        //reset player
        _PC.health = 100;
        _PC.maxHP = 100;

        for (int i = 0; i < _PC.playerInventory.Count; i++)
        {
            if (_PC.playerInventory[i] != null) _ISitemD.RemoveItemFromInventory(i);
        }

        //give player tentacle mouth
        gameState = GameState.Playing;
    }

    void GenerateLevel()
    {
        readyForGeneration = false;

        //increase dungeon lvl
        dungeonLevel++;
        print("Dungeon Level " + dungeonLevel);

        //update UI
        _UI.UpdateLevelext(dungeonLevel);
        //clear dungeon
        ClearPreviousLevel();

        //pick prefab
        //use this to load any thing with room and can search for difficulty too 
        //FLOOR TEMP
        //instantiate new prefab

        var randomLevel = Random.Range(0, 2); // last digit excluded

        //will change to change to Environment_Floor_ later
        currentLevel = Instantiate(Resources.Load("Enviroment_Floor_" + randomLevel, typeof(GameObject)), levelParent) as GameObject;

        //find beginning room

        levelStartRoom = currentLevel.transform.Find("BeginningRoom");
        Transform levelEndRoom = currentLevel.transform.Find("EndRoom");

        //add in controls
        if(dungeonLevel == 1)
        {
            var controls = Instantiate(Resources.Load("ControlsPrefab", typeof(GameObject)), new Vector3(levelStartRoom.position.x, 0.02f, levelStartRoom.position.z), Quaternion.Euler(new Vector3(90f,0f,0f)), levelParent) as GameObject;
            var startingItem = Instantiate(itemPF, new Vector3(levelStartRoom.position.x, 0.02f, levelStartRoom.position.z-2.5f), Quaternion.Euler(new Vector3(0f,0f,0f)), levelParent) as GameObject;

            startingItem.GetComponent<ItemIdentifier>().itemInfo = _ItemD.itemDataBase[9]; //set as peashooter
        
        }

        _RP.RandomiseEnvionmentProps();


        endRoomOB.GetComponent<EndLevelTrigger>().ResetDoor(); //reset animations
        //move end room trigger
        endRoomOB.transform.position = levelEndRoom.position;

        //move player to room
        player.transform.position = new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z);

    }

    void ClearPreviousLevel()
    {
        //destroy all room and any enemies, item etc. (could be all in same layer)
        if (levelParent.childCount != 0)
        {
            foreach (Transform child in levelParent.transform)
            {
                Destroy(child.gameObject);
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


