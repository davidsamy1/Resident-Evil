using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIVisibility : MonoBehaviour
{
    public GameObject storeUICanvas;
    public GameObject inventoryUICanvas;
    public GameObject pauseCanvas;

    public Boolean isInventoryOpened = false;
    public Boolean isStoreOpened = false;
    public bool isPaused = false;
    private StarterAssetsInputs starterAssetsInputs;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isPaused)
                ToggleInventoryVisibility();
            if(isStoreOpened)
            {
                ToggleStoreVisibility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInventoryOpened)
            {
                ToggleInventoryVisibility();
            }

            if (isStoreOpened)
            {
                ToggleStoreVisibility();
            }
            if (!isInventoryOpened && !isStoreOpened)
            {
                TogglePauseVisibility();
            }
        }
    }

  public  void ToggleInventoryVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
        starterAssetsInputs.cursorInputForLook = !starterAssetsInputs.cursorInputForLook;
        if (inventoryUICanvas != null)
        {
            if (inventoryUICanvas.activeInHierarchy)
            {
                inventoryUICanvas.SetActive(false);
                ResumeGame();
            }
            else
            {
                inventoryUICanvas.SetActive(true);
                PauseGame();
            }
        }
        isInventoryOpened = !isInventoryOpened;

    }

    public void ToggleStoreVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
        starterAssetsInputs.cursorInputForLook = !starterAssetsInputs.cursorInputForLook;
        Debug.Log(storeUICanvas);
        if (storeUICanvas != null)
        {
            if (storeUICanvas.activeInHierarchy)
            {
                storeUICanvas.SetActive(false);
                ResumeGame();
                Debug.Log("Store Not active now");
            }
            else
            {
                storeUICanvas.SetActive(true);
                PauseGame();
                Debug.Log("Store active now");

            }
        }
        isStoreOpened = !isStoreOpened;

    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }

  public void TogglePauseVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
        starterAssetsInputs.cursorInputForLook = !starterAssetsInputs.cursorInputForLook;
        // if (pauseCanvas != null)
        // {
            if (isPaused)
            {
                InputSystem.EnableDevice(Keyboard.current);
                pauseCanvas.SetActive(false);
                ResumeGame();
                isPaused = false;
            }
            else
            {
                // InputSystem.DisableDevice(Keyboard.current,false);
                pauseCanvas.SetActive(true);
                PauseGame();
                isPaused = true;
            }
            // isPaused = !isPaused;
        // }
      
    }

    void ToggleCursorVisibility()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
