using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugLegsAnim : GameBehaviour
{
    public void Walk()
    {
        _AM.slugWalk.Play();
    }
}
