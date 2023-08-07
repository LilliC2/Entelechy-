using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUISwitcher : GameBehaviour
{
    public List<GameObject> meleeUI;

    public void SwitchMeleeUI(int _itemID)
    {
        print("Item ID is " + _itemID);

        switch (_itemID)
        {
            case 8: //CLAW
                TurnOffOtherUI(meleeUI[0]);
                break;
            case 6: //SLUG
                TurnOffOtherUI(meleeUI[1]);
                break;
            case 9: //HUMAN FIST
                TurnOffOtherUI(meleeUI[2]);
                break;

        }



    }

    void TurnOffOtherUI(GameObject _active)
    {
        foreach (var item in meleeUI)
        {
            if (item != _active)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.SetActive(true);
                _PC.meleeUI = item;
            }
        }
    }
}
