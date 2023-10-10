using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPEAG : GameBehaviour
{
    public void FireProjectile()
    {
        _PC.FireProjectile(_PIA.bigPea, 300, 1, _PC.projectileRange);
    }


}
