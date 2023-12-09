using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class DoorInteractor : MonoBehaviour, Interactable
{ 
    public enum KeyType
    {
        Emblem,
        KeyCard,
        Spade,
        Heart
    }
    private Boolean isDoorLocked = true;
    public KeyType requiredKey;
    private Animator doorAnimator;
    private Collider doorCollider;

    // Errors
    private Coroutine errorMessageCoroutine;
    public UIError uiError;

    public void Start()
    {
        doorCollider = GetComponent<Collider>();
        doorAnimator = GetComponent<Animator>();
        uiError = FindObjectOfType<UIError>();
    }

    public void Interact()
    {
        Item doorKey = null;
        Inventory inventory = InventoryCreator.getInstance();
        Debug.Log("Door Key Needed: " + requiredKey.ToString());
        if (isDoorLocked) 
        {
            switch(requiredKey)
            {
                case KeyType.Emblem:
                    {
                        doorKey = inventory.SearchItem(Item.ItemType.emblemKey);
                        break;
                    }
                case KeyType.Spade:
                    {
                        doorKey = inventory.SearchItem(Item.ItemType.spadeKey);
                        break;
                    }
                case KeyType.Heart:
                    {
                        doorKey = inventory.SearchItem(Item.ItemType.heartKey);
                        break;
                    }
                case KeyType.KeyCard:
                    {
                        doorKey = inventory.SearchItem(Item.ItemType.cardKey);
                        break;
                    }
            }

            if (doorKey != null)
            {
                isDoorLocked = false;
                Debug.Log("Door Unlocked");

                if (doorAnimator != null)
                {
                    doorAnimator.SetTrigger("Unlock");
                }

                if (doorCollider != null)
                {
                    doorCollider.enabled = false;
                }

                // Remove key from inventory
                inventory.Remove(doorKey);
                errorMessageCoroutine = null;

            }
            else
            {
                if (errorMessageCoroutine != null)
                {
                    StopCoroutine(errorMessageCoroutine); // stop if there exists an existing coroutine
                }

                string errorMessage = requiredKey != KeyType.KeyCard ? formatKeyTypeName(requiredKey) + " Key is required!" : "Key Card is Required!";
                errorMessageCoroutine = StartCoroutine(uiError.ShowErrorMessage(errorMessage, 2f));
            }

            
            
            

            
        }

    }

    public string GetInteractMessage()
    {
        return "Unlock Door";
    }

    string formatKeyTypeName(KeyType keyType)
    {
        string input = keyType.ToString();
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
                if (i == 0 && !char.IsUpper(currentChar)) // handle camelCase words
                    formattedName.Append(char.ToUpper(currentChar));
                else
                    formattedName.Append(currentChar);
            }
        }

        return formattedName.ToString();
    }



}
