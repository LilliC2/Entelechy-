using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAttack : MonoBehaviour
{
    public GameObject boxAttack;
    public GameObject ConeAttack1;
    public GameObject ConeAttack2;
    public GameObject ConeAttack3;

    public float coneSpeed1 = 0.2f;
    public float coneSpeed2 = 0.2f;
    public float boxDelay = 0.2f;

    public bool cone;
    public bool box;

    public float damage;

    public float cone1x;
    public float cone1y;
    public float cone1z;

    public float cone2x;
    public float cone2y;
    public float cone2z;

    public float cone3x;
    public float cone3y;
    public float cone3z;

    public float BoxSizeX;
    public float BoxSizeZ;


    public GameObject Player;
    public Camera phantomCamera;

    public bool reload;
    public float reloadTime;

    public float critChance;
    public float critMultiplier;

    public GameObject firingPoint;

    //public GameObject mouseLook;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        boxAttack.transform.localScale = new Vector3(BoxSizeX, 1, BoxSizeZ);
        ConeAttack1.transform.localScale = new Vector3(cone1x, cone1y, cone1z);
        ConeAttack2.transform.localScale = new Vector3(cone2x, cone2y, cone2z);
        ConeAttack3.transform.localScale = new Vector3(cone3x, cone3y, cone3z);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
        if (Input.GetMouseButtonDown(1))
        {
            if (cone == true)
            {
                StartCoroutine(ConeAttack());
            }
            if (box == true)
            {
                StartCoroutine (BoxAttack());
            }
        }
        MouseRotate();
    }
    public void MouseRotate()
    {
        Ray ray = phantomCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
            Mathf.Clamp(transform.rotation.x, 0, 0);
            Mathf.Clamp(transform.rotation.z, 0, 0);
        }
       // Ray mouseRay = phantomCamera.ScreenPointToRay(Input.mousePosition);
        //float midPoint = (transform.position - phantomCamera.transform.position).magnitude * 0.5f;

       // transform.LookAt(mouseRay.origin + mouseRay.direction);

    }

    public IEnumerator ConeAttack() //activates cone attack
    {
        ConeAttack1.SetActive(true);
        yield return new WaitForSeconds(coneSpeed1);
        ConeAttack2.SetActive(true);
        yield return new WaitForSeconds(coneSpeed2);
        ConeAttack3.SetActive(true);

        ConeAttack1.SetActive(false);
        ConeAttack2.SetActive(false);
        ConeAttack3.SetActive(false);
        StartCoroutine(Reload());
        StopCoroutine(ConeAttack());
    }

    public IEnumerator BoxAttack()
    {
        boxAttack.SetActive(true);
        yield return new WaitForSeconds(boxDelay);
        boxAttack.SetActive(false);
        StartCoroutine(Reload());
        StopCoroutine (BoxAttack());
    }

    public IEnumerator Reload()
    {
        reload = false;
        yield return new WaitForSeconds(reloadTime);
        reload = true;
        StopCoroutine(Reload());
    }
}
