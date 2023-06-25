using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : Singleton<DragController>
{
    public Transform mCursorVisual;
    public Vector3 mDisplacement;
    public GameObject heldItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (heldItem != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit))
            {
                var mousePos = raycasthit.point;
                print(mousePos);

                heldItem.transform.position = new Vector3(mousePos.x,mousePos.y, _PC.transform.position.z);



            }

        }
    }
}
