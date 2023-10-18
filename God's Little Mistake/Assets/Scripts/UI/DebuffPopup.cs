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
        UpdateDebuffBar(totalDuration, currentDuration);
        fill.fillAmount = 1;
        fill.fillAmount = 1;
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
