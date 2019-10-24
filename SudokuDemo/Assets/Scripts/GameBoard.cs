using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class GameBoard : MonoBehaviour
{
    public static GameBoard instance;

    public GameObject board9x9;
    public Tile[] tiles;
    public NumericKeyboard numericKeyboard;

    int numberOfTilesToHide = 36;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // get difficulty mode - 1 for easy 2 medium 3 hard
        int difmode = PlayerPrefs.GetInt(Constants.DIFFICULTY_MODE, 1);
        switch(difmode)
        {
            case 1:
                {
                    numberOfTilesToHide = 36;
                    break;
                }
            case 2:
                {
                    numberOfTilesToHide = 44;
                    break;
                }
            case 3:
                {
                    numberOfTilesToHide = 56;
                    break;
                }
            default:
                {
                    numberOfTilesToHide = 36;
                    break;
                }
        }
    }

    /// <summary>
    /// Adds number to every button on the board
    /// </summary>
    /// <param name="values">new sudoku numbers</param>
    public void PopulateButtonsWithValues(int [] values)
    {
        for(int i = 0; i < values.Length; i++)
        {
            tiles[i].InitializeTile(values[i]);
        }

        ClearRadnomTiles();
    }


    /// <summary>
    /// Clear random field on the board so user have emtpy field to play
    /// </summary>
    void ClearRadnomTiles()
    {
        //clear random tile numbers with mirror method
        //generate numbers and their mirror counterparts

        int[] numbersToClear = new int[numberOfTilesToHide];

        for(int i = 0; i < numberOfTilesToHide;i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, 81);

            // prevents generation of same number
            while(numbersToClear.Contains(randomNumber))
                randomNumber = UnityEngine.Random.Range(0, 81);

            numbersToClear[i++] = randomNumber;
          
            numbersToClear[i] = 80 - randomNumber;
        }

        // Mark that tile is empty
        for(int i = 0; i < numbersToClear.Length; i++)
        {
            tiles[numbersToClear[i]].TileIsInvisible();
        }

        board9x9.SetActive(true);
    }


    /// <summary>
    /// Show keyboard when button is pressed
    /// </summary>
    /// <param name="tilePressed"></param>
    public void ShowNumericKeyboard(Tile tilePressed)
    {
        numericKeyboard.OnKeyboardShown(tilePressed);
    }

    /// <summary>
    /// Checks if sudoku is solved after every tile has been populated
    /// </summary>
    public void CheckForCorrectValues()
    {
        int correctTiles = 0;
        foreach(Tile tt in tiles)
        {
            if(tt.IsNewValueCorrect)
                correctTiles++;
        }

        if(correctTiles >= 81)
        {
            UIButtonHandler.instance.OnGameWin();
        }
         
    }
}
