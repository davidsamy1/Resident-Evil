using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    public abstract void Interact();
    public abstract string GetInteractMessage();
}

public class InteractionController : MonoBehaviour
{

    public GameObject player;
    //public EnemyDamageDealer enemyScript;
    public Boolean isPlayerInGrapple = false;
    public enemyScript knockDownEnemy = null;
    public UIVisibility UIVisibility;
    public EnemyInteractor enemyInteractor;
    public enemyScript enemyScript;
    public TPSController tpsController;

    private void Update()
    {
        knockDownEnemy = isKnockedDownEnemy();// checks if player collides with any knocked down enemies
        if (Input.GetKeyDown(KeyCode.G) && !UIVisibility.isInventoryOpened  && !UIVisibility.isStoreOpened && !UIVisibility.isPaused)
        {
            if (isPlayerInGrapple)
            {
                enemyInteractor.InteractGrappleWithGrenade();
                //isPlayerInGrapple = false;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !UIVisibility.isInventoryOpened && !UIVisibility.isStoreOpened && !UIVisibility.isPaused)
        { 
            if (isPlayerInGrapple)
            {
                enemyInteractor.InteractGrappleWithKnife();
                //isPlayerInGrapple = false;
                return;
            }

            if (knockDownEnemy!=null)
            {
                enemyInteractor.InteractKnockDown(knockDownEnemy);
                knockDownEnemy = null;
                return;
            }

            Interactable interactableItem = GetInteractableObject();
            if (interactableItem != null)
            {
                interactableItem.Interact();
            }


        }
    }

    public Interactable GetInteractableObject()
    {

        float interactRange = 1f;
        Collider[] colliderArray = Physics.OverlapSphere(player.transform.position, interactRange);
        Interactable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Interactable interactable) && !collider.CompareTag("EquippedRevolver") && !collider.CompareTag("throwGrenade"))
            {

                float distanceToInteractable = Vector3.Distance(player.transform.position, collider.transform.position);
                if (distanceToInteractable < closestDistance)
                {
                    closestDistance = distanceToInteractable;
                    closestInteractable = interactable;
                }

            }
        }
        return closestInteractable;
    }

    public enemyScript isKnockedDownEnemy()
    {
        Collider collider = tpsController.getBulletCollider().collider;
        if (collider!=null && collider.TryGetComponent(out enemyScript enemyScript) && collider.gameObject.tag == "KnockedDown")
        {
            return enemyScript;
        }
        return null;
    }
}

