using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        SceneManager.LoadSceneAsync(1);
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
