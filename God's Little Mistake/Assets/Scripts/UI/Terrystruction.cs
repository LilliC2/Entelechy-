using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Terrystruction : MonoBehaviour
{
    
    public enum PageNumber { Page1, Page2, Page3, Page4, Page5, Page6, Page7 };
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
                pageNumber = PageNumber.Page4;
                break;

            case PageNumber.Page4: 
                pageNumber = PageNumber.Page5;
                break;

            case PageNumber.Page5:
                pageNumber = PageNumber.Page6;
                break;

            case PageNumber.Page6:
                pageNumber = PageNumber.Page7;
                break;

            case PageNumber.Page7:
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

            case PageNumber.Page4:
                pageNumber = PageNumber.Page3;
                break;

            case PageNumber.Page5:
                pageNumber = PageNumber.Page4;
                break;

            case PageNumber.Page6:
                pageNumber = PageNumber.Page5;
                break;

            case PageNumber.Page7:
                pageNumber = PageNumber.Page6;
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
                bodyText.text = "Name's Terry, im your tapewoor.... i mean tutorial as i now inhabit the cozy confines of your insides";
                nextButton.interactable = true;
                prevButton.interactable = false;
                break;

            case PageNumber.Page2:
                instructionImage.sprite = insImg[1];
                titleText.text = "MOVE THEM CHEEKS";
                bodyText.text = "Get movin' with WASD keys, Press space to flex those special cheeks or any other fancy legs you snag along the way.";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page3:
                instructionImage.sprite = insImg[2];
                titleText.text = "SHOOT'EM'UP";
                bodyText.text = "Ready, aim, spit?! Left-click to blast away with your head, and right-click to let your torso do the talking.";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page4:
                instructionImage.sprite = insImg[3];
                titleText.text = "Evict and Loot";
                bodyText.text = "Finish those misfits before they eat us and swipe their limbs by holding E to make us stonger.  member, you are only the sum of your parts!";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page5:
                instructionImage.sprite = insImg[4];
                titleText.text = "Spa Day";
                bodyText.text = "Take a dip in the pink goo of vanquished foes to rejuvenate. Ah, nothing beats a good viscera spa day!";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page6:
                instructionImage.sprite = insImg[5];
                titleText.text = "Doors of Opportunity";
                bodyText.text = "Deplete this ecosystem of mistakes to break into new biomes. It's extinction day, every day! :)";
                nextButton.interactable = true;
                prevButton.interactable = true;
                break;

            case PageNumber.Page7:
                instructionImage.sprite = insImg[6];
                titleText.text = "Final Words";
                bodyText.text = "Good luck sport and prove that you aren't Natures little mistake";
                nextButton.interactable = false;
                prevButton.interactable = true;
                break;
        }
    }
}
