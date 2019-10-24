using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonHandler : MonoBehaviour
{
    public static UIButtonHandler instance;

    public GameObject panelGameWin;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void OnGameWin()
    {
        panelGameWin.SetActive(true);
    }

    public void OnButton_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
