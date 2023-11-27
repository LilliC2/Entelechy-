using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : GameBehaviour
{
    bool isTrailActive;
    [SerializeField]
    float activeTime = 2f;
    float refreshRate = 0.1f;

    [SerializeField]
    GameObject fader;

    public GameObject[] childrenWithSprites;
    public SpriteRenderer[] spriteRenderers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }

        spriteRenderers = childrenWithSprites[0].GetComponentsInChildren<SpriteRenderer>();
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= refreshRate;

            

            for (int i = 0; i < spriteRenderers.Length; i++)
            {

                GameObject gObj = Instantiate(fader, new Vector3(childrenWithSprites[0].transform.position.x, childrenWithSprites[0].transform.position.y, childrenWithSprites[0].transform.position.z), childrenWithSprites[0].transform.rotation) as GameObject;

                gObj.transform.localScale = childrenWithSprites[0].transform.localScale;

                gObj.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spriteRenderers[i].GetComponent<SpriteRenderer>().sprite;

                gObj.transform.GetChild(i).gameObject.transform.position = childrenWithSprites[i + 1].gameObject.transform.position;
                gObj.transform.GetChild(i).gameObject.transform.localScale = childrenWithSprites[i + 1].gameObject.transform.localScale;
            }

            yield return new WaitForSeconds(refreshRate);
        }

        isTrailActive = false;
    }

}
