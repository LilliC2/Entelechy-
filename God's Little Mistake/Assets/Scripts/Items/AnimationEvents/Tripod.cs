using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripod : GameBehaviour
{
    public void Walk()
    {
        _AM.tripodWalk.Play();

    }

    public void Bounce()
    {
        _AM.tripodBounce.Play();

    }

}
