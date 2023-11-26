using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : GameBehaviour
{
    bool isTrailActive;
    [SerializeField]
    float activeTime = 2f;
    float meshRefreshRate = 0.1f;

    private SpriteRenderer[] spriteRenderers;
    private Vector3[] spriteRenderersScale;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while(timeActive > 0)
        {
            timeActive -= meshRefreshRate;


            if (spriteRenderers == null)
            {
                spriteRenderers[i] = GetComponentsInChildren<SpriteRenderer>().;
                spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            }
            

            for (int i = 0; i < spriteRenderers.Length; i++)
            {

                GameObject gObj = new GameObject();

                //might need to change to sprite and sprite renderer or mat
                SpriteRenderer sr = gObj.AddComponent<SpriteRenderer>();

                Sprite sprite = null;
                sprite = spriteRenderers[i].sprite;

                sr.sprite = sprite;

            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

}
