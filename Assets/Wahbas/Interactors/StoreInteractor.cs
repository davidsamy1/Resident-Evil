using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class StoreInteractor : MonoBehaviour, Interactable
{

    public UIVisibility uiVisibility;
    public void Interact()
    {
        Debug.Log("Interacted with Store");
        uiVisibility.ToggleStoreVisibility();
    }

    public string GetInteractMessage()
    {
        return "Open Store";
    }

}
