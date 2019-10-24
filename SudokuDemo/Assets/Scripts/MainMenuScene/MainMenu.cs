using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AsyncOperation _async;

    // Use this for initialization
    void Start()
    {
        _async = SceneManager.LoadSceneAsync(Constants.SCENE_GAMESCENE);
        _async.allowSceneActivation = false;
    }

   
 
    public void OnButton_Play()
    {
        // some fade animation i dont have time to do
        // pfju
        _async.allowSceneActivation = true;
    }

    public void OnToggle_EasyChange(bool state)
    {
        if(state)
            PlayerPrefs.SetInt(Constants.DIFFICULTY_MODE, 1);
    }
    public void OnToggle_MediumChange(bool state)
    {
        if(state)
            PlayerPrefs.SetInt(Constants.DIFFICULTY_MODE, 2);
    }
    public void OnToggle_HardChange(bool state)
    {
        if(state)
            PlayerPrefs.SetInt(Constants.DIFFICULTY_MODE, 3);
    }
}
