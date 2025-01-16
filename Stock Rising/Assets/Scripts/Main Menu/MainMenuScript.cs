using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public bool isMusicOn = true;
    public bool isSFXOn = true;
    public Sprite onSourceImg; 
    public Sprite offSourceImg; 
    public Sprite onPressedSprite; 
    public Sprite offPressedSprite;

    public GameObject musicButton;
    public GameObject sfxButton;

    public void PlayButtonClicked()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void InGameQuitButtonClicked()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void MusicButton()
    {
        Button myButton = musicButton.GetComponent<Button>();
        Image buttonImg = musicButton.GetComponent<Image>();

        if (isMusicOn == true)
        {
            // ganti source image 
            buttonImg.sprite = offSourceImg;

            // ganti pressed sprite
            SpriteState spriteState = myButton.spriteState;
            spriteState.pressedSprite = offPressedSprite;
            myButton.spriteState = spriteState;

            isMusicOn = !isMusicOn;
        } else
        {
            // ganti source image 
            buttonImg.sprite = onSourceImg;

            // ganti pressed sprite
            SpriteState spriteState = myButton.spriteState;
            spriteState.pressedSprite = onPressedSprite;
            myButton.spriteState = spriteState;

            isMusicOn = !isMusicOn;
        }

        AudioManager.instance.MuteMusic();
    }

    public void SFXButton()
    {
        Button myButton = sfxButton.GetComponent<Button>();
        Image buttonImg = sfxButton.GetComponent<Image>();

        if (isSFXOn == true)
        {
            // ganti source image 
            buttonImg.sprite = offSourceImg;

            // ganti pressed sprite
            SpriteState spriteState = myButton.spriteState;
            spriteState.pressedSprite = offPressedSprite;
            myButton.spriteState = spriteState;

            isSFXOn = !isSFXOn;
        }
        else
        {
            // ganti source image 
            buttonImg.sprite = onSourceImg;

            // ganti pressed sprite
            SpriteState spriteState = myButton.spriteState;
            spriteState.pressedSprite = onPressedSprite;
            myButton.spriteState = spriteState;

            isSFXOn = !isSFXOn;
        }

        AudioManager.instance.MuteSFX();
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
