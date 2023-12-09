using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class CollectiblesInteractor : MonoBehaviour, Interactable
{
    
    public Item.ItemType collectibleType;
    public void Interact()
    {
        Inventory inventory = InventoryCreator.getInstance();
        Debug.Log("Interacted with " + formatCollectibleTypeName(collectibleType));

        // Valid if Not Revolver OR Revolver with Key Card in Inventory
        if (collectibleType!= Item.ItemType.revolver || 
            (collectibleType == Item.ItemType.revolver && inventory.SearchItem(Item.ItemType.cardKey)!=null))
        {
            inventory.AddPickUpItem(collectibleType);
            Destroy(gameObject);
        }
        else
        {
            //error here
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


}
