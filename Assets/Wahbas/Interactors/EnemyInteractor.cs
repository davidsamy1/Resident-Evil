using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class EnemyInteractor : MonoBehaviour
{

    // Errors
    private Coroutine errorMessageCoroutine;
    public UIError uiError;
    public TPSController tpsController;
    public Grenade grenade;

    void Start()
    {
        uiError = FindObjectOfType<UIError>();

    }

    public void InteractGrappleWithKnife()
    {
        Debug.Log("Interacted with Grapple With Knife");
        int currentKnifeDurability = tpsController.knifeDurabilityGetter();
        Debug.Log("Current Knife Durability" + currentKnifeDurability);
        if (currentKnifeDurability < 2)
        {
            showInteractErrorMessage("Knife Durability is less than 2");
        }

        // Decrease Knife Durability by 2
        tpsController.knifeDurabilitySetter(currentKnifeDurability - 2);
 
    }

    public void InteractGrappleWithGrenade()
    {
        Debug.Log("Interacted with Grapple With Grenade");
        //grenade.isFlashSetter();
        if (!grenade.isFlashGetter() && !grenade.isExplodingGrenadeGetter())
        {
            showInteractErrorMessage("Grenade is not Equipped");
        }

        Inventory inventory = InventoryCreator.getInstance();
/*        if (grenade.isFlashGetter())
        {
            Item thrownFlashGrenade = inventory.SearchItem(Item.ItemType.flashGrenade);
            inventory.Remove(thrownFlashGrenade);
        }
        else
        {
            Item thrownFlashGrenade = inventory.SearchItem(Item.ItemType.handGrenade);
            inventory.Remove(thrownFlashGrenade);
        }*/
    }

    public void InteractKnockDown()
    {
        Debug.Log("Interacted with Knock Down");

        // Player Stabs
        // Enemy Gets Hit (make sure range allows player to hit enemy)
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
