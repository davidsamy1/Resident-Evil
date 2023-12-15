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
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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