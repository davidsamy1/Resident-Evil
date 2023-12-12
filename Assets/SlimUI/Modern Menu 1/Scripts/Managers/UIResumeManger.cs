using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using UnityEngine;

public class UIResumeManger : MonoBehaviour
{

    // campaign button sub menu
    [Header("MENUS")]
    public GameObject resumeMenu;
    public GameObject firstMenu;
    public GameObject restartMenu;
    public GameObject mainMenu;

    public enum Theme { custom1, custom2, custom3 };
    [Header("THEME SETTINGS")]
    public Theme theme;
    private int themeIndex;
    public ThemedUIData themeController;

    [Header("PANELS")]
    public GameObject resumeCanvas;

    [Header("SFX")]
    public AudioSource hoverSound;
    public AudioSource sliderSound;
    public AudioSource swooshSound;

    // Start is called before the first frame update
    void Start()
    {
        resumeMenu.SetActive(true);
        firstMenu.SetActive(true);
        restartMenu.SetActive(false);
        mainMenu.SetActive(false);
        SetThemeColors();
    }

    void SetThemeColors()
    {
        switch (theme)
        {
            case Theme.custom1:
                themeController.currentColor = themeController.custom1.graphic1;
                themeController.textColor = themeController.custom1.text1;
                themeIndex = 0;
                break;
            case Theme.custom2:
                themeController.currentColor = themeController.custom2.graphic2;
                themeController.textColor = themeController.custom2.text2;
                themeIndex = 1;
                break;
            case Theme.custom3:
                themeController.currentColor = themeController.custom3.graphic3;
                themeController.textColor = themeController.custom3.text3;
                themeIndex = 2;
                break;
            default:
                Debug.Log("Invalid theme selected.");
                break;
        }
    }

    public void Resume()
    {
        //set time scale back to 1
        //set the canvas active to false
    }

    public void ReturnMenu()
    {
        restartMenu.SetActive(false);
        mainMenu.SetActive(false);
        resumeMenu.SetActive(true);
    }

    public void AreYouSureRestart()
    {
        restartMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void AreYouSureMainMenu()
    {
        restartMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Restart()
    {
        //load the game scene again
    }

    public void ReturnMainMenu()
    {
        //load the main menu scene again
    }

    public void PlayHover()
    {
        hoverSound.Play();
    }

    public void PlaySFXHover()
    {
        sliderSound.Play();
    }

    public void PlaySwoosh()
    {
        swooshSound.Play();
    }
}