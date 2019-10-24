using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericKeyboard : MonoBehaviour
{

    public static NumericKeyboard instance;
    Tile currentActiveTile;

    public void OnKeyboardShown(Tile activeTile)
    {
        this.gameObject.SetActive(true);
        currentActiveTile = activeTile;
    }

    public void OnKeyboardButtonPressed(int numberPressed)
    {
        currentActiveTile.SetNewValue(numberPressed);
        this.gameObject.SetActive(false);
    }

}
