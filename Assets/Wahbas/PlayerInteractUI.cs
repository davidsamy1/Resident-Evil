using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerInteractUI : MonoBehaviour
{
    public GameObject interactUICanvas;
    public PlayerInteract playerInteract;
    public TMP_Text interactMessageText;
    

    private void Update()
    {
        Interactable interactableItem = playerInteract.GetInteractableObject();
        if (interactableItem != null)
        {
            Show(interactableItem.GetInteractMessage());
        }
        else
        {
            Hide();
        }
    }

    public void Show(string message)
    {
        interactUICanvas.SetActive(true);
        interactMessageText.text = message;

    }

    public void Hide()
    {
        interactUICanvas.SetActive(false);
    }


}

