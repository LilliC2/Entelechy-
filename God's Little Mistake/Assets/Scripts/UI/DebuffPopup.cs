using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebuffPopup : GameBehaviour
{
    public float durationTimer;
    public float duration;
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
        //durationTimer -= Time.deltaTime;

        //fill.fillAmount = durationTimer / durationTimer;

        //if (fill.fillAmount <= 0) Destroy(this.gameObject);
    }

    //call this function with the duration of the debuff on current duration and max duration
    void UpdateDebuffBar(float _maxDuration, float _currentDuration)
    {
        _currentDuration -= Time.deltaTime;
        fill.fillAmount = _currentDuration / _maxDuration;
        if(_currentDuration <= 0)
        {
            isActive = false;
            Destroy(gameObject);
        }
    }
}
