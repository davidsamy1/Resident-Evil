using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InteractionUI : MonoBehaviour
{
    public GameObject interactUICanvas;
    public InteractionController interactionController;
    public TMP_Text interactMessageText;
    public UIVisibility UIVisibility;

    private void Update()
    {

        if (UIVisibility.isStoreOpened || UIVisibility.isInventoryOpened || UIVisibility.isPaused) // Hide 'E' if inventory/store is opened
        {
            Hide();
            return;
        }

        if (interactionController.isPlayerInGrapple) // Knife Interactions has higher priority than items priority
        {
            Show("Break Free");
            return;
        }
        else if (interactionController.isKnockDown)
        {
            Show("Stab Enemy");
            return;
        }
        else
        {
            Hide();
        }

        Interactable interactableItem = interactionController.GetInteractableObject();
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

