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
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (_GM.gameState)
        {
            case GameManager.GameState.Playing:

                gameObject.transform.position = new Vector3(player.transform.position.x, 3, player.transform.position.z + -3);
                break;

            case GameManager.GameState.Iventory:

                gameObject.transform.DOMoveY(2f, 2);
                gameObject.transform.DOMoveZ(-2f, 2);

                break;
        }
        
    }
}
