using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("LOADING SCREEN")]
    public bool waitForInput = true;
    public GameObject loadingMenu;
    public Slider loadingBar;
    public TMP_Text loadPromptText;
    public KeyCode userPromptKey;

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
        StartCoroutine(LoadAsynchronously("Mina"));
        Time.timeScale = 1.0f;
        InventoryCreator.restartInventory();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1.0f;
        InventoryCreator.restartInventory();
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

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        resumeCanvas.SetActive(false);
        loadingMenu.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            if (operation.progress >= 0.9f && waitForInput)
            {
                loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
                loadingBar.value = 1;

                if (Input.GetKeyDown(userPromptKey))
                {
                    operation.allowSceneActivation = true;
                }
            }
            else if (operation.progress >= 0.9f && !waitForInput)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}