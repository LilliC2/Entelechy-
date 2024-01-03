using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nubs : GameBehaviour
{
    public void Step()
    {
        int r = Random.Range(0, 1);

        if (r == 0) _AM.nubsStep0.Play();
        else _AM.nubsStep1.Play();

    }
}
