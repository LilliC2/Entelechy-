using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraController : GameBehaviour
{
    public GameObject player;

    [SerializeReference]
    Collider[] enemiesNearPlayer;
    [SerializeReference]
    float playerCheckRadius;
    [SerializeReference]
    float lerpSpeed;
    [SerializeReference]
    LayerMask enemyLayerMask;
    enum CameraStates { Default, EnemyTargetting}
    [SerializeReference]
    CameraStates cameraState;
    public GameObject closestToMouse;

    public GameObject dummy;

    Vector3 defaultPOS;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        defaultPOS =  new Vector3(player.transform.position.x, 6, player.transform.position.z + -6);
        player = _PC.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        switch (cameraState)
        {
            case CameraStates.Default:
                gameObject.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z + -6);

                var target = GetNearbyEnemies();

                if (target != null) cameraState = CameraStates.EnemyTargetting;
                    break;

            //MIGHT NOT USE
            case CameraStates.EnemyTargetting:

                //gameObject.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z + -6);


                target = GetNearbyEnemies();

                if(target != null)
                {

                    //list.average

                    //get distance and devide in half
                    var x = (player.transform.position.x + target.transform.position.x) / 2;
                    var z = (player.transform.position.z + target.transform.position.z) / 2;

                    gameObject.transform.position = Vector3.SmoothDamp(transform.position,new Vector3(x, 6, z + -6),ref velocity,lerpSpeed);

                    if (Vector3.Distance(player.transform.position, target.transform.position) > playerCheckRadius)
                    {
                        closestToMouse = null;
                        gameObject.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, 6, player.transform.position.z + -6), ref velocity, lerpSpeed);
                    }

                }
                else
                {
                    gameObject.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, 6, player.transform.position.z + -6), ref velocity, 0.2f);

                    //ExecuteAfterSeconds(lerpSpeed, () => cameraState = CameraStates.Default);

                }

                break;
        }
        
    }

    GameObject GetNearbyEnemies()
    {


        enemiesNearPlayer = Physics.OverlapSphere(player.transform.position, playerCheckRadius, enemyLayerMask);


        var mouse = Input.mousePosition;
        mouse.z = 6;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);

        //dummy.transform.position = mousePos;
        

        foreach (var enemyCollider in enemiesNearPlayer)
        {
            if(closestToMouse == null) closestToMouse = enemyCollider.gameObject;
            else
            {
                if (Vector3.Distance(enemyCollider.gameObject.transform.position, mousePos) < Vector3.Distance(closestToMouse.transform.position, mousePos))
                {
                    if(Vector3.Distance(player.transform.position, enemyCollider.gameObject.transform.position)>playerCheckRadius)
                    {
                        closestToMouse = null;
                    }
                    else closestToMouse = enemyCollider.gameObject;

                }
            }
            
        }

        return closestToMouse;
    }
}
