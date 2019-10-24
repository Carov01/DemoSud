using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SimpleHideSplash : MonoBehaviour
{
    public Image imageFade;

    AsyncOperation _async;

    // Use this for initialization
    void Start()
    {
        imageFade.color = Color.white;
        imageFade.CrossFadeAlpha(1f, 0f, true);

        _async = SceneManager.LoadSceneAsync(Constants.SCENE_MAIN_MENU);
        _async.allowSceneActivation = false;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {

        yield return new WaitForSeconds(1.2f);
        imageFade.CrossFadeAlpha(0f, 1f, true);
        yield return new WaitForSeconds(3f);
        imageFade.CrossFadeAlpha(1f, 1f, true);
        yield return new WaitForSeconds(1.2f);
        _async.allowSceneActivation = true;
    }


}
