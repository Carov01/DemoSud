using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SudokuGenerator : MonoBehaviour
{

    public static SudokuGenerator instance;

    private int[][] mat;
    private int[][] solvedMat;
    [SerializeField]
    private int N = 9;
    [SerializeField]
    private int SRN;
    [SerializeField]
    private int K;

    private GameObject[][] table;


    private void Awake()
    {
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        Double SRNd = Math.Sqrt(N);
        SRN = Convert.ToInt32(SRNd);
        Initialize();
        fillValues();
        GameBoard.instance.PopulateButtonsWithValues(GetNewSudokuNumbers());
    }

    private void Initialize()
    {
        mat = new int[N][];
        table = new GameObject[N][];

        for(int i = 0; i < N; i++)
        {
            mat[i] = new int[N];
            table[i] = new GameObject[N];
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    Debug.Log(mat[i][j] + " " + mat[i][++j] + " " + mat[i][++j]);
                }
            }
        }
    }



    
    // Return sudoku numbers as array

    public int[] GetNewSudokuNumbers()
    {
        int k = 0;
        int[] sudoku = new int[81];

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                sudoku[k++] = mat[i][j];
            }
        }
        return sudoku;
    }


    public void InitializeTable()
    {

        table = new GameObject[N][];

        for(int i = 0; i < N; i++)
        {

            table[i] = new GameObject[N];
        }
    }

    public void fillValues()
    {
        // Fill the diagonal of SRN x SRN matrices 
        fillDiagonal();

        // Fill remaining blocks 
        fillRemaining(0, SRN);

        // Remove Randomly K digits to make game 
        removeKDigits();
    }


    // Fill the diagonal SRN number of SRN x SRN matrices 
    void fillDiagonal()
    {

        for(int i = 0; i < N; i = i + SRN)

            // for diagonal box, start coordinates->i==j 
            fillBox(i, i);
    }

    // Returns false if given 3 x 3 block contains num. 
    bool unUsedInBox(int rowStart, int colStart, int num)
    {
        for(int i = 0; i < SRN; i++)
            for(int j = 0; j < SRN; j++)
                if(mat[rowStart + i][colStart + j] == num)
                    return false;

        return true;
    }


    // Fill a 3 x 3 matrix. 
    void fillBox(int row, int col)
    {
        int num;
        for(int i = 0; i < SRN; i++)
        {
            for(int j = 0; j < SRN; j++)
            {
                do
                {
                    num = randomGenerator(N);
                }
                while(!unUsedInBox(row, col, num));

                mat[row + i][col + j] = num;
            }
        }
    }

    // Random generator 
    int randomGenerator(int num)
    {
        System.Random r = new System.Random();
        return (int)Math.Floor((r.NextDouble() * num + 1));
    }

    // Check if safe to put in cell 
    bool CheckIfSafe(int i, int j, int num)
    {
        return (unUsedInRow(i, num) &&
                unUsedInCol(j, num) &&
                unUsedInBox(i - i % SRN, j - j % SRN, num));
    }

    // check in the row for existence 
    bool unUsedInRow(int i, int num)
    {
        for(int j = 0; j < N; j++)
            if(mat[i][j] == num)
                return false;
        return true;
    }

    // check in the row for existence 
    bool unUsedInCol(int j, int num)
    {
        for(int i = 0; i < N; i++)
            if(mat[i][j] == num)
                return false;
        return true;
    }

    // A recursive function to fill remaining  
    // matrix 
    bool fillRemaining(int i, int j)
    {
        //  System.out.println(i+" "+j); 
        if(j >= N && i < N - 1)
        {
            i = i + 1;
            j = 0;
        }
        if(i >= N && j >= N)
            return true;

        if(i < SRN)
        {
            if(j < SRN)
                j = SRN;
        }
        else if(i < N - SRN)
        {
            if(j == (int)(i / SRN) * SRN)
                j = j + SRN;
        }
        else
        {
            if(j == N - SRN)
            {
                i = i + 1;
                j = 0;
                if(i >= N)
                    return true;
            }
        }

        for(int num = 1; num <= N; num++)
        {
            if(CheckIfSafe(i, j, num))
            {
                mat[i][j] = num;
                if(fillRemaining(i, j + 1))
                    return true;

                mat[i][j] = 0;
            }
        }
        return false;
    }

    // Remove the K no. of digits to 
    // complete game 
    public void removeKDigits()
    {
        int count = K;
        while(count != 0)
        {
            int cellId = randomGenerator(N * N)-1;

            // System.out.println(cellId); 
            // extract coordinates i  and j 
            int i = (cellId / N);
            int j = cellId % 9;
         

            // System.out.println(i+" "+j); 
            if(mat[i][j] != 0)
            {
                count--;
                mat[i][j] = 0;
            }
        }
    }
}
