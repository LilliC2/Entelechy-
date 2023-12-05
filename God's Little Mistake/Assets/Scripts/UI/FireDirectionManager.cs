using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class FireDirectionManager : Singleton<FireDirectionManager>
{
    [Header("Head Fire")]
    public bool leftHasFired;
    public bool leftHasHeat;
    public bool leftFireFilling;
    public GameObject leftFillObject;
    public Image leftFirerate;
    public Image leftOverheat;
    public GameObject leftMouseHeat;
    public GameObject leftMouseDis;
    public float leftFireCurrent;
    public float leftFireTotal;
    public float leftHeatCurrent;
    public float leftHeatTotal;

    [Header("Torso Fire")]
    public bool rightHasFired;
    public bool rightFireFilling;
    public GameObject rightFillObject;
    public Image rightFirerate;
    public Image rightOverheat;
    public GameObject rightMouseHeat;
    public GameObject rightMouseDis;
    public float rightFireCurrent;
    public float rightFireTotal;
    public float rightHeatCurrent;
    public float rightHeatTotal;

    [Header("Torso Heat")]
    public bool rightHasHeat;
    public bool rHeatActive;
    public bool rHeatFilling;
    public bool cooldownRight;


    // Start is called before the first frame update
    void Start()
    {
        leftHasFired = false;
        rightHasFired = false;
        leftHasHeat = false;
        rightHasHeat = false;
        leftFireFilling = false;
        rightFireFilling = false;

        //Set firerate fill to full
        leftFirerate.fillAmount = 1;
        rightFirerate.fillAmount = 1;

        //overheat def to 0
        leftOverheat.fillAmount = 0;
        rightOverheat.fillAmount = 0;

        //disbale overheat mouse indicator
        leftMouseHeat.SetActive(false);
        rightMouseHeat.SetActive(false);
        leftMouseDis.SetActive(false);
        rightMouseDis.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Left firerate fill
        if (leftHasFired && !leftFireFilling)
        {
            StartCoroutine(ProcessLeftFire());

        }

        //Right firerate fill
        if (rightHasFired && !rightFireFilling)
        {
            StartCoroutine(ProcessRightFire());
        }

        if (rightHasFired && !rightFireFilling)
        {
            StartCoroutine(ProcessRightFire());
        }

        if(!cooldownRight && rightHasHeat)
        {
            if (rightHeatCurrent < rightHeatTotal)
            {
                rightOverheat.fillAmount = rightHeatCurrent / rightHeatTotal;
                rHeatFilling = true;
                rHeatActive = true;
            }
        }

        //if (Input.GetButtonDown("Fire2") && rightHasHeat && _PAtk.overHeatCooldown == false)
        //{
        //    rHeatActive = true;
        //}

        //if (Input.GetButtonUp("Fire2") && rightHasHeat)
        //{
        //    rHeatActive = false;
        //}

        //if (rHeatActive)
        //{
        //    if (rightHeatCurrent < rightHeatTotal)
        //    {
        //        rightHeatCurrent += Time.deltaTime * 4.99f;
        //        rightOverheat.fillAmount = rightHeatCurrent / rightHeatTotal;
        //        rHeatFilling = true;
        //    }
        //    else
        //    {
        //        rHeatActive = false;
        //    }
        //}

        //if (!rHeatActive)
        //{
        //        rightHeatCurrent -= Time.deltaTime * 2.2f;
        //        rightOverheat.fillAmount = rightHeatCurrent / rightHeatTotal;
        //        rHeatFilling = true;


        //    if(rightHeatCurrent <= 0)
        //    {
        //        rightHeatCurrent = 0;
        //        rightOverheat.fillAmount = 0;
        //        rHeatFilling = false;
        //    }
        //}
    }

    public void SetLeftAttack(float firerate)
    {
        leftHasFired = true;
        leftFireCurrent = 0;
        leftFireTotal = firerate;
    }

    public void SetRightAttack(float firerate)
    {
        rightHasFired = true;
        rightFireCurrent = 0;
        rightFireTotal = firerate;
    }

    public void SetRightHeat(float firerate)
    {
        if(!rHeatFilling)
        {
            rightHasHeat = true;
            rightFirerate.fillAmount = 1;
            rightHeatTotal = firerate;
            rightHeatCurrent = 0;
        }
        
        
    }

    public IEnumerator HeatRight()
    {
        //if(_PAtk.overHeatCooldown)
        //{
        //    rightFireFilling = true;


        //    while(rightHeatCurrent < rightHeatTotal)
        //    {
        //        rightMouseHeat.SetActive(true);
        //        leftFirerate.fillAmount = 1;
        //        rightHeatCurrent += Time.deltaTime;
        //        rightOverheat.fillAmount = rightHeatCurrent / rightFireTotal;
        //        yield return null;
        //    }

        //    leftFireFilling = false;
        //    rightMouseHeat.SetActive(false);
        //}

        rightHasHeat = true;


        while (rightHeatCurrent < rightHeatTotal)
        {
            leftFirerate.fillAmount = 1;
            rightHeatCurrent += Time.deltaTime;
            rightOverheat.fillAmount = rightHeatCurrent / rightFireTotal;
            yield return null;
        }

        leftFireFilling = false;
        rightMouseHeat.SetActive(true);
    }

    public IEnumerator ProcessLeftFire()
    {
        leftFireFilling = true;

        while (leftFireCurrent < leftFireTotal)
        {
            leftMouseDis.SetActive(true);
            leftFireCurrent += Time.deltaTime; // Increment by Time.deltaTime for a linear 
            leftFirerate.fillAmount = leftFireCurrent / leftFireTotal;
            yield return null;
        }

        leftMouseDis.SetActive(false);
        leftHasFired = false; // Set to false only when it reaches the total

        leftFireFilling = false;

    }

    public IEnumerator ProcessRightFire()
    {
        rightFireFilling = true;

        while (rightFireCurrent < rightFireTotal)
        {
            rightMouseDis.SetActive(true);
            rightFireCurrent += Time.deltaTime; // Increment by Time.deltaTime for a linear 
            rightFirerate.fillAmount = rightFireCurrent / rightFireTotal;
            yield return null;
        }

        rightMouseDis.SetActive(false);
        rightHasFired = false; // Set to false only when it reaches the total

        rightFireFilling = false;

    }


}
