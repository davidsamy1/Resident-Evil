using UnityEngine;

public class StoreInteractor : MonoBehaviour, Interactable
{
    public UIVisibility uiVisibility;

    void Start()
    {
        uiVisibility = FindObjectOfType<UIVisibility>();

    }
    public void Interact()
    {
        // Debug.Log("Interacted with Store");
        uiVisibility.ToggleStoreVisibility();
        if(uiVisibility.isInventoryOpened)
        {
            uiVisibility.ToggleInventoryVisibility();
        }
    }

    public string GetInteractMessage()
    {
        return "Open Store";
    }

}
