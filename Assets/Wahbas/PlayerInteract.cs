using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    public abstract void Interact();
    public abstract string GetInteractMessage();
}

public class PlayerInteract : MonoBehaviour
{

    public GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Interactable interactableItem = GetInteractableObject();
            if (interactableItem != null)
            {
                interactableItem.Interact();
            }
        }
    }

    public Interactable GetInteractableObject()
    {
        // should return closest interactable item
        // should do some tag comparison also
        // box collider should be on interactable items
        // script added onto object, implementing interface and interact() logic
        // canvas should be linked to this script

        float interactRange = 1f;
        Collider[] colliderArray = Physics.OverlapSphere(player.transform.position, interactRange);
        Interactable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Interactable interactable) && !collider.CompareTag("EquippedRevolver"))
            {

                float distanceToInteractable = Vector3.Distance(player.transform.position, collider.transform.position);
                if (distanceToInteractable < closestDistance)
                {
                    closestDistance = distanceToInteractable;
                    closestInteractable = interactable;
                }

                //Destroy(collider);

            }
        }
        return closestInteractable;
    }



}

