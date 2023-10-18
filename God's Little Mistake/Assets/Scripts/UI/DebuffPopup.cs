using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebuffPopup : GameBehaviour
{
    public float totalDuration;
    public float currentDuration;
    [SerializeField]
    Image fill;
    public bool isActive;

    private void Start()
    {
        fill.fillAmount = 1;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(isActive == true)
        {
            //UpdateDebuffBar(totalDuration, currentDuration);
            if (currentDuration > 0)
            {
                currentDuration -= Time.deltaTime;
                fill.fillAmount = currentDuration / totalDuration;
                Debug.Log(fill.fillAmount);
            }
            if (currentDuration <= 0)
            {
                isActive = false;
                Destroy(gameObject);
            }

        }
    }

    //call this function with the duration of the debuff on current duration and max duration
    void UpdateDebuffBar(float _maxDuration, float _currentDuration)
    {
        
        if(currentDuration > 0) 
        {
            _currentDuration -= Time.deltaTime;
            fill.fillAmount = _currentDuration / _maxDuration;
            Debug.Log(fill.fillAmount);
        }
        if(_currentDuration <= 0)
        {
            isActive = false;
            Destroy(gameObject);
        }
    }
}
