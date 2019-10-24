using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Button button;
    public Text textValue;

    // original value when tile is given number
    public int originalTileValue;
    // if value of the tile is shown to user or not
    // if tile is visible it means their value is shown to user, and is not interactable
    public bool isTileVisible = true;


    private bool isNewValueCorrect = true;
    public bool IsNewValueCorrect
    {
        get
        {
            return isNewValueCorrect;
        }
        set
        {
            isNewValueCorrect = value;
        }
    }

    /// <summary>
    /// Initializes Tile
    /// </summary>
    /// <param name="value"></param>
    public void InitializeTile(int value)
    {
        originalTileValue = value;
        textValue.text = value.ToString();
    }

    public void TileIsInvisible()
    {
        isTileVisible = false;
        button.interactable = true;
        textValue.text = "";
        isNewValueCorrect = false;
    }

    /// <summary>
    /// When any button on board is pressed
    /// </summary>
    public void OnButton_Pressed()
    {
        GameBoard.instance.ShowNumericKeyboard(this);
    }

    /// <summary>
    /// When keyboard is pressed it will update ui to show new value
    /// </summary>
    /// <param name="value"></param>
    public void SetNewValue(int value)
    {
        if(value == 0)
            textValue.text = "";
        else
            textValue.text = value.ToString();

        // testing to see if this is how it can be done
        if(value == originalTileValue)
        {
            isNewValueCorrect = true;
            textValue.color = Color.green;
        }
        else
        {
            isNewValueCorrect = false;
            textValue.color = Color.red;
        }
        GameBoard.instance.CheckForCorrectValues();
    }
}
