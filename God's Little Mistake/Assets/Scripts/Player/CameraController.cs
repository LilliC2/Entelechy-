using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : GameBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        switch (_GM.gameState)
        {
            case GameManager.GameState.Playing:

                gameObject.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z + -6);
                break;

            //MIGHT NOT USE
            case GameManager.GameState.Iventory:



                break;
        }
        
    }
}
