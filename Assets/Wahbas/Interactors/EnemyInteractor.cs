using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyInteractor : MonoBehaviour
{

    // Errors
    private Coroutine errorMessageCoroutine;
    public UIError uiError;
    public TPSController tpsController;
    public Grenade grenade;

    public enemyScript enemy;

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
            return;
        }
        tpsController.breakGrapple();
        enemy.BreakGreapple("Knife");
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
            return;
        }

        Inventory inventory = InventoryCreator.getInstance();
       if (grenade.isFlashGetter())
        {
            Item thrownFlashGrenade = inventory.SearchItem(Item.ItemType.flashGrenade);
            inventory.Remove(thrownFlashGrenade);
            enemy.BreakGreapple("Grenade");
        }
        else
        {
            Item thrownFlashGrenade = inventory.SearchItem(Item.ItemType.handGrenade);
            inventory.Remove(thrownFlashGrenade);
            enemy.BreakGreapple("Grenade");
        }
    }

    public void InteractKnockDown(enemyScript enemyKnock)
    {
        Debug.Log("Interacted with Knock Down");
        int currentKnifeDurability = tpsController.knifeDurabilityGetter();
        if (currentKnifeDurability < 1)
        {
            showInteractErrorMessage("Knife Durability is less than 1");
            return;
        }
        tpsController.stabKnockedDownEnemy();
        enemyKnock.Die();
        tpsController.knifeDurabilitySetter(currentKnifeDurability - 1);

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
