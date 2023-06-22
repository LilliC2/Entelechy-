using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameBehaviour<PlayerController>
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 2.0f;

    public GameObject head;


    public class Attack : GameBehaviour
    {
        public string atkName;
        public int ID;
        public string category;
        public bool active;

        public float dmg;
        public float dps;
        public float fireRate;

        public float critX; //crit dmg multipler
        public float critChance;
        public float range;

        public bool projectile;
        public float projectileSpeed;
        
    }

    public Attack headbutt;
    public Attack spit;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        #region Headbutt
        headbutt.name = "Headbutt";
        headbutt.ID = 1;
        
        #endregion
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        controller.Move(playerVelocity * Time.deltaTime);

        #region Attacks



        #endregion

    }


}

