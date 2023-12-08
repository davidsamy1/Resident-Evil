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
        if (isDoorLocked) // check if required key exists in inventory
        {
            // sheel key from inventory
            isDoorLocked = false;

            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Unlock");
            }

            if (doorCollider != null)
            {
                doorCollider.enabled = false;
            }

            Debug.Log("Door Unlocked");

            
        }

    }

    public string GetInteractMessage()
    {
        return "Unlock Door";
    }

}
