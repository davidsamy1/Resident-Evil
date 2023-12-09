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

    public void Start()
    {
        doorCollider = GetComponent<Collider>();
        doorAnimator = GetComponent<Animator>();
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

            }
            else
            {
                // error
            }

            
            
            

            
        }

    }

    public string GetInteractMessage()
    {
        return "Unlock Door";
    }

}
