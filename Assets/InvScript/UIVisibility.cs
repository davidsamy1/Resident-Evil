using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class UIVisibility : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject storeUI;
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
        if (inventoryUI != null)
        {   
            if (inventoryUI.activeInHierarchy)
            {
                inventoryUI.SetActive(false);
                ResumeGame();
            }
            else
            {
                inventoryUI.SetActive(true);
                PauseGame();
            }
        }
        isInventoryOpened = !isInventoryOpened;
        
    }

    void ToggleStoreVisibility()
    {
        ToggleCursorVisibility(); // cursor is visibile when UI is on and off otherwise
        if (storeUI != null)
        {
            if (storeUI.activeInHierarchy)
            {
                storeUI.SetActive(false);
                ResumeGame();
            }
            else
            {
                storeUI.SetActive(true);
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
