using System.Collections;
using System.Collections.Generic;
using SlimUI.ModernMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverManager : MonoBehaviour
{
    [Header("MENUS")]
    public GameObject gameOverMenu;
    public GameObject firstMenu;

    public enum Theme { custom1, custom2, custom3 };
    [Header("THEME SETTINGS")]
    public Theme theme;
    private int themeIndex;
    public ThemedUIData themeController;

    [Header("PANELS")]
    public GameObject gameOverCanvas;

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
        gameOverMenu.SetActive(true);
        firstMenu.SetActive(true);
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

    public void Restart()
    {
        StartCoroutine(LoadAsynchronously("Mina"));
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1.0f;
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
        gameOverCanvas.SetActive(false);
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
