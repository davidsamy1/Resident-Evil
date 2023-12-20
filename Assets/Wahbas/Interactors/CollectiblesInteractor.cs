using UnityEngine;
using System;
using System.Text;

public class CollectiblesInteractor : MonoBehaviour, Interactable
{
    
    public Item.ItemType collectibleType;
    private Coroutine errorMessageCoroutine;
    public UIError uiError;
    public AudioSource goldSFX;
    public AudioSource doorSFX;
    public AudioSource collectSFX;


    void Start()
    {
        uiError = FindObjectOfType<UIError>();

    }
    public void Interact()
    {
        Inventory inventory = InventoryCreator.getInstance();
        Debug.Log("Interacted with " + formatCollectibleTypeName(collectibleType));

        // Valid if Not Revolver OR Revolver with Key Card in Inventory
        if (collectibleType!= Item.ItemType.revolver || 
            (collectibleType == Item.ItemType.revolver && inventory.SearchItem(Item.ItemType.cardKey)!=null))
        {
            Boolean isSuccessful = inventory.AddPickUpItem(collectibleType);
            if (isSuccessful)
            {
                if (collectibleType == Item.ItemType.gold || collectibleType == Item.ItemType.goldTreasure)
                {
                    goldSFX.Play();
                } else  
                {
                    collectSFX.Play();
                }
                errorMessageCoroutine = null;
                Destroy(gameObject);
            }
            else
            {
                showInteractErrorMessage("Inventory is full!");
            }
            
        }
        else if (collectibleType == Item.ItemType.revolver && inventory.SearchItem(Item.ItemType.cardKey) == null)
        {
            showInteractErrorMessage("Key Card Required!");
        }
        
    }

    public string GetInteractMessage()
    {
        return "Pick up " + formatCollectibleTypeName (collectibleType);
    }

    string formatCollectibleTypeName(Enum collectibleType)
    {
        string input = collectibleType.ToString();
        StringBuilder formattedName = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];

            if (char.IsUpper(currentChar) && i > 0)
            {
                formattedName.Append(' ').Append(currentChar);
            }
            else
            {
                if (i==0 && !char.IsUpper(currentChar)) // handle camelCase words
                    formattedName.Append(char.ToUpper(currentChar));
                else
                    formattedName.Append(currentChar);
            }
        }

        return formattedName.ToString();
    }

    private void showInteractErrorMessage(string message)
    {
        if (errorMessageCoroutine != null)
        {
            StopCoroutine(errorMessageCoroutine); // stop if there exists an existing coroutine
        }

        errorMessageCoroutine = StartCoroutine(uiError.ShowErrorMessage(message, 2f));
    }

}
