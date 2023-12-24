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
    public GameObject grappleUICanvas;
    public GameObject OriginalEButtonContainer;
    public GameObject TranslatedEButtonContainer;

    private void Update()
    {

        if (UIVisibility.isStoreOpened || UIVisibility.isInventoryOpened || UIVisibility.isPaused) // Hide 'E' if inventory/store is opened
        {
            Hide();
            return;
        }

        if (interactionController.isPlayerInGrapple) // Knife Interactions has higher priority than items priority
        {
            Show("Press 'E' to Stab Enemy or Press 'G' to use Grenade to Break Free");
            return;
        }
        else if (interactionController.knockDownEnemy)
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
        if (interactionController.isPlayerInGrapple)
        {
            grappleUICanvas.SetActive(true);
            OriginalEButtonContainer.SetActive(false);
            TranslatedEButtonContainer.SetActive(true);
        }
        else
        {
            OriginalEButtonContainer.SetActive(true);
            TranslatedEButtonContainer.SetActive(false);
        }
        


    }

    public void Hide()
    {
        interactUICanvas.SetActive(false);
        if (grappleUICanvas.activeInHierarchy)
            grappleUICanvas.SetActive(false);
        if (OriginalEButtonContainer.activeInHierarchy)
            OriginalEButtonContainer.SetActive(false);
        if (TranslatedEButtonContainer.activeInHierarchy)
            TranslatedEButtonContainer.SetActive(false);
    }


}

