using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    //public GameObject mainMenuButton;
    //public GameObject loadingBarObj;
    public Slider loadingBarSlider;


    //private void OnEnable()
    //{
    //    mainMenuButton.SetActive(true);
    //    loadingBarObj.SetActive(false);
    //}

    private void Start()
    {
        LoadGame(2);
    }

    public void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //mainMenuButton.SetActive(false);
        //loadingBarObj.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            //Debug.Log(progress);
            loadingBarSlider.value = progress;

            yield return null;
        }
    }

    public void InGameQuitButtonClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
