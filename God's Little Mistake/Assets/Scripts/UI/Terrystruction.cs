using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Terrystruction : MonoBehaviour
{
    
    public enum PageNumber { Page1, Page2 };
    public PageNumber pageNumber;

    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Image instructionImage;
    public Button nextButton;
    public Button prevButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
