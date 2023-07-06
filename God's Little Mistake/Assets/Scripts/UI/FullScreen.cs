using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    public void ScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Screen Changed");
    }

}
