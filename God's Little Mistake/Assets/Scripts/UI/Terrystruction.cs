using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Terrystruction : MonoBehaviour
{
    
    public enum PageNumber { Page1, Page2, Page3 };
    public PageNumber pageNumber;

    [Header("Page Contents")]
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Image instructionImage;
    public Button nextButton;
    public Button prevButton;

    [Header("Content Variations")]
    public Sprite[] insImg;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(OnNextClick);
        prevButton.onClick.AddListener(OnPrevClick);
        Establish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextClick()
    {
        switch (pageNumber)
        {
            case PageNumber.Page1:
                pageNumber = PageNumber.Page2;
                break;

            case PageNumber.Page2:
                pageNumber = PageNumber.Page3;
                break;

            case PageNumber.Page3:
                break;
        }

        if(nextButton.interactable) 
        {
            Establish();
        }
    }

    public void OnPrevClick()
    {
        switch (pageNumber)
        {
            case PageNumber.Page1:
                break;

            case PageNumber.Page2:
                pageNumber = PageNumber.Page1;
                break;

            case PageNumber.Page3:
                pageNumber = PageNumber.Page2;
                break;
        }

        if (prevButton.interactable)
        {
            Establish();
        }
    }


    public void Establish()
    {
        switch (pageNumber)
        {
            case PageNumber.Page1:
                instructionImage.sprite = insImg[0];
                titleText.text = "IM TERRY";
                bodyText.text = "Heya lil missy,[insert exposition here], you got that?";
                nextButton.interactable = true;
                prevButton.interactable = false;
                break;

            case PageNumber.Page2:
                instructionImage.sprite = insImg[1];
                titleText.text = "MOVE THEM CHEEKS";
                bodyText.text = "Use WASD to move, press space to use them special cheeks of yours or any other legs you're using";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page3:
                instructionImage.sprite = insImg[2];
                titleText.text = "SHOOT'EM'UP";
                bodyText.text = "Press left click to shoot using your head, press right click to shoot using your body";
                nextButton.interactable = false;
                prevButton.interactable = true;
                break;
        }
    }
}
