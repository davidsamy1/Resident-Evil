using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEditor.PackageManager;

public class StoreInteractor : MonoBehaviour, Interactable
{
    public UIVisibility uiVisibility;

    void Start()
    {
        uiVisibility = FindObjectOfType<UIVisibility>();

    }
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
