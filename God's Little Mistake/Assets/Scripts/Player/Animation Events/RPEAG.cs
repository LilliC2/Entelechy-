using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPEAG : GameBehaviour
{
    PlayerAudioFunctions playerAudioFunctions;

    private void Start()
    {
        playerAudioFunctions = GetComponent<PlayerAudioFunctions>();
    }

    public void FireProjectile()
    {
        playerAudioFunctions.PeaShootSound();
        //_PC.FireProjectile(_PIA.bigPea, 300, 1, _PC.projectileRange);
    }


}
