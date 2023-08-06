using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUISwitcher : GameBehaviour
{
    [SerializeField]
    GameObject antlersUI;
    [SerializeField]
    GameObject beakUI;
    [SerializeField]
    GameObject clawUI; 
    [SerializeField]
    GameObject slugUI; 
    [SerializeField]
    GameObject fistUI; 

    GameObject newMeleeUI;

    public void SwitchMeleeUI(int _itemID)
    {
        print("Item ID is " + _itemID);

        switch (_itemID)
        {
            case 8: //CLAW
                newMeleeUI = clawUI;
                
                break;
            case 6: //SLUG
                newMeleeUI = slugUI;
                break;
            case 9: //HUMAN FIST
                newMeleeUI = fistUI;
                break;

        }
        _PC.meleeUI = newMeleeUI;
        newMeleeUI.GetComponent<Animator>().gameObject.SetActive(true);

       
        print("MELEE IS " + _PC.meleeUI);

        //destroy current child
        for (int i = 0; i < _PC.directional.transform.childCount; i++)
        {
            if (_PC.directional.transform.GetChild(i).name.Contains("Attack_Melee_"))
            {
                var objToDestroy = _PC.directional.transform.GetChild(i).gameObject;
                Destroy(objToDestroy);
            }
        }
        

        //make new one
        Instantiate(newMeleeUI, _PC.directional.transform);

    }
}
