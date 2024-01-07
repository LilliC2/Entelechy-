using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{

    GameObject player;

    [Header("Dungeon Generation")]
    public bool readyForGeneration;
    public int dungeonLevel;
    public Transform levelParent;
    Transform levelStartRoom;
    GameObject currentLevel;
    public GameObject endRoomOB;
    public GameObject itemPF;

    public bool isLevelCleared;

    [Header("Pause and game over")]
    public bool isPlaying;
    public bool isPaused;
    //public string urlToOpen = "https://www.instagram.com/nlmgame/";
    public Image fadeImage;
    public float fadeOutTime = 1.0f;



    public enum GameState
    {
        Playing, Pause, Iventory, Dead, Instruction
    }

    public GameState gameState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadeImage.fillAmount = 1;
        gameState = GameState.Instruction;
        Time.timeScale = 1.0f;

        GenerateLevel();
        readyForGeneration = true;

    }


    // Update is called once per frame
    void Update()
    {
        if (readyForGeneration)
        {
            print("Generate new level");
            GenerateLevel();
            readyForGeneration = false;
            endRoomOB.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.I) && isPlaying)
        {
            OnPause();
        }

        if (Input.GetKeyDown(KeyCode.I) && isPaused)
        {
            OnResume();
        }

        if (Input.GetKeyDown(KeyCode.I) && gameState == GameState.Dead)
        {

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

        if(gameState == GameState.Instruction)
        {
            TerryIntro();
        }

        if(isLevelCleared)
        {
            endRoomOB.SetActive(true);

            endRoomOB.transform.position = player.transform.position;
            ExecuteAfterSeconds(1, () => readyForGeneration = true);
        }


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

    public void TerryIntro()
    {
        Time.timeScale = 0;
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
        SceneManager.LoadScene("TitleScreen");
    }

    public void GameLevel()
    {
        SceneManager.LoadScene("GameLoopPrototype");
        //GenerateLevel();
    }

    public void FeedbackLink()
    {
        Application.OpenURL("https://forms.gle/TQJNc4cRzDxJzh9i9");
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


        //give player tentacle mouth
        gameState = GameState.Playing;
    }

    void GenerateLevel()
    {

        readyForGeneration = false;

        fadeImage.DOFade(0f, fadeOutTime);

        //increase dungeon lvl
        dungeonLevel++;
        print("Dungeon Level " + dungeonLevel);

        //update UI
        _UI.dungeonLevel.text = dungeonLevel.ToString();
        //clear dungeon
        ClearPreviousLevel();

        //pick prefab
        //use this to load any thing with room and can search for difficulty too 
        //FLOOR TEMP
        //instantiate new prefab

        var randomLevel = Random.Range(1, 3); // last digit excluded

        //will change to change to Environment_Floor_ later //randomLevel changed to 2
        currentLevel = Instantiate(Resources.Load("Environment_Floor_" + randomLevel, typeof(GameObject)), levelParent) as GameObject;
        //AddObjectsToMaskObject(currentLevel);
        //find beginning room

        levelStartRoom = currentLevel.transform.Find("BeginningRoom");
        Transform levelEndRoom = currentLevel.transform.Find("EndRoom");

        //add in controls
        if(dungeonLevel == 1)
        {

            var controls = Instantiate(Resources.Load("ControlsPrefab", typeof(GameObject)), new Vector3(levelStartRoom.position.x, -1.5f, levelStartRoom.position.z), Quaternion.Euler(new Vector3(90f,0f,0f)), levelParent) as GameObject;
            var startingItem = Instantiate(itemPF, new Vector3(levelStartRoom.position.x, -1.5f, levelStartRoom.position.z-2.5f), Quaternion.Euler(new Vector3(0f,0f,0f)), levelParent) as GameObject;

            startingItem.GetComponent<ItemIdentifier>().itemInfo = _IM.itemDataBase[0]; //set as peashooter

            //ExecuteAfterSeconds(0.5f, () => player.transform.position = new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z));
            //ExecuteAfterSeconds(0.5f, () => fadeImage.DOFade(0f, fadeOutTime));

        
        }

        _RP.RandomiseEnvionmentProps();

        //endRoomOB.GetComponent<EndLevelTrigger>().ResetDoor(); //reset animations
        //move end room trigger

        //move player to room
        player.transform.position = new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z);
        //print("Player is at " + player.transform.position);
        //print("Spawan is at " + new Vector3(levelStartRoom.position.x, 0, levelStartRoom.position.z));

        _EM.SpawnEnemiesForLevel();

    }


    void ClearPreviousLevel()
    {
        //destroy all room and any enemies, item etc. (could be all in same layer)
        if (levelParent.childCount != 0)
        {
            foreach (Transform child in levelParent.transform)
            {
                //if (_Mask.ObjMasked.Contains(child.gameObject)) _Mask.ObjMasked.Remove(child.gameObject);

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


