using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class UIVisibility : MonoBehaviour
{
    public GameObject inventoryUICanvas;
    public GameObject storeUICanvas;
    public Boolean isInventoryOpened = false;
    public Boolean isStoreOpened = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryVisibility();
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
        }
    }

    void ToggleInventoryVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
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

    void ToggleStoreVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
        if (storeUICanvas != null)
        {
            if (storeUICanvas.activeInHierarchy)
            {
                storeUICanvas.SetActive(false);
                ResumeGame();
            }
            else
            {
                storeUICanvas.SetActive(true);
                PauseGame();
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
    
    void ToggleCursorVisibility()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
