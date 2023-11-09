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
    public bool rightHasHeat;
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


    // Start is called before the first frame update
    void Start()
    {
        leftHasFired = false;
        rightHasFired = false;
        leftHasHeat = false;
        rightHasHeat = false;

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
        if (leftHasFired)
        {
            // Reset the current value

            if (leftFireCurrent < leftFireTotal)
            {
                leftMouseDis.SetActive(true);
                //leftFireFilling = true;
                leftFireCurrent += Time.deltaTime; // Increment by Time.deltaTime for a linear 
                leftFirerate.fillAmount = leftFireCurrent / leftFireTotal;
            }
            else
            {
                leftMouseDis.SetActive(false);
                leftHasFired = false; // Set to false only when it reaches the total
                //leftFireFilling = false;
            }
        }

        //Right firerate fill
        if (rightHasFired)
        {
            // Reset the current value

            if (rightFireCurrent < rightFireTotal)
            {
                rightMouseDis.SetActive(true);
                //rightFireFilling = true;
                rightFireCurrent += Time.deltaTime; // Increment by Time.deltaTime for a linear 
                rightFirerate.fillAmount = rightFireCurrent / rightFireTotal;
            }
            else
            {
                rightMouseDis.SetActive(false);
                rightHasFired = false; // Set to false only when it reaches the total
                //rightFireFilling = false;
            }
        }

        //Left overheat fill
        if (leftHasHeat)
        {

            if (leftHeatCurrent > 0)
            {
                leftHeatCurrent -= Time.deltaTime;
                leftOverheat.fillAmount = leftHeatCurrent / leftHeatTotal;
                Debug.Log(rightFirerate.fillAmount);
            }
            if (leftHeatCurrent <= 0)
            {
                leftHasHeat = false;
            }
        }

        //Right overheat fill
        if (rightHasHeat)
        {

            if (rightHeatCurrent > 0)
            {
                rightHeatCurrent -= Time.deltaTime;
                rightOverheat.fillAmount = rightHeatCurrent / rightHeatTotal;
                Debug.Log(rightFirerate.fillAmount);
            }
            if (rightHeatCurrent <= 0)
            {
                rightHasFired = false;
            }

        }
    }

    public void LeftFill(float firerate)
    {
        Debug.Log(firerate);
        // Reset the current value
        leftFireTotal = firerate;
        leftFireCurrent = 0;
        leftFireCurrent += Time.deltaTime; // Increment by Time.deltaTime for a linear 
        if (leftFireCurrent < leftFireTotal)
        {
           leftMouseDis.SetActive(true);
           //leftFireFilling = true;
           leftFirerate.fillAmount = leftFireCurrent / leftFireTotal;
        }
        else
        {
           leftMouseDis.SetActive(false);
           //leftHasFired = false; // Set to false only when it reaches the total
           //leftFireFilling = false;
        }
    }

    //public void LeftFirerateUpdate()
    //{
    //    //leftFireCurrent = 0; // Reset the current value

    //    if (leftFireCurrent < leftFireTotal)
    //    {
    //        leftFireCurrent -= Time.deltaTime; // Increment by Time.deltaTime for a linear increase
    //        leftFirerate.fillAmount = leftFireCurrent / leftFireTotal;
    //    }
    //    else
    //    {
    //        leftHasFired = false; // Set to false only when it reaches the total
    //    }
    //}


}
