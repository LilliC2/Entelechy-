using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeCheck : MonoBehaviour
{
    public List<GameObject> inRangeEnemies;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            inRangeEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            inRangeEnemies.Remove(other.gameObject);
        }
    }
}
