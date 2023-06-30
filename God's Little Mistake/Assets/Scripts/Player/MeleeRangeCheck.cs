using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeRangeCheck : MonoBehaviour
{

    [SerializeField] GameObject aimPoint; //Aims at the cursor
    [SerializeField] float meleeRange = 1.0f;
    public List<GameObject> inRangeEnemies;
    GameObject obj = null;
    public GameObject player;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
           
            for (int i = 0; i < inRangeEnemies.Count; i++)
            {
                float dist = Vector3.Distance(player.transform.position, inRangeEnemies[i].gameObject.transform.position);
                if (dist > meleeRange)
                {
                    inRangeEnemies.Remove(inRangeEnemies[i]);
                }
            }
        }
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(aimPoint.transform.position), out hit, meleeRange))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
                Debug.Log("Did Hit");
                obj = hit.collider.gameObject;
                EnemyCheck();
            }
        
    }

    void EnemyCheck()
    {
        if (obj.tag == "Enemy")
        {
            inRangeEnemies.Add(obj);
            obj = null;
        }
    }
}
